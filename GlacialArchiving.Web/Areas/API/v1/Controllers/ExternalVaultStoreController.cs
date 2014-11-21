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
using GlacialArchiving.BusinessLogic.ExternalInterfaceObjects;

namespace GlacialArchiving.Web.Areas.API.v1.Controllers
{
    public class ExternalVaultStoreController : BaseApiController
    {
        [HttpPost]
        //[Route("api/v1/externalinterface/validate")]
        public StoreVaultResult VaultStore(ExternalVaultStore itemToStore)
        {
            StoreVaultResult vr = new StoreVaultResult();
            vr.Valid = false;
            vr.Errors = new List<string>();
            vr.ExternalVaultStore = itemToStore;

            //completely hosed so don't save anything
            if (vr.ExternalVaultStore == null)
            {
                vr.ExternalVaultStore = new ExternalVaultStore();
                vr.ExternalVaultStore.SetSampleValues();

                vr.Valid = false;
                vr.Errors.Add("No input itemToStore.  Sample returned.");
                return vr;
            }

            ExternalVaultStore.ValidationResult ivr = itemToStore.GetErrors(work, ModelState);

            vr.Errors = ivr.Errors;
            vr.Valid = (vr.Errors.Count == 0);

            if (vr.Valid)
            {
                var glacierStorageItems = ivr.UploadedItem.GlacierStorages;
                bool hasOne = (glacierStorageItems != null && glacierStorageItems.Count > 0);

                GlacierStorage gs = new GlacierStorage() 
                { Region = itemToStore.Region, Vault = itemToStore.Vault, 
                    StorageId = itemToStore.Id, UploadedItem = ivr.UploadedItem };

                if (hasOne)
                    ivr.UploadedItem.StatusId = (int)UploadedItemStatusEnum.FullyStored;
                else
                    ivr.UploadedItem.StatusId = (int)UploadedItemStatusEnum.PartiallyStored;

                work.GlacierStorageRepository.InsertOrUpdate(gs, null);
                work.UploadedItemRepository.InsertOrUpdate(ivr.UploadedItem, null);
                work.Save();
            }

           
            return vr;
        }
    }
}