using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.Web.Areas.API.v1.Controllers;
using GlacialArchiving.Web.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Serialization;

namespace GlacialArchiving.Web.Areas.API.v1.Controllers
{
    public class ExternalStoreController : BaseApiController
    {
        [HttpPost]
        //[Route("api/v1/externalinterface/validate")]
        public StoreResult ValidateUploadedItem(ExternalUploadedItemToStore itemToStore)
        {
            StoreResult vr = new StoreResult();
            vr.Valid = false;
            vr.Errors = new List<string>();
            vr.ExternalUploadedItemToStore = itemToStore;

            //completely hosed so don't save anything
            if (vr.ExternalUploadedItemToStore == null)
            {
                vr.ExternalUploadedItemToStore = new ExternalUploadedItemToStore();
                vr.ExternalUploadedItemToStore.SetSampleValues();

                vr.Valid = false;
                vr.Errors.Add("No input itemToStore.  Sample returned.");
                return vr;
            }

            InternalValidationResult ivr = ExternalValidateController.GetErrorsForExternalUploadedItem(work, itemToStore, ModelState);
            vr.Errors = ivr.Errors;
            vr.Valid = (vr.Errors.Count == 0);

            //if validation has failed then log it and return
            if (!vr.Valid)
            {
                UploadedItemValidation uiv = new UploadedItemValidation() { UniqueIdentifier = itemToStore.uniqueid, Succeeded = vr.Valid };
                //this might be slow
                using (StringWriter stringWriter = new StringWriter())
                {
                    XmlSerializer serializer = new XmlSerializer(itemToStore.GetType());
                    serializer.Serialize(stringWriter, itemToStore);
                    uiv.InputData = stringWriter.ToString();
                }

                uiv.ValidationErrors = new List<ValidationError>();

                foreach (var item in vr.Errors)
                {
                    uiv.ValidationErrors.Add(new ValidationError() { Error = item });
                }
                work.UploadedItemValidationRepository.InsertOrUpdate(uiv, null);
                return vr;
            }


            UploadedItem testItem = new UploadedItem()
            {
                Client = ivr.Client,
                UniqueIdentifier = itemToStore.uniqueid,
                PayloadChecksum = itemToStore.checksum,
                PayloadFilename = itemToStore.filename,
                PayloadFilesize = itemToStore.filesize,
                PayloadType = ivr.PayloadType,
                TradingDateEnd = itemToStore.enddate,
                TradingDateStart = itemToStore.startdate,
                Description = itemToStore.description,
                ExpirationDate = itemToStore.enddate.AddMonths(ivr.PayloadType.MonthsToKeep),
                GenericField1 = itemToStore.field1,
                GenericField2 = itemToStore.field2
            };

            if (!string.IsNullOrWhiteSpace(itemToStore.replacementreason))
            {
                Replacement replacement = new Replacement()
                {
                    Reason = itemToStore.replacementreason,
                    UniqueIdentifier = itemToStore.replacementuniqueid,
                    UploadedItem = testItem
                };
                testItem.Replacements = new List<Replacement>();
                testItem.Replacements.Add(replacement);
            }
            if (itemToStore.tags != null && itemToStore.tags.Count > 0)
            {
                testItem.DataTags = new List<DataTag>();
                foreach (var tag in itemToStore.tags)
                {
                    testItem.DataTags.Add(new DataTag() { Value = tag });
                }
            }

            work.UploadedItemRepository.InsertOrUpdate(testItem, null);
            work.Save();
            return vr;
        }
    }

    public class StoreResult
    {
        public bool Valid { get; set; }

        public List<string> Errors { get; set; }

        public ExternalUploadedItemToStore ExternalUploadedItemToStore { get; set; }
    }

    [Serializable]
    public class ExternalUploadedItemToStore : ExternalUploadedItem
    {
        [Required]
        public string Region1 { get; set; }
        [Required]
        public string Region2 { get; set; }
        [Required]
        public string Vault1 { get; set; }
        [Required]
        public string Vault2 { get; set; }
        [Required]
        public string Id1 { get; set; }
        [Required]
        public string Id2 { get; set; }

        public override void SetSampleValues()
        {
            base.SetSampleValues();
            this.Region1 = "awsregion1";
            this.Region2 = "awsregion2";
            this.Vault1 = "awsvault1";
            this.Vault2 = "awsvault1";
            this.Id1 = "awsglacierid1";
            this.Id2 = "awsglacierid2";
        }
    }
}
