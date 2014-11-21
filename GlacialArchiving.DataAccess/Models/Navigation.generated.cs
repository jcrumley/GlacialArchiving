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
    [ClassName("Navigation", "Navigations")]
	[ClassDisplayName("Navigation", "Navigations")]
    [Table("Navigation", Schema="nav")]
    public partial class Navigation : BaseObject
    {
        #region Property 'NavigationId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NavigationId { get; set; }
        #endregion
        #region Property 'Name'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        #endregion
        #region Property 'InConstruction'
        [ControlEditor(ControlType.Bool)]
        public bool InConstruction { get; set; }
        #endregion
        #region Property 'SystemGenerated'
        [ControlEditor(ControlType.Bool)]
        [Display(Name = "System Generated")]
        public bool SystemGenerated { get; set; }
        #endregion
        #region Property 'NavItems'
        [InverseProperty("TopLevel")]
        [JsonIgnore]
        public virtual IList<NavItem> NavItems { get; set; }
        #endregion
        
        public Navigation() { }
        
        partial void BeforeSyncExcludedFields(Navigation oldObj);
        public void SyncExcludedFields(Navigation oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}