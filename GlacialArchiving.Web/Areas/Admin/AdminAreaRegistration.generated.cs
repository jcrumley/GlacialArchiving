using System.Web.Mvc;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;
using TBADev.MVC.Tools;

namespace GlacialArchiving.Web.Areas.Admin
{
    /// <summary>
    ///     The definition for the Admin area
    /// </summary>
    public partial class AdminAreaRegistration : AreaRegistration
    {
        /// <summary>
        ///     The name of the area
        /// </summary>
        public override string AreaName
        {
            get { return "Admin"; }
        }
        
        /// <summary>
        ///     Registers the area and defines the route
        /// </summary>
        /// <param name="context"></param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
