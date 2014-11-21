using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBADev.MVC.DataAttributes;
using TBADev.MVC.Security;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.Web.Common
{
    /// <summary>
    ///     The controller that is used for secure mvc controllers
    /// </summary>
    [Authorize]
    [Breadcrumb]
    public partial class SecureController : BaseController
    {
        #region Property 'UserDetails'
        private User m_UserDetails = null;
        /// <summary>
        ///     The user currently logged in
        /// </summary>
        public User UserDetails
        {
            get
            {
                if (m_UserDetails == null)
                {
                    try
                    {
                        if (!this.User.Identity.IsAuthenticated)
                            throw new Exception("SecurePage.UserDetails : No User is authenicated");

                        LoggedInUser loggedIn = new LoggedInUser(work, User.Identity.Name);
                        if (loggedIn.User == null)
                            throw new Exception("SecurePage.UserDetails : No User found for email = " + User.Identity.Name);
                        m_UserDetails = loggedIn.User;
                    }
                    catch { }
                }
                return m_UserDetails;
            }
        }
        #endregion
        #region Static Property 'UserName'
        public static string UserName
        {
            get
            {
                string userName = string.Empty;
                try
                {
                    if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        using (UnitOfWork work = new UnitOfWork())
                        {
                            LoggedInUser loggedIn = new LoggedInUser(work, System.Web.HttpContext.Current.User.Identity.Name);
                            userName = loggedIn.User.NameFormat(NameMode.FirstLast);
                        }
                    }
                }
                catch { }
                return userName;
            }
        }
        #endregion
        #region Static Property 'IsLoggedIn'
        public static bool IsLoggedIn
        {
            get
            {
                return System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }
        #endregion
        
        partial void OnLoad();

        public SecureController()
        {
            this.OnLoad();
        }
    
        /// <summary>
        ///     Looks at the submitted model and builds the errors listing
        /// </summary>
        protected virtual void SetErrors()
        {
            List<string> errors = new List<string>();
            foreach (string key in ModelState.Keys)
            {
                if (ModelState[key].Errors.Count > 0)
                {
                    for (int i = 0; i < ModelState[key].Errors.Count; i++)
                        errors.Add(ModelState[key].Errors[0].ErrorMessage);
                }
            }

            if (errors.Count > 0)
                ViewBag.Errors = errors;
            else
                ViewBag.Errors = null;
        }
    }
}