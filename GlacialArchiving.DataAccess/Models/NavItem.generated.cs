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
    [DisplayProperty("Title")]
    [ClassName("NavItem", "NavItems")]
	[ClassDisplayName("Nav Item", "Nav Items")]
    [Table("NavItem", Schema="nav")]
    public partial class NavItem : BaseObject
    {
        #region Property 'NavItemId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NavItemId { get; set; }
        #endregion
        #region Property 'Title'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        #endregion
        #region Property 'TopLevelId'
        [ForeignKey("TopLevel")]
        [Display(Name = "Top Level")]
        public int TopLevelId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual Navigation TopLevel { get; set; }
        #endregion
        #region Property 'URL'
        [ControlEditor(ControlType.TextBox)]
        [MaxLength(100)]
        public string URL { get; set; }
        #endregion
        #region Property 'Sequence'
        [ControlEditor(ControlType.Int)]
        public int Sequence { get; set; }
        #endregion
        #region Property 'IconId'
        [ForeignKey("Icon")]
        [Display(Name = "Icon")]
        public int? IconId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual NavIcon Icon { get; set; }
        #endregion
        #region Property 'SystemGenerated'
        [ControlEditor(ControlType.Bool)]
        [Display(Name = "System Generated")]
        public bool SystemGenerated { get; set; }
        #endregion
        #region Property 'NavSubItems'
        [InverseProperty("NavItem")]
        [JsonIgnore]
        public virtual IList<NavSubItem> NavSubItems { get; set; }
        #endregion
        
        public NavItem() { }
        
        partial void BeforeSyncExcludedFields(NavItem oldObj);
        public void SyncExcludedFields(NavItem oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}