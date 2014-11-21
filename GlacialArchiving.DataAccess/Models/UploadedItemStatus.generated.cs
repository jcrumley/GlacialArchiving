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
    [DisplayProperty("Name")]
    [ClassName("UploadedItemStatus", "UploadedItemStatuss")]
	[ClassDisplayName("UploadedItemStatus", "UploadedItemStatuss")]
    [Table("UploadedItemStatus", Schema="datastorage")]
    public partial class UploadedItemStatus : BaseObject
    {
        #region Property 'UploadedItemStatusId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UploadedItemStatusId { get; set; }
        #endregion
        #region Property 'Name'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        #endregion
        
        public UploadedItemStatus() { }
        
        partial void BeforeSyncExcludedFields(UploadedItemStatus oldObj);
        public void SyncExcludedFields(UploadedItemStatus oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
        
        #region Property 'EnumValue'
        [NotMapped]
        [NoDisplay]
        public UploadedItemStatusEnum EnumValue { get { return (UploadedItemStatusEnum)this.Id; } }
        #endregion
    }
    
    #region Enum 'UploadedItemStatusEnum'
    public enum UploadedItemStatusEnum : int
    {
        Validated = 1,
        PartiallyStored = 2,
        FullyStored = 3
    }
    #endregion
}