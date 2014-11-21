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
    [ClassName("UserGroup", "UserGroups")]
	[ClassDisplayName("User Group", "User Groups")]
    [Table("UserGroup", Schema="dbo")]
    public partial class UserGroup : Trackable
    {
        #region Property 'UserGroupId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserGroupId { get; set; }
        #endregion
        #region Property 'Name'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        #endregion
        #region Property 'Memberships'
        [InverseProperty("Group")]
        [JsonIgnore]
        public virtual IList<Membership> Memberships { get; set; }
        #endregion
        
        public UserGroup() { }
        
        partial void BeforeSyncExcludedFields(UserGroup oldObj);
        public void SyncExcludedFields(UserGroup oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}