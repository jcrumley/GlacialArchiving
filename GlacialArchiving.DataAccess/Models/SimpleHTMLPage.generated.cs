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
    [ClassName("SimpleHTMLPage", "SimpleHTMLPages")]
	[ClassDisplayName("Simple HTML Page", "Simple HTML Pages")]
    [Table("SimpleHTMLPage", Schema="dbo")]
    public partial class SimpleHTMLPage : Trackable
    {
        #region Property 'SimpleHTMLPageId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SimpleHTMLPageId { get; set; }
        #endregion
        #region Property 'Title'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        #endregion
        #region Property 'BodyHTML'
        [ControlEditor(ControlType.HTML, 5)]
        [Display(Name = "Body HTML")]
        [Required]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string BodyHTML { get; set; }
        #endregion
        
        public SimpleHTMLPage() { }
        
        partial void BeforeSyncExcludedFields(SimpleHTMLPage oldObj);
        public void SyncExcludedFields(SimpleHTMLPage oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}