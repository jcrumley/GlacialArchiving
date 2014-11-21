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
    [DisplayProperty("FileName")]
    [ClassName("StoredFile", "StoredFile")]
	[ClassDisplayName("StoredFile", "StoredFile")]
    [Table("StoredFile", Schema="dbo")]
    public partial class StoredFile : Trackable
    {
        #region Property 'StoredFileId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoredFileId { get; set; }
        #endregion
        #region Property 'FileName'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(50)]
        public string FileName { get; set; }
        #endregion
        #region Property 'FileTypeId'
        [ForeignKey("FileType")]
        [Display(Name = "FileType")]
        public int FileTypeId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual FileType FileType { get; set; }
        #endregion
        #region Property 'FileDataId'
        [ForeignKey("FileData")]
        [Display(Name = "FileData")]
        public int FileDataId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual FileData FileData { get; set; }
        #endregion
        
        public StoredFile() { }
        
        partial void BeforeSyncExcludedFields(StoredFile oldObj);
        public void SyncExcludedFields(StoredFile oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}