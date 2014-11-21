using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;
using GlacialArchiving.Web.Common;

namespace GlacialArchiving.Web.Controllers
{
    /// <summary>
    ///     Home page for the site
    /// </summary>
    public partial class HomeController : BaseController
    {
        /// <summary>
        ///     The default view for the home page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
		    if (Request.Url.Authority.Contains("localhost"))
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("DevLogin"))
                    return RedirectToAction("Login","Account");

            ViewBag.Module = "Home";
            return View();
        }
        /// <summary>
        ///     View shown whenever an unhandled error occurs
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            if (Request.Url.Authority.Contains("localhost") && Session["PageError"] != null)
                ViewBag.PageError = Session["PageError"].ToString().Replace("\n", "<br />");
            return View();
        }
    }
}
