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
    [ClassName("ValidationError", "ValidationErrors")]
	[ClassDisplayName("Validation Error", "Validation Errors")]
    [Table("ValidationError", Schema="dbo")]
    public partial class ValidationError : BaseObject
    {
        #region Property 'ValidationErrorId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ValidationErrorId { get; set; }
        #endregion
        #region Property 'UploadedItemValidationId'
        [ForeignKey("UploadedItemValidation")]
        [Display(Name = "UploadedItemValidation")]
        public int UploadedItemValidationId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual UploadedItemValidation UploadedItemValidation { get; set; }
        #endregion
        #region Property 'Error'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        public string Error { get; set; }
        #endregion
        
        public ValidationError() { }
        
        partial void BeforeSyncExcludedFields(ValidationError oldObj);
        public void SyncExcludedFields(ValidationError oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}