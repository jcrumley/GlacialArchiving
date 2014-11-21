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
    [DisplayProperty("UniqueIdentifier")]
    [ClassName("UploadedItem", "UploadedItems")]
	[ClassDisplayName("Uploaded Item", "Uploaded Items")]
    [Table("UploadedItem", Schema="datastorage")]
    [TrackHistory(TrackHistoryMode.ByProperty)]
    public partial class UploadedItem : Trackable
    {
        #region Property 'UploadedItemId'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UploadedItemId { get; set; }
        #endregion
        #region Property 'ClientId'
        [ForeignKey("Client")]
        [Display(Name = "Client")]
        public int ClientId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual Client Client { get; set; }
        #endregion
        #region Property 'UniqueIdentifier'
        [ControlEditor(ControlType.TextBox)]
        [Display(Name = "Unique Identifier")]
        [Required]
        [MaxLength(100)]
        public string UniqueIdentifier { get; set; }
        #endregion
        #region Property 'PayloadTypeId'
        [ForeignKey("PayloadType")]
        [Display(Name = "Payload Type")]
        public int PayloadTypeId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual PayloadType PayloadType { get; set; }
        #endregion
        #region Property 'PayloadFilename'
        [ControlEditor(ControlType.TextBox)]
        [Display(Name = "Payload Filename")]
        [Required]
        [MaxLength(200)]
        public string PayloadFilename { get; set; }
        #endregion
        #region Property 'PayloadChecksum'
        [ControlEditor(ControlType.TextBox)]
        [Display(Name = "Payload Checksum")]
        [Required]
        [MaxLength(100)]
        public string PayloadChecksum { get; set; }
        #endregion
        #region Property 'PayloadFilesize'
        [ControlEditor(ControlType.TextBox)]
        [Display(Name = "Payload Filesize")]
        [Required]
        [MaxLength(100)]
        public string PayloadFilesize { get; set; }
        #endregion
        #region Property 'TradingDateStart'
        [ControlEditor(ControlType.DateTime)]
        [Display(Name = "Start Trading Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString="MM/dd/yyyy hh:mm:ss tt")]
        public DateTime TradingDateStart { get; set; }
        #endregion
        #region Property 'TradingDateEnd'
        [ControlEditor(ControlType.DateTime)]
        [Display(Name = "End Trading Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString="MM/dd/yyyy hh:mm:ss tt")]
        public DateTime TradingDateEnd { get; set; }
        #endregion
        #region Property 'Description'
        [ControlEditor(ControlType.TextBox)]
        [Required]
        public string Description { get; set; }
        #endregion
        #region Property 'ExpirationDate'
        [ControlEditor(ControlType.DateTime)]
        [Display(Name = "Expiration Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString="MM/dd/yyyy hh:mm:ss tt")]
        public DateTime? ExpirationDate { get; set; }
        #endregion
        #region Property 'GenericField1'
        [ControlEditor(ControlType.TextBox)]
        [Display(Name = "Generic Field 1")]
        [MaxLength(100)]
        public string GenericField1 { get; set; }
        #endregion
        #region Property 'GenericField2'
        [ControlEditor(ControlType.TextBox)]
        [Display(Name = "Generic Field 2")]
        [MaxLength(100)]
        public string GenericField2 { get; set; }
        #endregion
        #region Property 'StatusId'
        [ForeignKey("Status")]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual UploadedItemStatus Status { get; set; }
        #endregion
        #region Property 'Replacements'
        [InverseProperty("UploadedItem")]
        [JsonIgnore]
        public virtual IList<Replacement> Replacements { get; set; }
        #endregion
        #region Property 'CustomerCoverages'
        [InverseProperty("UploadedItem")]
        [JsonIgnore]
        public virtual IList<CustomerCoverage> CustomerCoverages { get; set; }
        #endregion
        #region Property 'TradingUnitCoverages'
        [InverseProperty("UploadedItem")]
        [JsonIgnore]
        public virtual IList<TradingUnitCoverage> TradingUnitCoverages { get; set; }
        #endregion
        #region Property 'DataTags'
        [InverseProperty("UploadedItem")]
        [JsonIgnore]
        public virtual IList<DataTag> DataTags { get; set; }
        #endregion
        #region Property 'GlacierStorages'
        [InverseProperty("UploadedItem")]
        [JsonIgnore]
        public virtual IList<GlacierStorage> GlacierStorages { get; set; }
        #endregion
        #region Property 'IsDeleted'
        [ControlEditor(ControlType.Bool)]
        [Display(Name = "Is Deleted")]
        public virtual bool IsDeleted { get; set; }
        #endregion
        
        public UploadedItem() { }
        
        partial void BeforeSyncExcludedFields(UploadedItem oldObj);
        public void SyncExcludedFields(UploadedItem oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
        }        
    }
}