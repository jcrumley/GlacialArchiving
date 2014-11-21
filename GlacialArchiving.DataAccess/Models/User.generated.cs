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
using Newtonsoft.Json;
using TBADev.MVC.DataAttributes;
using TBADev.MVC.Entity;
using TBADev.MVC.Entity.Generation;
using TBADev.MVC.MVCHelpers;
using TBADev.MVC.Security;
using TBADev.MVC.Tools;
using GlacialArchiving.DataAccess.Base;

namespace GlacialArchiving.DataAccess.Models
{
    [DisplayProperty("NameFull")]
    [ClassName("User", "Users")]
    [Table("User", Schema="dbo")]
    public partial class User : Trackable, ITBADevUser
    {
        #region Section 'Standard Properties from ITBADevUser'
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(100)]
        public string NameFirst { get; set; }
        [Display(Name = "Last Name")]
        [MaxLength(100)]
        public string NameLast { get; set; }
        [NotMapped]
        [NoDisplay]
        public string NameFull
        {
            get { return this.NameFormat(NameMode.FirstLast); }
        }
        [Display(Name = "Email Address")]
        [MaxLength(200)]
        public string EmailAddress { get; set; }
        [NoDisplay]
        [MaxLength(100)]
        [JsonIgnore]
        public string Password { get; set; }
        
        [Display(Name = "Last Login Date")]
        [ControlEditor(ControlType.DateTime)]
        [DataType(DataType.DateTime)]
        public DateTime? LastLoginDate { get; set; }
        [Display(Name = "Last Lockout Date")]
        [ControlEditor(ControlType.DateTime)]
        [DataType(DataType.DateTime)]
        public DateTime? LastLockoutDate { get; set; }
        [Display(Name = "Last Password Changed Date")]
        [ControlEditor(ControlType.DateTime)]
        [DataType(DataType.DateTime)]
        public DateTime? LastPasswordChangedDate { get; set; }
        [Display(Name = "Is Verified")]
        public bool IsVerified { get; set; }
        [Display(Name = "Is Locked Out")]
        public bool IsLockedOut { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [NotMapped]
        [NoDisplay]
        public virtual bool Enabled
        {
            get { return this.IsActive && this.IsVerified && !this.IsLockedOut; }
        }
        
        [NoDisplay]
        public NavigationDisplayMode NavigationMode { get; set; }
        [NoDisplay]
        public PageLayoutMode PageLayout { get; set; }
        [NoDisplay]
        [MaxLength(200)]
        public string PageTheme { get; set; }        
        #endregion
        
        // Custom Properties Defined
        #region Property 'Address'
        [ControlEditor(ControlType.TextArea, 5)]
        [MaxLength(50)]
        public string Address { get; set; }
        #endregion
        #region Property 'City'
        [ControlEditor(ControlType.TextBox)]
        [MaxLength(200)]
        public string City { get; set; }
        #endregion
        #region Property 'State'
        [ControlEditor(ControlType.TextBox)]
        [MaxLength(2)]
        public string State { get; set; }
        #endregion
        #region Property 'Zipcode'
        [ControlEditor(ControlType.TextBox)]
        [MaxLength(10)]
        public string Zipcode { get; set; }
        #endregion
        #region Property 'Memberships'
        [InverseProperty("Member")]
        [JsonIgnore]
        public virtual IList<Membership> Memberships { get; set; }
        #endregion
        
        public User() { }
        
        partial void BeforeSyncExcludedFields(User oldObj);
        public void SyncExcludedFields(User oldObj)
        {
            this.BeforeSyncExcludedFields(oldObj);
            this.Password = oldObj.Password;
            this.NavigationMode = oldObj.NavigationMode;
            this.PageLayout = oldObj.PageLayout;
            this.PageTheme = oldObj.PageTheme;
        }
        
        public override string ToString()
        {
            return NameFormat(NameMode.LastFirst);
        }
        public string NameFormat(NameMode mode)
        {
            switch (mode)
            {
                case NameMode.FirstLast:
                case NameMode.FirstMiddleLast:
                    return this.NameFirst + " " + this.NameLast;
                case NameMode.LastFirst:
                case NameMode.LastFirstMiddle:
                    return this.NameLast + ", " + this.NameFirst;
                case NameMode.Initials:
                    return this.NameFirst[0].ToString() + this.NameLast[0].ToString();
            }
            return null;
        }
        public void ResetPassword(bool sendEmail, int actionUserId)
        {
            string password = Utilities.CreateRandomString(8).ToUpper();
            this.Password = TBADevMembershipProvider.EncodePassword(password);
            this.LastPasswordChangedDate = DateTime.Now;
            this.ModifiedBy_UserId = actionUserId;
            this.ModifiedOn = DateTime.Now;

            if (sendEmail)
            {
                EmailBasicItem item = new EmailBasicItem(this.EmailAddress, "Password has changed", "Your new password is " + password);
                Utilities.SendEmail(item);
            }
        }
        public PasswordChangeResults ChangePassword(PasswordChangeSettings settings, int actionUserId)
        {
            if (settings.NewPassword1.Trim() != settings.NewPassword2.Trim())
                return new PasswordChangeResults("New passwords do not match");

            string encodedOldPassword = TBADevMembershipProvider.EncodePassword(settings.OldPassword);
            if (this.Password != encodedOldPassword)
                return new PasswordChangeResults("Current password does not match");

            this.Password = TBADevMembershipProvider.EncodePassword(settings.NewPassword1);
            this.LastPasswordChangedDate = DateTime.Now;
            this.ModifiedBy_UserId = actionUserId;
            this.ModifiedOn = DateTime.Now;
            return new PasswordChangeResults();
        }
        public bool Validate(string[] AllowGroups, string[] DenyGroups)
        {
            //TODO
            return true;
        }
    }
}