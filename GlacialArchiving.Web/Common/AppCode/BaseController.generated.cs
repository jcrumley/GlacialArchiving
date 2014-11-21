using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBADev.MVC.Logging;
using TBADev.MVC.MVCHelpers;
using TBADev.MVC.Tools;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.Web.Common
{
    /// <summary>
    ///     Base controller for mvc controllers
    /// </summary>
    public partial class BaseController : Controller
    {
        #region Property 'log'
        private PerformanceLogger m_log = null;
        /// <summary>
        ///     Write message out to log4net for this controller
        /// </summary>
        protected PerformanceLogger log
        {
            get
            {
                if (m_log == null)
                {
                    string pageType = this.GetType().Namespace + "." + this.GetType().Name;
                    m_log = new PerformanceLogger(pageType);
                }
                return m_log;
            }
        }
        #endregion
        #region Property 'work'
        /// <summary>
        ///     Unit of work to be used for calls to/from database
        /// </summary>
        protected UnitOfWork work = new UnitOfWork();
        #endregion
        
        partial void OnLoad();

        public BaseController()
        {
            this.OnLoad();
        }
        
        /// <summary>
        ///     Determines the css classes for the page based on the logged in user
        /// </summary>
        public static string PageConfigurationCssClasses
        {
            get
            {
                string cssClasses = string.Empty;
                string email = System.Web.HttpContext.Current.User.Identity.Name;
                if (!string.IsNullOrWhiteSpace(email))
                {
                    using (UnitOfWork work = new UnitOfWork())
                    {
                        User u = new LoggedInUser(work, email).User;
						if (u != null)
                        	cssClasses = PageConfiguration.DetermineBodyCss(LayoutTemplate.SmartAdmin, u.NavigationMode, u.PageLayout, u.PageTheme);
                    }
                }
                return cssClasses;
            }
        }
        
        /// <summary>
        ///     Determines the navigation based on the current user
        /// </summary>
        /// <param name="user">The current logged in user</param>
        /// <returns></returns>
        public ActionResult Navigation(LoggedInUser user)
        {
            if (user != null)
            {
                //Note this should idealy come from the user or user group or something to define which menu structure the user gets
                ViewBag.CurrentNavigation = this.getNavigation(user);
                return PartialView("Navigation-Secure");
            }
            else
                return PartialView("Navigation-Public");
        }
		
		/// <summary>
        ///     Determines the navigation based on the current user and the specific navigation requested
        /// </summary>
        /// <param name="id">The name to identify the navigation partial view grouping to return</param>
        /// <param name="user">The current logged in user</param>
        /// <returns></returns>
        public ActionResult NavigationNamed(string id, LoggedInUser user)
        {
            if (user != null)
            {
                //Note this should idealy come from the user or user group or something to define which menu structure the user gets
                ViewBag.CurrentNavigation = this.getNavigation(user);
                return PartialView("Navigation-Secure-" + id);
            }
            else
                return PartialView("Navigation-Public");
        }
    }
}