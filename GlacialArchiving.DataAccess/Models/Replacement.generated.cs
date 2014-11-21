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
    [DisplayProperty("Reason")]
    [ClassName("Replacement", "Replacements")]
	[ClassDisplayName("Replacement", "Replacements")]
    [Table("Replacement", Schema="datastorage")]
    [TrackHistory(TrackHistoryMode.ByProperty)]
    public partial class Replacement : Trackable
    {
        #region Property 'ReplacementId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReplacementId { get; set; }
        #endregion
        #region Property 'UploadedItemId'
        [ForeignKey("UploadedItem")]
        [Display(Name = "Uploaded Item")]
        public int UploadedItemId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual UploadedItem UploadedItem { get; set; }
        #endregion
        #region Property 'UniqueIdentifier'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(100)]
        public string UniqueIdentifier { get; set; }
        #endregion
        #region Property 'Reason'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        public string Reason { get; set; }
        #endregion
        #region Property 'IsDeleted'
        [ControlEditor(ControlType.Bool)]
        [Display(Name = "Is Deleted")]
        public virtual bool IsDeleted { get; set; }
        #endregion
        
        public Replacement() { }
        
        partial void BeforeSyncExcludedFields(Replacement oldObj);
        public void SyncExcludedFields(Replacement oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}