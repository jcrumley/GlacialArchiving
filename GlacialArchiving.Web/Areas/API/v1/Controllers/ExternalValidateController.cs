using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.Web.Common;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using GlacialArchiving.DataAccess.DAL;
using System.Web.Http.ModelBinding;

namespace GlacialArchiving.Web.Areas.API.v1.Controllers
{
    public class ExternalValidateController : BaseApiController
    {

        public ExternalValidateController()
        {
            var xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
            xml.UseXmlSerializer = true;
        }

        [HttpPost]
        //[Route("api/v1/externalinterface/validate")]
        public ValidationResult ValidateUploadedItem(ExternalUploadedItem itemToValidate)
        {
            ValidationResult vr = new ValidationResult();
            vr.Valid = false;
            vr.ExternalUploadedItem = itemToValidate;

            //completely hosed so don't save anything
            if (vr.ExternalUploadedItem == null)
            {
                vr.Errors = new List<string>();
                vr.ExternalUploadedItem = new ExternalUploadedItem();
                vr.ExternalUploadedItem.SetSampleValues();

                vr.Valid = false;
                vr.Errors.Add("No input itemToValidate.  Sample returned.");
                return vr;
            }

            InternalValidationResult ivr = GetErrorsForExternalUploadedItem(work, itemToValidate, ModelState);
            vr.Errors = ivr.Errors;
           
            vr.Valid = (vr.Errors.Count == 0);
            
            UploadedItemValidation uiv = new UploadedItemValidation() { UniqueIdentifier = itemToValidate.uniqueid, Succeeded = vr.Valid };
            if (string.IsNullOrWhiteSpace(uiv.UniqueIdentifier))
                uiv.UniqueIdentifier = "UNKNOWN";

            //this might be slow
            using(StringWriter stringWriter = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(itemToValidate.GetType());
                serializer.Serialize(stringWriter, itemToValidate);
                uiv.InputData = stringWriter.ToString();
            }

            uiv.ValidationErrors = new List<ValidationError>();

            foreach (var item in vr.Errors)
            {
                uiv.ValidationErrors.Add(new ValidationError() { Error = item, UploadedItemValidation = uiv });
            }
            work.UploadedItemValidationRepository.InsertOrUpdate(uiv, null);

            //save the item if it is good
            if (vr.Valid)
            {
                var itemToStore = itemToValidate.getUploadedItem(ivr);

                itemToStore.StatusId = (int) UploadedItemStatusEnum.Validated;
                work.UploadedItemRepository.InsertOrUpdate(itemToStore, null);
            }

            work.Save();

            return vr;
        }

        public static InternalValidationResult GetErrorsForExternalUploadedItem(UnitOfWork work, 
            ExternalUploadedItem itemToValidate, ModelStateDictionary modelState)
        {
            InternalValidationResult ivr = new InternalValidationResult();
            List<string> errors = new List<String>();
            Client clientToFind = work.ClientRepository.All.FirstOrDefault(x => x.ClientIdTag == itemToValidate.client);
            if (clientToFind == null)
            {
                errors.Add("Could not find client for client '" + itemToValidate.client + "'");
            }

            PayloadType pt = work.PayloadTypeRepository.All.FirstOrDefault(x => x.Name == itemToValidate.type);
            if (pt == null)
            {
                errors.Add("Could not find type for '" + itemToValidate.type + "'");
            }

            if (!modelState.IsValid)
            {
                foreach (string key in modelState.Keys)
                {
                    if (modelState[key].Errors.Count > 0)
                    {
                        for (int i = 0; i < modelState[key].Errors.Count; i++)
                            errors.Add(string.Format("{0} : {1}", key, modelState[key].Errors[0].ErrorMessage));
                    }
                }
            }

            if (itemToValidate.startdate != null)
            {
                if (itemToValidate.startdate.Date > DateTime.Now.Date)
                    errors.Add("startdate cannot be in the future.");
                if (itemToValidate.startdate.Date < DateTime.Now.AddYears(-3).Date)
                    errors.Add("startdate cannot be more than three years in the past.");
            }
            if (itemToValidate.enddate != null)
            {
                if (itemToValidate.enddate.Date > DateTime.Now.Date)
                    errors.Add("enddate cannot be in the future.");
                if (itemToValidate.enddate.Date < DateTime.Now.AddYears(-3).Date)
                    errors.Add("enddate cannot be more than three years in the past.");
            }

            if (string.IsNullOrWhiteSpace(itemToValidate.uniqueid))
            {
                errors.Add("uniqueid cannot be empty.");
            }
            else
            {
                if (clientToFind != null)
                {
                    var check = work.UploadedItemRepository.Get(x => x.ClientId == clientToFind.ClientId &&
                                                    x.UniqueIdentifier == itemToValidate.uniqueid);
                    if (check != null && check.Count > 0)
                        errors.Add("uniqueid already exists for this client.");
                }
            }

            bool hasReplacementId = string.IsNullOrWhiteSpace(itemToValidate.replacementuniqueid);
            bool hasReplacementReason = string.IsNullOrWhiteSpace(itemToValidate.replacementuniqueid);

            if (!(hasReplacementId == hasReplacementReason))
            {
                errors.Add("Both replacement unique identifier and replacement reason must be specified or neither.");
            }

            if (hasReplacementId)
            {
                if (clientToFind != null)
                {
                    var check = work.UploadedItemRepository.Get(x => x.ClientId == clientToFind.ClientId &&
                                                    x.UniqueIdentifier == itemToValidate.replacementuniqueid);
                    if (check == null || check.Count != 1)
                        errors.Add("Did not find the replacementuniqueid.  There was none or more than 1.");
                }
            }

            if (itemToValidate.allcustomers && itemToValidate.customers != null)
            {
                errors.Add("'allcustomers' can not be true and the customers element be provided.");
            }

            if (itemToValidate.alldepartments && itemToValidate.departments != null)
            {
                errors.Add("'alldepartments' can not be true and the departments element be provided.");
            }

            ivr.Errors = errors;
            ivr.Client = clientToFind;
            ivr.PayloadType = pt;

            return ivr;
        }
    }

 
    public class InternalValidationResult
    {
        public List<string> Errors { get; set; }
        public Client Client { get; set; }
        public PayloadType PayloadType { get; set; }
    }

