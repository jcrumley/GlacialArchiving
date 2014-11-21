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
    [ClassName("Membership", "Memberships")]
	[ClassDisplayName("Membership", "Memberships")]
    [Table("Membership", Schema="dbo")]
    public partial class Membership : BaseObject
    {
        #region Property 'MembershipId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MembershipId { get; set; }
        #endregion
        #region Property 'MemberId'
        [ForeignKey("Member")]
        [Display(Name = "Member")]
        public int MemberId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual User Member { get; set; }
        #endregion
        #region Property 'GroupId'
        [ForeignKey("Group")]
        [Display(Name = "Group")]
        public int GroupId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual UserGroup Group { get; set; }
        #endregion
        
        public Membership() { }
        
        partial void BeforeSyncExcludedFields(Membership oldObj);
        public void SyncExcludedFields(Membership oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}