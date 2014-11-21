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
    [ClassName("UploadedItemValidation", "UploadedItemValidations")]
	[ClassDisplayName("Uploaded Item Validation", "Uploaded Item Validations")]
    [Table("UploadedItemValidation", Schema="dbo")]
    public partial class UploadedItemValidation : Trackable
    {
        #region Property 'UploadedItemValidationId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UploadedItemValidationId { get; set; }
        #endregion
        #region Property 'UniqueIdentifier'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(100)]
        public string UniqueIdentifier { get; set; }
        #endregion
        #region Property 'Succeeded'
        [ControlEditor(ControlType.Bool)]
        public bool Succeeded { get; set; }
        #endregion
        #region Property 'InputData'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        public string InputData { get; set; }
        #endregion
        #region Property 'ValidationErrors'
        [InverseProperty("UploadedItemValidation")]
        [JsonIgnore]
        public virtual IList<ValidationError> ValidationErrors { get; set; }
        #endregion
        
        public UploadedItemValidation() { }
        
        partial void BeforeSyncExcludedFields(UploadedItemValidation oldObj);
        public void SyncExcludedFields(UploadedItemValidation oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}