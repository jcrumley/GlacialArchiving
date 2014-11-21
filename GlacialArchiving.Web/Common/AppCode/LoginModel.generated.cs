using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.Web.Common
{
    /// <summary>
    ///     Model used for login and forgot password pages
    /// </summary>
    public partial class LoginModel
    {
        /// <summary>
        ///     The supplied user name from the UI
        /// </summary>
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        /// <summary>
        ///     The supplied password from the UI
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     Whether the user name should remember for future visits
        /// </summary>
        [Display(Name = "Remember me?")]
        public string RememberMe { get; set; }
        public bool RememberMeValue { get { return string.Equals(this.RememberMe, "on", StringComparison.CurrentCultureIgnoreCase); } }
        
        /// <summary>
        ///     Message to use for the forgot password
        /// </summary>
        public string ForgotPasswordMessage { get; set; }
    }
}