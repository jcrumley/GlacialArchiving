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
    ///     The definition of ClientsController
    /// </summary>
    public partial class ClientsController : SecureController
    {
        /// <summary>
        ///     Default constructor for ClientsController
        /// </summary>
        public ClientsController() 
        { 
            ViewBag.AllowCreate = true;
            ViewBag.AllowEdit = true;
            ViewBag.AllowDelete = true;
            ViewBag.PageDisplayMode = PageDisplayMode.Inline;
            ViewBag.IsPost = false;
        }
        
        #region Section 'Partial Functions'
        partial void BeforeValidate(Client obj);
        partial void BeforeCreate(Client obj);
        partial void AfterCreate(Client obj);
        partial void BeforeEdit(Client oldObj, Client obj);
        partial void AfterEdit(Client obj);
        partial void BeforeDelete(int id);
        partial void AfterDelete(int id);
        #endregion

        #region Section 'Actions - Index'
        /// <summary>
        ///     Sends the user to the listing page for Clients
        /// </summary>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult Index(LoggedInUser currentUser)
        {
            this.log.Info("Index", "Start of function");
            ViewBag.PageDisplayMode = PageDisplayMode.Index;
            ViewResult result = View(work.ClientRepository.AllIncluding(obj => obj.CreatedBy, obj => obj.ModifiedBy));
            this.log.Info("Index", "View created and returning");
            return result;
        }   // GET: /Admin/Clients/
        #endregion
        #region Section 'Actions - Create'
        /// <summary>
        ///     View to create a new Client in the system
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
        }   // GET: /Admin/Clients/Create
        /// <summary>
        ///     Submission of a Client from the UI
        /// </summary>
        /// <param name="obj">The new object to insert into the database</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Client obj, LoggedInUser currentUser)
        {
            this.log.Info("Create [POST]", "Start of function");
            if (obj.Id == 0)
                ModelState["ClientId"].Errors.Clear();
                
            this.BeforeValidate(obj);
            if (ModelState.IsValid) 
            {
                this.BeforeCreate(obj);
                work.ClientRepository.InsertOrUpdate(obj, currentUser.User);
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
        }  // POST: /Admin/Clients/Create
        #endregion
        #region Section 'Actions - Edit'
        /// <summary>
        ///     View to edit an existing Client record in the system
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
            
            ViewResult result = View(work.ClientRepository.Find(id));
            this.log.Info("Edit", "View created and returning");
            return result;
        }  // GET: /Admin/Clients/Edit/{id}
        /// <summary>
        ///     Submission of the editing model back from the UI
        /// </summary>
        /// <param name="obj">The edited model</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Client obj, LoggedInUser currentUser)
        {
            this.log.Info("Edit [POST]", "Start of function");
            this.LoadDropDowns();
            this.BeforeValidate(obj);
            if (ModelState.IsValid) 
            {
                Client oldObj = work.ClientRepository.Find(obj.Id);
                obj.SyncExcludedFields(oldObj);
            
                this.BeforeEdit(oldObj, obj);
                work.ClientRepository.InsertOrUpdate(obj, currentUser.User);
                work.Save();
                obj = work.ClientRepository.Find(obj.Id); //need to rehydrate the FKs
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
        }  // POST: /Admin/Clients/Edit/{id}
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
            ViewResult result = View(work.ClientRepository.Find(id));
            this.log.Info("Delete", "View created and returning");
            return result;
        }  // GET: /Admin/Clients/Delete/5
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
            work.ClientRepository.Delete(id);
            work.Save();
            this.AfterDelete(id);
            this.log.Info("Delete [POST]", "Object has been deleted");
            return RedirectToAction("Index");
        }  // POST: /Admin/Clients/Delete/{id}
        #endregion
        #region Section 'Actions - Inline'
        /// <summary>
        ///     Retrieve view to create/edit an existing Client record in the system
        /// </summary>
        /// <param name="id">The id of the record to edit</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult QueryItem(int id, LoggedInUser currentUser)
        {
            this.log.Info("QueryItem", "Start of function");
            this.LoadDropDowns();

            Client obj = null;
            if (id != 0)
                obj = work.ClientRepository.Find(id);

            ViewResult result = View("_CreateOrEdit", obj);
            this.log.Info("QueryItem", "View created and returning");
            return result;
        }  // GET: /Admin/Clients/QueryItem/{id}
        /// <summary>
        ///     Submission of the editing model back from the UI
        /// </summary>
        /// <param name="obj">The edited model</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public MvcHtmlString InsertOrUpdate(Client obj, LoggedInUser currentUser)
        {
            this.log.Info("Edit [POST]", "Start of function");
            if (obj.Id == 0)
                ModelState["ClientId"].Errors.Clear();
                
            
            bool isNew = false;
            AjaxResult returnObj;
            this.BeforeValidate(obj);
            if (ModelState.IsValid)
            {
                if (obj.Id != 0)
                {
                    Client oldObj = work.ClientRepository.Find(obj.Id);
                    obj.SyncExcludedFields(oldObj);
                    isNew = true;
                    this.BeforeEdit(oldObj, obj);
                }
                else
                    this.BeforeCreate(obj);

                work.ClientRepository.InsertOrUpdate(obj, currentUser.User);
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
            ViewResult result = View("_Display", work.ClientRepository.Find(id));
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
                work.ClientRepository.Delete(id);
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
        #endregion
        
        private void LoadDropDowns()
        {
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