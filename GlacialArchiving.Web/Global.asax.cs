using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using GlacialArchiving.Web.Common;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;
using TBADev.MVC.Entity;
using TBADev.MVC.Logging;
using TBADev.MVC.Security;
using TBADev.MVC.Tools;

namespace GlacialArchiving.Web
{
    /// <summary>
    ///     Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    ///     visit http://go.microsoft.com/?LinkId=9394801
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        private User GetCurrentUser()
        {
            User m_user = null;
            try
            {
                if (HttpContext.Current.User != null &&
                    HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    using (UnitOfWork work = new UnitOfWork())
                    {
                        LoggedInUser user = new LoggedInUser(work, this.User.Identity.Name);
                        m_user = user.User;
                    }
                }
            }
            catch { }
            return m_user;
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Start(object sender, EventArgs e)
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AuthConfig.RegisterAuth();

            ModelBinders.Binders.Add(typeof(LoggedInUser), new LoggedInUserBinder());

            log4net.Config.XmlConfigurator.Configure();
            LogManager.GetLogger("WebAdmin").Info("Application Starting");

            //allow special magic routing
            GlobalConfiguration.Configuration.EnsureInitialized();
        }

        private DateTime m_StartTime;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            m_StartTime = DateTime.Now;
            
            string requestingURL = Request.Url.PathAndQuery;
            if (Utilities.UrlVersioningDecode(ref requestingURL))
                HttpContext.Current.RewritePath(requestingURL, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            this.LogWebRequest();
        }
        private void LogWebRequest()
        {
            ActivityLogger.LogWebRequest(Request, m_StartTime, this.GetCurrentUser());
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            User user = this.GetCurrentUser();
            string subject = string.Empty, body = string.Empty, minBody = string.Empty;
            bool result = Utilities.ProcessWebError(user, out subject, out body, out minBody);
            if (!result) return;

            try
            {
                ILog logger = LogManager.GetLogger("WebError");
                logger.Error(string.Format("{0},{1}", (user != null ? user.NameFull : string.Empty), minBody));
            }
            catch { }

            try
            {
                string email = string.Empty;
                if (Request.Url.Authority.Contains("localhost") && user != null)
                    email = user.EmailAddress;
                else 
                    email = new SettingRepository().ErrorEmail;

                if (!string.IsNullOrWhiteSpace(email))
                {
                    EmailBasicItem eh = new EmailBasicItem(email, subject, body);
                    Utilities.SendEmail(eh);
                }
            }
            catch { }
            
            Server.ClearError();
            if (Request.Url.Authority.Contains("localhost"))
                Session["PageError"] = body;
            Server.TransferRequest("/Home/Error", true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_End(object sender, EventArgs e)
        {
            ILog logger = LogManager.GetLogger("WebAdmin");
            logger.Info("Application Ending");
        }
    }
}