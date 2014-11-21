using System.Web.Mvc;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;
using TBADev.MVC.Tools;

namespace GlacialArchiving.Web.Areas.Core
{
    /// <summary>
    ///     The definition for the Common area
    /// </summary>
    public partial class CoreAreaRegistration : AreaRegistration
    {
        /// <summary>
        ///     The name of the area
        /// </summary>
        public override string AreaName
        {
            get { return "Core"; }
        }
        
        /// <summary>
        ///     Registers the area and defines the route
        /// </summary>
        /// <param name="context"></param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Core_default",
                "Core/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
