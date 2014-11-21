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
    [ClassName("NavIcon", "NavIcons")]
	[ClassDisplayName("Nav Icon", "Nav Icons")]
    [Table("NavIcon", Schema="nav")]
    public partial class NavIcon : BaseObject
    {
        #region Property 'NavIconId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NavIconId { get; set; }
        #endregion
        #region Property 'Name'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        #endregion
        #region Property 'CssClassName'
        [ControlEditor(ControlType.TextBox)]
        [Display(Name = "Css Class")]
        [Required]
        [MaxLength(200)]
        public string CssClassName { get; set; }
        #endregion
        
        public NavIcon() { }
        
        partial void BeforeSyncExcludedFields(NavIcon oldObj);
        public void SyncExcludedFields(NavIcon oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}