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
    ///     Creates the interface via WebAPI for Memberships
    /// </summary>
    public partial class MembershipsController : SecureApiController
    {
        /// <summary>
        ///     Query the listing of Memberships in the system
        /// </summary>
        /// <param name="user">The current logged in user</param>
        /// <returns>Listing of all Memberships in the system that the logged in user can see</returns>
        public IEnumerable<Membership> Get([ModelBinder] LoggedInUser user)
        {
            this.log.Info("GetAll", "Start of function");
            IQueryable<Membership> collection = work.MembershipRepository.All;
            this.OnGetAll(collection, user);
            this.log.Info("GetAll", "Returning data");
            return collection;
        }
        partial void OnGetAll(IQueryable<Membership> collection, LoggedInUser user);
        
        /// <summary>
        ///     Returns the Membership record specified by the id
        /// </summary>
        /// <param name="id">The id of the record to return</param>
        /// <param name="user">The current logged in user</param>
        /// <returns></returns>
        public Membership Get(int id, [ModelBinder] LoggedInUser user)
        {
            this.log.Info("GetById", "Start of function");
            Membership obj = work.MembershipRepository.Find(id);
            this.OnGetById(obj, user);
            this.log.Info("GetById", "Returning data");
            return obj;
        }
        partial void OnGetById(Membership obj, LoggedInUser user);
        
        /// <summary>
        ///     Creates or updates a Membership record in the system
        /// </summary>
        /// <param name="obj">The object to create or update</param>
        /// <param name="user">The current logged in user</param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody] Membership obj, [ModelBinder] LoggedInUser user)
        {
            this.log.Info("Post", "Start of function");
            if (ModelState.IsValid)
            {
                this.OnBeforeSaveUpdate(obj, user);
                work.MembershipRepository.InsertOrUpdate(obj, user.User);
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
        partial void OnBeforeSaveUpdate(Membership obj, LoggedInUser user);
        partial void OnAfterSaveUpdate(Membership obj, LoggedInUser user);
        
        /// <summary>
        ///     Deletes a Membership  record from the system
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
                work.MembershipRepository.Delete(id);
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

