using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ModelBinding.Binders;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace GlacialArchiving.Web.Common
{
    /// <summary>
    ///     Controls the web api registration
    /// </summary>
    public static partial class WebApiConfig
    {
        /// <summary>
        ///     Defines the web api routes and adds them to the web site's configuration
        /// </summary>
        /// <param name="config">The web site's route configuration</param>
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{version}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, version = "v1" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiWithAction",
                routeTemplate: "api/{version}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional, version = "v1" }
            );

            config.Services.Replace(typeof(IHttpControllerSelector), new WebApiControllerSelector(config));

            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.None;
           
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.XmlFormatter.UseXmlSerializer = true;
            //config.EnsureInitialized();
        }
    }
}
