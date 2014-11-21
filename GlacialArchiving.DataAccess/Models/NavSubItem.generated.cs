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
    [ClassName("NavSubItem", "NavSubItems")]
	[ClassDisplayName("Nav Sub Item", "Nav Sub Items")]
    [Table("NavSubItem", Schema="nav")]
    public partial class NavSubItem : BaseObject
    {
        #region Property 'NavSubItemId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NavSubItemId { get; set; }
        #endregion
        #region Property 'NavItemId'
        [ForeignKey("NavItem")]
        [Display(Name = "NavItem")]
        public int NavItemId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual NavItem NavItem { get; set; }
        #endregion
        #region Property 'Title'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
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
        
        public NavSubItem() { }
        
        partial void BeforeSyncExcludedFields(NavSubItem oldObj);
        public void SyncExcludedFields(NavSubItem oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}