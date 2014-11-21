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
    [DisplayProperty("Model")]
    [ClassName("HistoryLog", "HistoryLogs")]
	[ClassDisplayName("History Log", "History Logs")]
    [Table("HistoryLog", Schema="audit")]
    public partial class HistoryLog : Trackable
    {
        #region Property 'HistoryLogId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HistoryLogId { get; set; }
        #endregion
        #region Property 'Model'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(250)]
        public string Model { get; set; }
        #endregion
        #region Property 'ModelId'
        [ControlEditor(ControlType.Int)]
        [Display(Name = "Model Id")]
        public int ModelId { get; set; }
        #endregion
        #region Property 'PropertyName'
        [ControlEditor(ControlType.TextBox)]
        [Display(Name = "Property Name")]
        [Required]
        [MaxLength(250)]
        public string PropertyName { get; set; }
        #endregion
        #region Property 'OldValue'
        [ControlEditor(ControlType.TextBox)]
        [Display(Name = "Old Value")]
        public string OldValue { get; set; }
        #endregion
        #region Property 'OldModelId'
        [ControlEditor(ControlType.Int)]
        [Display(Name = "Old Model Id")]
        public int? OldModelId { get; set; }
        #endregion
        #region Property 'NewValue'
        [ControlEditor(ControlType.TextBox)]
        [Display(Name = "New Value")]
        public string NewValue { get; set; }
        #endregion
        #region Property 'NewModelId'
        [ControlEditor(ControlType.Int)]
        [Display(Name = "New Model Id")]
        public int? NewModelId { get; set; }
        #endregion
        
        public HistoryLog() { }
        
        partial void BeforeSyncExcludedFields(HistoryLog oldObj);
        public void SyncExcludedFields(HistoryLog oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}