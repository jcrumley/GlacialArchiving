using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using TBADev.MVC.Security;
using TBADev.MVC.Tools;
using TBADev.MVC.Logging;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.Web.Common
{
    /// <summary>
    ///     Lowest level page inheritence for public viewable pages
    /// </summary>
    [System.Web.Script.Services.ScriptService]
    public partial class BasePage : Page
    {
        #region Property 'log'
        private PerformanceLogger m_log = null;
        /// <summary>
        ///     Write message out to log4net for this page
        /// </summary>
        protected PerformanceLogger log
        {
            get
            {
                if (m_log == null)
                {
                    string pageType = this.GetType().BaseType.Namespace + "." + this.GetType().BaseType.Name;
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

        /// <summary>
        ///     Constructor for the BasePage object
        /// </summary>
        public BasePage()
            : base()
        {
            this.Load += BasePage_Load;
            this.LoadComplete += BasePage_LoadComplete;
            this.PreRender += BasePage_PreRender;
            this.Unload += BasePage_Unload;
        }

        partial void OnLoad();
        /// <summary>
        ///     Function that gets called on page load
        /// </summary>
        protected void BasePage_Load(object sender, EventArgs e)
        {
            log.Info("BasePage", "Load");
            this.OnLoad();
        }
        /// <summary>
        ///     Function that gets called on page load complete
        /// </summary>
        protected void BasePage_LoadComplete(object sender, EventArgs e)
        {
            log.Info("BasePage", "LoadComplete");
        }
        /// <summary>
        ///     Function that gets called on page render
        /// </summary>
        protected void BasePage_PreRender(object sender, EventArgs e)
        {
            log.Info("BasePage", "PreRender");
        }
        /// <summary>
        ///     Function that gets called on page unload
        /// </summary>
        protected void BasePage_Unload(object sender, EventArgs e)
        {
            log.Info("BasePage", "Unload");
        }
    }
}