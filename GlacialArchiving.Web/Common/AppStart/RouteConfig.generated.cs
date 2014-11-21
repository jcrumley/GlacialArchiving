using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.Web.Common
{
    /// <summary>
    ///     Defines base level and area level routes for site
    /// </summary>
    public partial class RouteConfig
    {
        /// <summary>
        ///     Creates the base level mvc routes
        /// </summary>
        /// <param name="routes">The route collection to add new routes to</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);
        
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");            
            AreaRegistration.RegisterAllAreas();
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "GlacialArchiving.Web.Controllers" }
            );
        }
    }
}