using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TBADev.MVC.DataAttributes;
using GlacialArchiving.DataAccess.Models;

namespace GlacialArchiving.DataAccess.Base
{
    public abstract partial class Trackable : BaseObject
    {
        #region Property 'CreatedOn'
        [ReadOnly(true)]
        [Display(Name = "Created On")]
        [DoNotTrack]
        public DateTime? CreatedOn { get; set; }
        #endregion
        #region Property 'CreatedBy'
        [ReadOnly(true)]
        [Display(Name = "Created By")]
        [ForeignKey("CreatedBy")]
        [DoNotTrack]
        public int? CreatedBy_UserId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual User CreatedBy { get; set; }
        #endregion
        #region Property 'ModifiedOn'
        [ReadOnly(true)]
        [Display(Name = "Modified On")]
        [DoNotTrack]
        public DateTime? ModifiedOn { get; set; }
        #endregion
        #region Property 'ModifiedBy'
        [ReadOnly(true)]
        [Display(Name = "Modified By")]
        [ForeignKey("ModifiedBy")]
        [DoNotTrack]
        public int? ModifiedBy_UserId { get; set; }
        [NoDisplay]
        [JsonIgnore]
        public virtual User ModifiedBy { get; set; }
        #endregion
    }
}
