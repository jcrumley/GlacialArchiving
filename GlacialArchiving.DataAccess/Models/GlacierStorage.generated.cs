using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using TBADev.MVC.DataAttributes;
using TBADev.MVC.Entity.Generation;
using TBADev.MVC.MVCHelpers;
using GlacialArchiving.DataAccess.Base;

namespace GlacialArchiving.DataAccess.Models
{
    [DisplayProperty("StorageId")]
    [ClassName("GlacierStorage", "GlacierStorages")]
	[ClassDisplayName("GlacierStorage", "GlacierStorages")]
    [Table("GlacierStorage", Schema="datastorage")]
    public partial class GlacierStorage : Trackable
    {
        #region Property 'GlacierStorageId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GlacierStorageId { get; set; }
        #endregion
        #region Property 'UploadedItemId'
        [ForeignKey("UploadedItem")]
        [Display(Name = "UploadedItem")]
        public int UploadedItemId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual UploadedItem UploadedItem { get; set; }
        #endregion
        #region Property 'Region'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(400)]
        public string Region { get; set; }
        #endregion
        #region Property 'Vault'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(400)]
        public string Vault { get; set; }
        #endregion
        #region Property 'StorageId'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(400)]
        public string StorageId { get; set; }
        #endregion
        
        public GlacierStorage() { }
        
        partial void BeforeSyncExcludedFields(GlacierStorage oldObj);
        public void SyncExcludedFields(GlacierStorage oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}