    public class ValidationResult
    {
        public bool Valid { get; set; }

        public List<string> Errors { get; set; }

        public ExternalUploadedItem ExternalUploadedItem { get; set; }
    }

    //[DataContract()]
    //[Serializable]
    public class ExternalUploadedItem
    {
        [Required]
        public string client { get; set; }
        [Required]
        public string uniqueid { get; set; }
        [Required]
        public string type { get; set; }
        [Required]
        public string filename { get; set; }
        [Required]
        public string checksum { get; set; }
        [Required]
        public string filesize { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        [Required]
        public string description { get; set; }
        public string field1 { get; set; }
        public string field2 { get; set; }
        public bool alldepartments { get; set; }
        [XmlArray(ElementName = "departments")]
        [XmlArrayItem(ElementName = "department")]
        public List<string> departments { get; set; }
        public bool allcustomers { get; set; }
        [XmlArray(ElementName = "customers")]
        [XmlArrayItem(ElementName = "customer")]
        public List<string> customers { get; set; }
        public string replacementuniqueid { get; set; }
        public string replacementreason { get; set; }
        [XmlArray(ElementName = "tags")]
        [XmlArrayItem(ElementName = "tag")]
        public List<string> tags { get; set; }

        public virtual void SetSampleValues()
        {
            this.client = "TESTCLIENT1";
            this.type = "Order Data";
            this.uniqueid = "abce";
            this.checksum = "a1234";
            this.description = "description";
            this.filename = "filename";
            this.filesize = "1234";
            this.startdate = DateTime.Today;
            this.enddate = DateTime.Today;
            this.allcustomers = true;
            this.alldepartments = true;
            this.customers = new List<string>() { "a", "b" };
            this.tags = new List<string>() { "c", "d" };
            this.field1 = "gf1";
            this.field2 = "gf2";
            this.replacementreason = "some reason";
            this.replacementuniqueid = "uniq id";
            this.departments = new List<string>() { "e", "f" };
        }

        public UploadedItem getUploadedItem(InternalValidationResult ivr)
        {
            UploadedItem item = new UploadedItem()
            {
                Client = ivr.Client,
                UniqueIdentifier = this.uniqueid,
                PayloadChecksum = this.checksum,
                PayloadFilename = this.filename,
                PayloadFilesize = this.filesize,
                PayloadType = ivr.PayloadType,
                TradingDateEnd = this.enddate,
                TradingDateStart = this.startdate,
                Description = this.description,
                ExpirationDate = this.enddate.AddMonths(ivr.PayloadType.MonthsToKeep),
                GenericField1 = this.field1,
                GenericField2 = this.field2,
            };

            if (!string.IsNullOrWhiteSpace(this.replacementreason))
            {
                Replacement replacement = new Replacement()
                {
                    Reason = this.replacementreason,
                    UniqueIdentifier = this.replacementuniqueid,
                    UploadedItem = item
                };
                item.Replacements = new List<Replacement>();
                item.Replacements.Add(replacement);
            }
            if (this.tags != null && this.tags.Count > 0)
            {
                item.DataTags = new List<DataTag>();
                foreach (var tag in this.tags)
                {
                    item.DataTags.Add(new DataTag() { Value = tag });
                }
            }

            if (this.customers != null && this.customers.Count > 0)
            {
                item.DataTags = new List<DataTag>();
                foreach (var customer in this.customers)
                {
                    item.CustomerCoverages.Add(new CustomerCoverage() { Value = customer });
                }
            }

            return item;
        }
    }


}
