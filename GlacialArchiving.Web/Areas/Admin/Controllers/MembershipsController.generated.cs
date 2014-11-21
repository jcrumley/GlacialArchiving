using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TBADev.MVC.Entity;
using TBADev.MVC.MVCHelpers;
using TBADev.MVC.Tools;
using GlacialArchiving.Web.Common;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.Web.Areas.Admin.Controllers
{   
    /// <summary>
    ///     The definition of MembershipsController
    /// </summary>
    public partial class MembershipsController : SecureController
    {
        /// <summary>
        ///     Default constructor for MembershipsController
        /// </summary>
        public MembershipsController() 
        { 
            ViewBag.AllowCreate = true;
            ViewBag.AllowEdit = true;
            ViewBag.AllowDelete = true;
            ViewBag.PageDisplayMode = PageDisplayMode.Inline;
            ViewBag.IsPost = false;
        }
        
        #region Section 'Partial Functions'
        partial void BeforeValidate(Membership obj);
        partial void BeforeCreate(Membership obj);
        partial void AfterCreate(Membership obj);
        partial void BeforeEdit(Membership oldObj, Membership obj);
        partial void AfterEdit(Membership obj);
        partial void BeforeDelete(int id);
        partial void AfterDelete(int id);
        #endregion

        #region Section 'Actions - Index'
        /// <summary>
        ///     Sends the user to the listing page for Memberships
        /// </summary>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult Index(LoggedInUser currentUser)
        {
            this.log.Info("Index", "Start of function");
            ViewBag.PageDisplayMode = PageDisplayMode.Index;
            ViewResult result = View(work.MembershipRepository.AllIncluding(obj => obj.Member, obj => obj.Group));
            this.log.Info("Index", "View created and returning");
            return result;
        }   // GET: /Admin/Memberships/
        #endregion
        #region Section 'Actions - Create'
        /// <summary>
        ///     View to create a new Membership in the system
        /// </summary>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult Create(LoggedInUser currentUser)
        {
            this.log.Info("Create", "Start of function");
            this.LoadDropDowns();
            ViewResult result = View();
            this.log.Info("Create", "View created and returning");
            return result;
        }   // GET: /Admin/Memberships/Create
        /// <summary>
        ///     Submission of a Membership from the UI
        /// </summary>
        /// <param name="obj">The new object to insert into the database</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Membership obj, LoggedInUser currentUser)
        {
            this.log.Info("Create [POST]", "Start of function");
            if (obj.Id == 0)
                ModelState["MembershipId"].Errors.Clear();
                
            this.BeforeValidate(obj);
            if (ModelState.IsValid) 
            {
                this.BeforeCreate(obj);
                work.MembershipRepository.InsertOrUpdate(obj, currentUser.User);
                work.Save();
                TempData["WasCreated"] = true;
                this.AfterCreate(obj);
                this.log.Info("Create [POST]", "Object saved and redirecting");
                return RedirectToAction("Edit", new { id = obj.Id });
            } 
            else
            {
                this.SetErrors();
                this.LoadDropDowns();
                this.log.Info("Create [POST]", "Unable to save object");
                ViewBag.IsPost = true;
                return View("Create", obj);
            }
        }  // POST: /Admin/Memberships/Create
        #endregion
        #region Section 'Actions - Edit'
        /// <summary>
        ///     View to edit an existing Membership record in the system
        /// </summary>
        /// <param name="id">The id of the record to edit</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult Edit(int id, LoggedInUser currentUser)
        {
            this.log.Info("Edit", "Start of function");
            this.LoadDropDowns();
            
            if (TempData["WasCreated"] != null)
                ViewBag.IsPost = true;
            
            ViewResult result = View(work.MembershipRepository.Find(id));
            this.log.Info("Edit", "View created and returning");
            return result;
        }  // GET: /Admin/Memberships/Edit/{id}
        /// <summary>
        ///     Submission of the editing model back from the UI
        /// </summary>
        /// <param name="obj">The edited model</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Membership obj, LoggedInUser currentUser)
        {
            this.log.Info("Edit [POST]", "Start of function");
            this.LoadDropDowns();
            this.BeforeValidate(obj);
            if (ModelState.IsValid) 
            {
                Membership oldObj = work.MembershipRepository.Find(obj.Id);
                obj.SyncExcludedFields(oldObj);
            
                this.BeforeEdit(oldObj, obj);
                work.MembershipRepository.InsertOrUpdate(obj, currentUser.User);
                work.Save();
                obj = work.MembershipRepository.Find(obj.Id); //need to rehydrate the FKs
                this.AfterEdit(obj);
                this.log.Info("Edit [POST]", "Object saved and returning");
            } 
            else 
            {
                this.SetErrors();
                this.log.Info("Edit [POST]", "Unable to save object");
            }
            
            ViewBag.IsPost = true;
            return View("_CreateOrEdit", obj);
        }  // POST: /Admin/Memberships/Edit/{id}
        #endregion
        #region Section 'Actions - Delete'
        /// <summary>
        ///     Deletion view that shows the user the record to be deleted
        /// </summary>
        /// <param name="id">The id of the record to be deleted</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult Delete(int id, LoggedInUser currentUser)
        {
            this.log.Info("Delete", "Start of function");
            ViewResult result = View(work.MembershipRepository.Find(id));
            this.log.Info("Delete", "View created and returning");
            return result;
        }  // GET: /Admin/Memberships/Delete/5
        /// <summary>
        ///     The submission / acceptance of the deletion of the record
        /// </summary>
        /// <param name="id">The record's id that is to be deleted</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, LoggedInUser currentUser)
        {
            this.log.Info("Delete [POST]", "Start of function");
            this.BeforeDelete(id);
            work.MembershipRepository.Delete(id);
            work.Save();
            this.AfterDelete(id);
            this.log.Info("Delete [POST]", "Object has been deleted");
            return RedirectToAction("Index");
        }  // POST: /Admin/Memberships/Delete/{id}
        #endregion
        #region Section 'Actions - Inline'
        /// <summary>
        ///     Retrieve view to create/edit an existing Membership record in the system
        /// </summary>
        /// <param name="id">The id of the record to edit</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult QueryItem(int id, LoggedInUser currentUser)
        {
            this.log.Info("QueryItem", "Start of function");
            this.LoadDropDowns();

            Membership obj = null;
            if (id != 0)
                obj = work.MembershipRepository.Find(id);

            ViewResult result = View("_CreateOrEdit", obj);
            this.log.Info("QueryItem", "View created and returning");
            return result;
        }  // GET: /Admin/Memberships/QueryItem/{id}
        /// <summary>
        ///     Submission of the editing model back from the UI
        /// </summary>
        /// <param name="obj">The edited model</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public MvcHtmlString InsertOrUpdate(Membership obj, LoggedInUser currentUser)
        {
            this.log.Info("Edit [POST]", "Start of function");
            if (obj.Id == 0)
                ModelState["MembershipId"].Errors.Clear();
                
            
            bool isNew = false;
            AjaxResult returnObj;
            this.BeforeValidate(obj);
            if (ModelState.IsValid)
            {
                if (obj.Id != 0)
                {
                    Membership oldObj = work.MembershipRepository.Find(obj.Id);
                    obj.SyncExcludedFields(oldObj);
                    isNew = true;
                    this.BeforeEdit(oldObj, obj);
                }
                else
                    this.BeforeCreate(obj);

                work.MembershipRepository.InsertOrUpdate(obj, currentUser.User);
                work.Save();
                returnObj = new AjaxResult(true);
                
                if (isNew) this.AfterCreate(obj);
                else this.AfterEdit(obj);
                
                this.log.Info("Edit [POST]", "Saved changes to object");
            }
            else
            {
                returnObj = new AjaxResult(false, ModelState);
                this.log.Info("Edit [POST]", "Unable to edit object");
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return MvcHtmlString.Create(serializer.Serialize(returnObj));
        }
        /// <summary>
        ///     Deletion view that shows the user the record to be deleted
        /// </summary>
        /// <param name="id">The id of the record to be deleted</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult DeleteInline(int id, LoggedInUser currentUser)
        {
            this.log.Info("Delete", "Start of function");
            ViewResult result = View("_Display", work.MembershipRepository.Find(id));
            this.log.Info("Delete", "View created and returning");
            return result;
        }  // GET: /Admin/InvoiceDistributions/Delete/5
        /// <summary>
        ///     The submission / acceptance of the deletion of the record
        /// </summary>
        /// <param name="id">The record's id that is to be deleted</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost, ActionName("DeleteInline")]
        public MvcHtmlString DeleteInlineConfirmed(int id, LoggedInUser currentUser)
        {
            this.log.Info("Delete [POST]", "Start of function");
            AjaxResult returnObj = null;
            try
            {
                this.BeforeDelete(id);
                work.MembershipRepository.Delete(id);
                work.Save();
                returnObj = new AjaxResult(true);
                this.AfterDelete(id);
                this.log.Info("Delete [POST]", "Object has been deleted");
            }
            catch(Exception ex)
            {
                returnObj = new AjaxResult(false, ex.Message);
                this.log.Info("Delete [POST]", "Unable to delete object");
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return MvcHtmlString.Create(serializer.Serialize(returnObj));
        }  // POST: /Admin/InvoiceDistributions/Delete/{id}
        #endregion

        #region Section 'Parent Foriegn Keys'
					
        /// <summary>
        ///     Filters the listing down to only show specific Membership records based on the id provided for the Member record to look for
        /// </summary>
        /// <param name="id">The id to filter based on for the model of Member</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult Member(int id, LoggedInUser currentUser)
        {
            this.log.Info("Member", "Start of function");
            var item = work.UserRepository.Find(id);
            ViewBag.FilteredType = "Member";
            ViewBag.FilteredBy = item.NameFull;
            ViewBag.PageDisplayMode = PageDisplayMode.Index;
            
            ViewResult result = View("Index", item.Memberships);
            this.log.Info("Member", "View created and returning");
            return result;
        }  // GET: /Admin/Memberships/Member/{id}
		
        /// <summary>
        ///     Filters the listing down to only show specific Membership records based on the id provided for the Customer record to look for
        /// </summary>
        /// <param name="id">The id to filter based on for the model of Customer</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult MemberInline(int id, LoggedInUser currentUser)
        {
            this.log.Info("MemberInline", "Start of function");
            var item = work.UserRepository.Find(id);
            ViewResult result = View("_List", item.Memberships);
            this.log.Info("MemberInline", "View created and returning");
            return result;
        }  // GET: /Admin/Memberships/MemberInline/{id}
					
        /// <summary>
        ///     Filters the listing down to only show specific Membership records based on the id provided for the Group record to look for
        /// </summary>
        /// <param name="id">The id to filter based on for the model of Group</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult Group(int id, LoggedInUser currentUser)
        {
            this.log.Info("Group", "Start of function");
            var item = work.UserGroupRepository.Find(id);
            ViewBag.FilteredType = "Group";
            ViewBag.FilteredBy = item.Name;
            ViewBag.PageDisplayMode = PageDisplayMode.Index;
            
            ViewResult result = View("Index", item.Memberships);
            this.log.Info("Group", "View created and returning");
            return result;
        }  // GET: /Admin/Memberships/Group/{id}
		
        /// <summary>
        ///     Filters the listing down to only show specific Membership records based on the id provided for the Customer record to look for
        /// </summary>
        /// <param name="id">The id to filter based on for the model of Customer</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult GroupInline(int id, LoggedInUser currentUser)
        {
            this.log.Info("GroupInline", "Start of function");
            var item = work.UserGroupRepository.Find(id);
            ViewResult result = View("_List", item.Memberships);
            this.log.Info("GroupInline", "View created and returning");
            return result;
        }  // GET: /Admin/Memberships/GroupInline/{id}
        #endregion
        
        private void LoadDropDowns()
        {
			var possibleUsers = work.UserRepository.All.ToList();
			ViewBag.PossibleUsers = possibleUsers.OrderBy(x => x.NameFull);
            ViewBag.PossibleUserGroups = work.UserGroupRepository.All.OrderBy(x => x.Name);
        }

        /// <summary>
        ///     Disposes of the controller
        /// </summary>
        /// <param name="disposing">Whether or not the unit of work should be disposed</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                work.Dispose();
            base.Dispose(disposing);
        }
    }
}