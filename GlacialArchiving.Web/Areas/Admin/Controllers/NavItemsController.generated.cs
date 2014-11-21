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
    ///     The definition of NavItemsController
    /// </summary>
    public partial class NavItemsController : SecureController
    {
        /// <summary>
        ///     Default constructor for NavItemsController
        /// </summary>
        public NavItemsController() 
        { 
            ViewBag.AllowCreate = true;
            ViewBag.AllowEdit = true;
            ViewBag.AllowDelete = true;
            ViewBag.PageDisplayMode = PageDisplayMode.Inline;
            ViewBag.IsPost = false;
        }
        
        #region Section 'Partial Functions'
        partial void BeforeValidate(NavItem obj);
        partial void BeforeCreate(NavItem obj);
        partial void AfterCreate(NavItem obj);
        partial void BeforeEdit(NavItem oldObj, NavItem obj);
        partial void AfterEdit(NavItem obj);
        partial void BeforeDelete(int id);
        partial void AfterDelete(int id);
        #endregion

        #region Section 'Actions - Index'
        /// <summary>
        ///     Sends the user to the listing page for NavItems
        /// </summary>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult Index(LoggedInUser currentUser)
        {
            this.log.Info("Index", "Start of function");
            ViewBag.PageDisplayMode = PageDisplayMode.Index;
            ViewResult result = View(work.NavItemRepository.AllIncluding(obj => obj.TopLevel, obj => obj.Icon, obj => obj.NavSubItems));
            this.log.Info("Index", "View created and returning");
            return result;
        }   // GET: /Admin/NavItems/
        #endregion
        #region Section 'Actions - Create'
        /// <summary>
        ///     View to create a new NavItem in the system
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
        }   // GET: /Admin/NavItems/Create
        /// <summary>
        ///     Submission of a NavItem from the UI
        /// </summary>
        /// <param name="obj">The new object to insert into the database</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(NavItem obj, LoggedInUser currentUser)
        {
            this.log.Info("Create [POST]", "Start of function");
            if (obj.Id == 0)
                ModelState["NavItemId"].Errors.Clear();
                
            this.BeforeValidate(obj);
            if (ModelState.IsValid) 
            {
                this.BeforeCreate(obj);
                work.NavItemRepository.InsertOrUpdate(obj, currentUser.User);
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
        }  // POST: /Admin/NavItems/Create
        #endregion
        #region Section 'Actions - Edit'
        /// <summary>
        ///     View to edit an existing NavItem record in the system
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
            
            ViewResult result = View(work.NavItemRepository.Find(id));
            this.log.Info("Edit", "View created and returning");
            return result;
        }  // GET: /Admin/NavItems/Edit/{id}
        /// <summary>
        ///     Submission of the editing model back from the UI
        /// </summary>
        /// <param name="obj">The edited model</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(NavItem obj, LoggedInUser currentUser)
        {
            this.log.Info("Edit [POST]", "Start of function");
            this.LoadDropDowns();
            this.BeforeValidate(obj);
            if (ModelState.IsValid) 
            {
                NavItem oldObj = work.NavItemRepository.Find(obj.Id);
                obj.SyncExcludedFields(oldObj);
            
                this.BeforeEdit(oldObj, obj);
                work.NavItemRepository.InsertOrUpdate(obj, currentUser.User);
                work.Save();
                obj = work.NavItemRepository.Find(obj.Id); //need to rehydrate the FKs
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
        }  // POST: /Admin/NavItems/Edit/{id}
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
            ViewResult result = View(work.NavItemRepository.Find(id));
            this.log.Info("Delete", "View created and returning");
            return result;
        }  // GET: /Admin/NavItems/Delete/5
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
            work.NavItemRepository.Delete(id);
            work.Save();
            this.AfterDelete(id);
            this.log.Info("Delete [POST]", "Object has been deleted");
            return RedirectToAction("Index");
        }  // POST: /Admin/NavItems/Delete/{id}
        #endregion
        #region Section 'Actions - Inline'
        /// <summary>
        ///     Retrieve view to create/edit an existing NavItem record in the system
        /// </summary>
        /// <param name="id">The id of the record to edit</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult QueryItem(int id, LoggedInUser currentUser)
        {
            this.log.Info("QueryItem", "Start of function");
            this.LoadDropDowns();

            NavItem obj = null;
            if (id != 0)
                obj = work.NavItemRepository.Find(id);

            ViewResult result = View("_CreateOrEdit", obj);
            this.log.Info("QueryItem", "View created and returning");
            return result;
        }  // GET: /Admin/NavItems/QueryItem/{id}
        /// <summary>
        ///     Submission of the editing model back from the UI
        /// </summary>
        /// <param name="obj">The edited model</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public MvcHtmlString InsertOrUpdate(NavItem obj, LoggedInUser currentUser)
        {
            this.log.Info("Edit [POST]", "Start of function");
            if (obj.Id == 0)
                ModelState["NavItemId"].Errors.Clear();
                
            
            bool isNew = false;
            AjaxResult returnObj;
            this.BeforeValidate(obj);
            if (ModelState.IsValid)
            {
                if (obj.Id != 0)
                {
                    NavItem oldObj = work.NavItemRepository.Find(obj.Id);
                    obj.SyncExcludedFields(oldObj);
                    isNew = true;
                    this.BeforeEdit(oldObj, obj);
                }
                else
                    this.BeforeCreate(obj);

                work.NavItemRepository.InsertOrUpdate(obj, currentUser.User);
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
            ViewResult result = View("_Display", work.NavItemRepository.Find(id));
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
                work.NavItemRepository.Delete(id);
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
        ///     Filters the listing down to only show specific NavItem records based on the id provided for the TopLevel record to look for
        /// </summary>
        /// <param name="id">The id to filter based on for the model of TopLevel</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult TopLevel(int id, LoggedInUser currentUser)
        {
            this.log.Info("TopLevel", "Start of function");
            var item = work.NavigationRepository.Find(id);
            ViewBag.FilteredType = "TopLevel";
            ViewBag.FilteredBy = item.Name;
            ViewBag.PageDisplayMode = PageDisplayMode.Index;
            
            ViewResult result = View("Index", item.NavItems);
            this.log.Info("TopLevel", "View created and returning");
            return result;
        }  // GET: /Admin/NavItems/TopLevel/{id}
		
        /// <summary>
        ///     Filters the listing down to only show specific NavItem records based on the id provided for the Customer record to look for
        /// </summary>
        /// <param name="id">The id to filter based on for the model of Customer</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult TopLevelInline(int id, LoggedInUser currentUser)
        {
            this.log.Info("TopLevelInline", "Start of function");
            var item = work.NavigationRepository.Find(id);
            ViewResult result = View("_List", item.NavItems);
            this.log.Info("TopLevelInline", "View created and returning");
            return result;
        }  // GET: /Admin/NavItems/TopLevelInline/{id}
        #endregion
        
        private void LoadDropDowns()
        {
            ViewBag.PossibleNavigations = work.NavigationRepository.All.OrderBy(x => x.Name);
            ViewBag.PossibleNavIcons = work.NavIconRepository.All.OrderBy(x => x.Name);
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