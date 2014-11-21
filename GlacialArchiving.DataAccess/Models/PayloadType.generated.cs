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
    [ClassName("PayloadType", "PayloadTypes")]
	[ClassDisplayName("Payload Type", "Payload Types")]
    [Table("PayloadType", Schema="datastorage")]
    [TrackHistory(TrackHistoryMode.ByProperty)]
    public partial class PayloadType : Trackable
    {
        #region Property 'PayloadTypeId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PayloadTypeId { get; set; }
        #endregion
        #region Property 'Name'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        #endregion
        #region Property 'MonthsToKeep'
        [ControlEditor(ControlType.Int)]
        [DisplayFormat(DataFormatString="n0")]
        public int MonthsToKeep { get; set; }
        #endregion
        #region Property 'UploadedItems'
        [InverseProperty("PayloadType")]
        [JsonIgnore]
        public virtual IList<UploadedItem> UploadedItems { get; set; }
        #endregion
        #region Property 'IsDeleted'
        [ControlEditor(ControlType.Bool)]
        [Display(Name = "Is Deleted")]
        public virtual bool IsDeleted { get; set; }
        #endregion
        
        public PayloadType() { }
        
        partial void BeforeSyncExcludedFields(PayloadType oldObj);
        public void SyncExcludedFields(PayloadType oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}