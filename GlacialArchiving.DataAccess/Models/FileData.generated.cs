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
    [ClassName("FileData", "FileData")]
	[ClassDisplayName("File Data", "File Data")]
    [Table("FileData", Schema="dbo")]
    public partial class FileData : BaseObject
    {
        #region Property 'FileDataId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FileDataId { get; set; }
        #endregion
        #region Property 'Data'
        [ControlEditor(ControlType.ByteArray)]
        public byte[] Data { get; set; }
        #endregion
        
        public FileData() { }
        
        partial void BeforeSyncExcludedFields(FileData oldObj);
        public void SyncExcludedFields(FileData oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}