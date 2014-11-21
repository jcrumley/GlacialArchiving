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
    [DisplayProperty("Value")]
    [ClassName("DataTag", "DataTags")]
	[ClassDisplayName("Data Tag", "Data Tags")]
    [Table("DataTag", Schema="datastorage")]
    [TrackHistory(TrackHistoryMode.ByProperty)]
    public partial class DataTag : Trackable
    {
        #region Property 'DataTagId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DataTagId { get; set; }
        #endregion
        #region Property 'UploadedItemId'
        [ForeignKey("UploadedItem")]
        [Display(Name = "UploadedItem")]
        public int UploadedItemId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual UploadedItem UploadedItem { get; set; }
        #endregion
        #region Property 'Value'
        [ControlEditor(ControlType.TextBox)]
        [MaxLength(100)]
        public string Value { get; set; }
        #endregion
        #region Property 'IsDeleted'
        [ControlEditor(ControlType.Bool)]
        [Display(Name = "Is Deleted")]
        public virtual bool IsDeleted { get; set; }
        #endregion
        
        public DataTag() { }
        
        partial void BeforeSyncExcludedFields(DataTag oldObj);
        public void SyncExcludedFields(DataTag oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}