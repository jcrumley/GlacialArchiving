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
    [ClassName("Client", "Clients")]
	[ClassDisplayName("Client", "Clients")]
    [Table("Client", Schema="dbo")]
    [TrackHistory(TrackHistoryMode.ByProperty)]
    public partial class Client : Trackable
    {
        #region Property 'ClientId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }
        #endregion
        #region Property 'Name'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        #endregion
        #region Property 'ClientIdTag'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        [MaxLength(20)]
        public string ClientIdTag { get; set; }
        #endregion
        
        public Client() { }
        
        partial void BeforeSyncExcludedFields(Client oldObj);
        public void SyncExcludedFields(Client oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}