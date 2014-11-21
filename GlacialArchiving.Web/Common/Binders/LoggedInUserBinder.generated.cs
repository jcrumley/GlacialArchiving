using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.Web.Common
{
    /// <summary>
    ///     Defines the binder to use for the LoggedInUser
    /// </summary>
    public partial class LoggedInUserBinder : System.Web.Mvc.IModelBinder, System.Web.Http.ModelBinding.IModelBinder
    {
        /// <summary>
        ///     Default constructor
        /// </summary>
        public LoggedInUserBinder() { }

        /// <summary>
        ///     Function that implements the binding for System.Web.Mvc.IModelBinder (mvc controllers)
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public object BindModel(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            if (controllerContext.HttpContext != null &&
                !controllerContext.HttpContext.User.Identity.IsAuthenticated)
                return null;

            LoggedInUser user = null;
            using (UnitOfWork work = new UnitOfWork())
            {
                user = new LoggedInUser(work, controllerContext.HttpContext.User.Identity.Name);
            }
            return user;
        }

        /// <summary>
        ///     Function that implements the binding for System.Web.Http.ModelBinding.IModelBinder (web api controllers)
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public bool BindModel(System.Web.Http.Controllers.HttpActionContext actionContext, System.Web.Http.ModelBinding.ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(LoggedInUser))
                return false;

            System.Web.Http.ApiController controller = (actionContext.ControllerContext.Controller as System.Web.Http.ApiController);
            if (controller.User.Identity.IsAuthenticated)
            {
                using (UnitOfWork work = new UnitOfWork())
                {
                    bindingContext.Model = new LoggedInUser(work, controller.User.Identity.Name);
                }
                return true;
            }

            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Cannot convert value to LoggedInUser");
            return false;
        }
    }
}