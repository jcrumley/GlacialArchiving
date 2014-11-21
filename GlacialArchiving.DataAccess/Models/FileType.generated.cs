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
    [ClassName("FileType", "FileTypes")]
	[ClassDisplayName("File Type", "File Types")]
    [Table("FileType", Schema="enum")]
    public partial class FileType : BaseObject
    {
        #region Property 'FileTypeId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FileTypeId { get; set; }
        #endregion
        #region Property 'Name'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        #endregion
        #region Property 'Extension'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(10)]
        public string Extension { get; set; }
        #endregion
        #region Property 'ContextType'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(50)]
        public string ContextType { get; set; }
        #endregion
        
        public FileType() { }
        
        partial void BeforeSyncExcludedFields(FileType oldObj);
        public void SyncExcludedFields(FileType oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}