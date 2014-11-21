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
    [ClassName("fred", "freds")]
	[ClassDisplayName("fred", "freds")]
    [Table("fred", Schema="dbo")]
    public partial class fred : Trackable
    {
        #region Property 'fredId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fredId { get; set; }
        #endregion
        #region Property 'foo'
        [ControlEditor(ControlType.Int)]
        [Display(Name = "foo")]
        [DisplayFormat(DataFormatString="n0")]
        public int foo { get; set; }
        #endregion
        #region Property 'bar'
        [ControlEditor(ControlType.TextBox)]
        [Display(Name = "bar")]
        [Required]
        [MaxLength(50)]
        public string bar { get; set; }
        #endregion
        
        public fred() { }
        
        partial void BeforeSyncExcludedFields(fred oldObj);
        public void SyncExcludedFields(fred oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}