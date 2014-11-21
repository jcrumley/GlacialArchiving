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
    ///     The definition of DataTagsController
    /// </summary>
    public partial class DataTagsController : SecureController
    {
        /// <summary>
        ///     Default constructor for DataTagsController
        /// </summary>
        public DataTagsController() 
        { 
            ViewBag.AllowCreate = true;
            ViewBag.AllowEdit = true;
            ViewBag.AllowDelete = true;
            ViewBag.PageDisplayMode = PageDisplayMode.Inline;
            ViewBag.IsPost = false;
        }
        
        #region Section 'Partial Functions'
        partial void BeforeValidate(DataTag obj);
        partial void BeforeCreate(DataTag obj);
        partial void AfterCreate(DataTag obj);
        partial void BeforeEdit(DataTag oldObj, DataTag obj);
        partial void AfterEdit(DataTag obj);
        partial void BeforeDelete(int id);
        partial void AfterDelete(int id);
        #endregion

        #region Section 'Actions - Index'
        /// <summary>
        ///     Sends the user to the listing page for DataTags
        /// </summary>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult Index(LoggedInUser currentUser)
        {
            this.log.Info("Index", "Start of function");
            ViewBag.PageDisplayMode = PageDisplayMode.Index;
            ViewResult result = View(work.DataTagRepository.AllIncluding(obj => obj.UploadedItem, obj => obj.CreatedBy, obj => obj.ModifiedBy));
            this.log.Info("Index", "View created and returning");
            return result;
        }   // GET: /Admin/DataTags/
        #endregion
        #region Section 'Actions - Create'
        /// <summary>
        ///     View to create a new DataTag in the system
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
        }   // GET: /Admin/DataTags/Create
        /// <summary>
        ///     Submission of a DataTag from the UI
        /// </summary>
        /// <param name="obj">The new object to insert into the database</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(DataTag obj, LoggedInUser currentUser)
        {
            this.log.Info("Create [POST]", "Start of function");
            if (obj.Id == 0)
                ModelState["DataTagId"].Errors.Clear();
                
            this.BeforeValidate(obj);
            if (ModelState.IsValid) 
            {
                this.BeforeCreate(obj);
                work.DataTagRepository.InsertOrUpdate(obj, currentUser.User);
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
        }  // POST: /Admin/DataTags/Create
        #endregion
        #region Section 'Actions - Edit'
        /// <summary>
        ///     View to edit an existing DataTag record in the system
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
            
            ViewResult result = View(work.DataTagRepository.Find(id));
            this.log.Info("Edit", "View created and returning");
            return result;
        }  // GET: /Admin/DataTags/Edit/{id}
        /// <summary>
        ///     Submission of the editing model back from the UI
        /// </summary>
        /// <param name="obj">The edited model</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(DataTag obj, LoggedInUser currentUser)
        {
            this.log.Info("Edit [POST]", "Start of function");
            this.LoadDropDowns();
            this.BeforeValidate(obj);
            if (ModelState.IsValid) 
            {
                DataTag oldObj = work.DataTagRepository.Find(obj.Id);
                obj.SyncExcludedFields(oldObj);
            
                this.BeforeEdit(oldObj, obj);
                work.DataTagRepository.InsertOrUpdate(obj, currentUser.User);
                work.Save();
                obj = work.DataTagRepository.Find(obj.Id); //need to rehydrate the FKs
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
        }  // POST: /Admin/DataTags/Edit/{id}
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
            ViewResult result = View(work.DataTagRepository.Find(id));
            this.log.Info("Delete", "View created and returning");
            return result;
        }  // GET: /Admin/DataTags/Delete/5
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
            DataTag obj = work.DataTagRepository.Find(id);
            obj.IsDeleted = true;
            work.Save();
            this.AfterDelete(id);
            this.log.Info("Delete [POST]", "Object has been deleted");
            return RedirectToAction("Index");
        }  // POST: /Admin/DataTags/Delete/{id}
        #endregion
        #region Section 'Actions - Inline'
        /// <summary>
        ///     Retrieve view to create/edit an existing DataTag record in the system
        /// </summary>
        /// <param name="id">The id of the record to edit</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult QueryItem(int id, LoggedInUser currentUser)
        {
            this.log.Info("QueryItem", "Start of function");
            this.LoadDropDowns();

            DataTag obj = null;
            if (id != 0)
                obj = work.DataTagRepository.Find(id);

            ViewResult result = View("_CreateOrEdit", obj);
            this.log.Info("QueryItem", "View created and returning");
            return result;
        }  // GET: /Admin/DataTags/QueryItem/{id}
        /// <summary>
        ///     Submission of the editing model back from the UI
        /// </summary>
        /// <param name="obj">The edited model</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public MvcHtmlString InsertOrUpdate(DataTag obj, LoggedInUser currentUser)
        {
            this.log.Info("Edit [POST]", "Start of function");
            if (obj.Id == 0)
                ModelState["DataTagId"].Errors.Clear();
                
            
            bool isNew = false;
            AjaxResult returnObj;
            this.BeforeValidate(obj);
            if (ModelState.IsValid)
            {
                if (obj.Id != 0)
                {
                    DataTag oldObj = work.DataTagRepository.Find(obj.Id);
                    obj.SyncExcludedFields(oldObj);
                    isNew = true;
                    this.BeforeEdit(oldObj, obj);
                }
                else
                    this.BeforeCreate(obj);

                work.DataTagRepository.InsertOrUpdate(obj, currentUser.User);
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
            ViewResult result = View("_Display", work.DataTagRepository.Find(id));
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
                DataTag obj = work.DataTagRepository.Find(id);
                obj.IsDeleted = true;
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
        ///     Filters the listing down to only show specific DataTag records based on the id provided for the UploadedItem record to look for
        /// </summary>
        /// <param name="id">The id to filter based on for the model of UploadedItem</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult UploadedItem(int id, LoggedInUser currentUser)
        {
            this.log.Info("UploadedItem", "Start of function");
            var item = work.UploadedItemRepository.Find(id);
            ViewBag.FilteredType = "UploadedItem";
            ViewBag.FilteredBy = item.UniqueIdentifier;
            ViewBag.PageDisplayMode = PageDisplayMode.Index;
            
            ViewResult result = View("Index", item.DataTags);
            this.log.Info("UploadedItem", "View created and returning");
            return result;
        }  // GET: /Admin/DataTags/UploadedItem/{id}
		
        /// <summary>
        ///     Filters the listing down to only show specific DataTag records based on the id provided for the Customer record to look for
        /// </summary>
        /// <param name="id">The id to filter based on for the model of Customer</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult UploadedItemInline(int id, LoggedInUser currentUser)
        {
            this.log.Info("UploadedItemInline", "Start of function");
            var item = work.UploadedItemRepository.Find(id);
            ViewResult result = View("_List", item.DataTags);
            this.log.Info("UploadedItemInline", "View created and returning");
            return result;
        }  // GET: /Admin/DataTags/UploadedItemInline/{id}
        #endregion
        
        private void LoadDropDowns()
        {
            ViewBag.PossibleUploadedItems = work.UploadedItemRepository.All.OrderBy(x => x.UniqueIdentifier);
			var possibleUsers = work.UserRepository.All.ToList();
			ViewBag.PossibleUsers = possibleUsers.OrderBy(x => x.NameFull);
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