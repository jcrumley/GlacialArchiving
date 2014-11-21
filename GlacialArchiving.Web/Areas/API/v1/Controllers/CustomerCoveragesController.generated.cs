using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;
using GlacialArchiving.Web.Common;

namespace GlacialArchiving.Web.Areas.API.v1.Controllers
{
    /// <summary>
    ///     Creates the interface via WebAPI for CustomerCoverages
    /// </summary>
    public partial class CustomerCoveragesController : SecureApiController
    {
        /// <summary>
        ///     Query the listing of CustomerCoverages in the system
        /// </summary>
        /// <param name="user">The current logged in user</param>
        /// <returns>Listing of all CustomerCoverages in the system that the logged in user can see</returns>
        public IEnumerable<CustomerCoverage> Get([ModelBinder] LoggedInUser user)
        {
            this.log.Info("GetAll", "Start of function");
            IQueryable<CustomerCoverage> collection = work.CustomerCoverageRepository.All;
            this.OnGetAll(collection, user);
            this.log.Info("GetAll", "Returning data");
            return collection;
        }
        partial void OnGetAll(IQueryable<CustomerCoverage> collection, LoggedInUser user);
        
        /// <summary>
        ///     Returns the CustomerCoverage record specified by the id
        /// </summary>
        /// <param name="id">The id of the record to return</param>
        /// <param name="user">The current logged in user</param>
        /// <returns></returns>
        public CustomerCoverage Get(int id, [ModelBinder] LoggedInUser user)
        {
            this.log.Info("GetById", "Start of function");
            CustomerCoverage obj = work.CustomerCoverageRepository.Find(id);
            this.OnGetById(obj, user);
            this.log.Info("GetById", "Returning data");
            return obj;
        }
        partial void OnGetById(CustomerCoverage obj, LoggedInUser user);
        
        /// <summary>
        ///     Creates or updates a CustomerCoverage record in the system
        /// </summary>
        /// <param name="obj">The object to create or update</param>
        /// <param name="user">The current logged in user</param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody] CustomerCoverage obj, [ModelBinder] LoggedInUser user)
        {
            this.log.Info("Post", "Start of function");
            if (ModelState.IsValid)
            {
                this.OnBeforeSaveUpdate(obj, user);
                work.CustomerCoverageRepository.InsertOrUpdate(obj, user.User);
                work.Save();
                this.OnAfterSaveUpdate(obj, user);
                this.log.Info("Post", "Object saved to DB");
                
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                this.log.Info("Post", "Unable to save object");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        partial void OnBeforeSaveUpdate(CustomerCoverage obj, LoggedInUser user);
        partial void OnAfterSaveUpdate(CustomerCoverage obj, LoggedInUser user);
        
        /// <summary>
        ///     Deletes a CustomerCoverage  record from the system
        /// </summary>
        /// <param name="id">The id to delete from the system</param>
        /// <param name="user">The current logged in user</param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id, [ModelBinder] LoggedInUser user)
        {
            this.log.Info("Delete", "Start of function");
            try
            {
                this.OnBeforeDelete(id, user);
                work.CustomerCoverageRepository.Delete(id);
                work.Save();
                this.OnAfterDelete(id, user);
                this.log.Info("Delete", "Object deleted from DB");
                
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                this.log.Info("Delete", "Unable to delete object");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        partial void OnBeforeDelete(int objId, LoggedInUser user);
        partial void OnAfterDelete(int objId, LoggedInUser user);
    }
}

