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
using TBADev.MVC.DataAttributes;
using TBADev.MVC.Entity.Generation;
using GlacialArchiving.DataAccess.Base;

namespace GlacialArchiving.DataAccess.Models
{
    [DisplayProperty("Name")]
    [ClassName("Setting", "Settings")]
    [Table("Setting")]
    public partial class Setting : BaseObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SettingId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Data { get; set; }
        [MaxLength(100)]
        public string Type { get; set; }
        [Display(Name = "Is Static")]
        public bool IsStatic { get; set; }
		
        public Setting() { }
		        
        partial void BeforeSyncExcludedFields(Setting oldObj);
        public void SyncExcludedFields(Setting oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }   
    }
    
    #region Enum 'SettingEnum'
    public enum SettingEnum : int
    {
        [SettingInfo(DataTypeEnum.String, BoolString.Yes)]
        ErrorEmail = 1,

        [SettingInfo(DataTypeEnum.Int, BoolString.Yes)]
        WebUserId = 2
    }
    #endregion
}