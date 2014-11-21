using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlacialArchiving.Web.Common;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;
using TBADev.MVC.Entity;
using TBADev.MVC.Tools;

namespace GlacialArchiving.Web.Controllers
{   
    /// <summary>
    ///     First page the user goes to on login
    /// </summary>
    public partial class DashboardController : SecureController
    {
        /// <summary>
        ///     The default view for the dashboard
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }  // GET: /Admin/Dashboard/
    }
}