using GlacialArchiving.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GlacialArchiving.DataAccess.DAL;

namespace GlacialArchiving.BusinessLogic.ExternalInterfaceObjects
{
    [Serializable]
    public class ExternalVaultStore
    {
        [Required]
        public string Client { get; set; }
        [Required]
        public string Uniqueid { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string Vault { get; set; }
        [Required]
        public string Id { get; set; }

        public void SetSampleValues()
        {
            this.Client = "client";
            this.Uniqueid = "someuniqueid";
            this.Region = "awsregion1";
            this.Vault = "awsvault1";
            this.Id = "awsglacierid1";
        }

        public class ValidationResult
        {
            public List<string> Errors { get; set; }
            public UploadedItem UploadedItem { get; set; }
        }

        public ValidationResult GetErrors(UnitOfWork work, System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            ValidationResult vr = new ValidationResult();
            List<string> errors = new List<String>();

            Client clientToFind = work.ClientRepository.All.FirstOrDefault(x => x.ClientIdTag == this.Client);
            if (clientToFind == null)
            {
                errors.Add("Could not find client for client '" + this.Client + "'");
            }

            if (clientToFind != null)
            {
                var check = work.UploadedItemRepository.Get(x => x.ClientId == clientToFind.ClientId &&
                                                x.UniqueIdentifier == this.Uniqueid);
                if (check == null || check.Count == 0)
                    errors.Add("uniqueid '" + this.Uniqueid + "' does not exist for this client.");
                else
                    vr.UploadedItem = check.First();
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

            vr.Errors = errors;

            return vr;
        }
    }
  
}
