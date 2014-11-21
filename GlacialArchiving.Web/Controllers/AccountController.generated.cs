using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using TBADev.MVC.MVCHelpers;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;
using GlacialArchiving.Web.Common;

namespace GlacialArchiving.Web.Controllers
{
    /// <summary>
    ///     The controller that handles logins and forgot password
    /// </summary>
    public partial class AccountController : SecureController
    {
        #region Section 'Actions - Login'
        /// <summary>
        ///     Takes the user to the login view
        /// </summary>
        /// <param name="returnUrl">The url to post to</param>
        /// <returns>The view associated with the login</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
			if (Request.Url.Authority.Contains("localhost"))
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("DevLogin"))
                {
                    string userName = System.Configuration.ConfigurationManager.AppSettings["DevLogin"];
                    string password = System.Configuration.ConfigurationManager.AppSettings["DevPassword"];
                    if (WebSecurity.Login(userName, password, persistCookie: false))
                        return RedirectToLocal(returnUrl);
                }
            }
            
			ViewBag.ReturnUrl = returnUrl;
            string user = WebSecurity.CurrentUserName;
            return View(new LoginModel { UserName = user, RememberMe = !string.IsNullOrWhiteSpace(user) ? "on" : "" });
        }  // GET: /Account/Login

        /// <summary>
        ///     Handles the submission of the login form
        /// </summary>
        /// <param name="model">The login data submitted</param>
        /// <param name="returnUrl">The url to navigate to on a successful login</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMeValue))
                return RedirectToLocal(returnUrl);

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }  // POST: /Account/Login
        #endregion
        #region Section 'Actions - Forgot Password'
        /// <summary>
        ///     Handles the submission of the forgot password form
        /// </summary>
        /// <param name="model">The submitted data</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public void ForgotPassword(LoginModel model)
        {
            User u = work.UserRepository.GetByEmailAddress(model.UserName);
            if (u != null)
            {
                u.ResetPassword(true, work.SettingRepository.WebUserId);
                work.Save();
            }
        }  // POST: /Account/ForgotPassword
        #endregion
        #region Section 'Actions - LogOff'
        /// <summary>
        ///     Logs the current user out of the system
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Login", "Account");
        }  // POST: /Account/LogOff
        /// <summary>
        ///     Logs the current user out of the system
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff(LoggedInUser user)
        {
            WebSecurity.Logout();

            return RedirectToAction("Login", "Account");
        }  // GET: /Account/LogOff
        #endregion
        #region Section 'Actions - Navigations'
        /// <summary>
        ///     Toggles the navigation setting on collapsed
        /// </summary>
        public void ToggleCollapse(LoggedInUser user)
        {
            if (this.UserDetails.NavigationMode == NavigationDisplayMode.Collapsed)
                this.UserDetails.NavigationMode = NavigationDisplayMode.Expanded;
            else
                this.UserDetails.NavigationMode = NavigationDisplayMode.Collapsed;
            work.Save();
        }
        /// <summary>
        ///     Toggles the navigation setting on minified
        /// </summary>
        public void ToggleMinify(LoggedInUser user)
        {
            if (this.UserDetails.NavigationMode == NavigationDisplayMode.Minified)
                this.UserDetails.NavigationMode = NavigationDisplayMode.Expanded;
            else
                this.UserDetails.NavigationMode = NavigationDisplayMode.Minified;
            work.Save();
        }
        #endregion
        
        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Dashboard", new { area = "" });
        }
        #endregion
    }
}