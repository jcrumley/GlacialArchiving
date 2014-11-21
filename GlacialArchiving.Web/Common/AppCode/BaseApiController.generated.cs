using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TBADev.MVC.Tools;
using TBADev.MVC.Logging;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.Web.Common
{
    /// <summary>
    ///     The base controller for the web api controllers
    /// </summary>
    public partial class BaseApiController : ApiController
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

        public BaseApiController()
        {
            this.OnLoad();
        }
    }
}