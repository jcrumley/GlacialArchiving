using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TBADev.MVC.Entity;
using TBADev.MVC.MVCHelpers;
using TBADev.MVC.Tools;
using TBADev.MVC.Security;
using GlacialArchiving.Web.Common;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.Web.Areas.Admin.Controllers
{   
    /// <summary>
    ///     The definition of UsersController
    /// </summary>
    public partial class UsersController : SecureController
    {
        /// <summary>
        ///     Default constructor for UsersController
        /// </summary>
        public UsersController() 
        { 
            ViewBag.AllowCreate = true;
            ViewBag.AllowEdit = true;
            ViewBag.AllowDelete = true;
            ViewBag.PageDisplayMode = PageDisplayMode.Inline;
            ViewBag.IsPost = false;
            ViewBag.IsNewUser = false;
        }

        #region Section 'Partial Functions'
        partial void BeforeValidate(User  obj);
        partial void BeforeCreate(User obj);
        partial void AfterCreate(User obj);
        partial void BeforeEdit(User oldObj, User obj);
        partial void AfterEdit(User obj);
        partial void BeforeDelete(int id);
        partial void AfterDelete(int id);
        #endregion

        #region Section 'Actions - Index'
        /// <summary>
        ///     Sends the user to the listing page for Users
        /// </summary>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult Index(LoggedInUser currentUser)
        {
            this.log.Info("Index", "Start of function");
            ViewBag.PageDisplayMode = PageDisplayMode.Index;
            ViewResult result = View(work.UserRepository.AllIncluding(obj => obj.CreatedBy, obj => obj.ModifiedBy, obj => obj.Memberships));
            this.log.Info("Index", "View created and returning");
            return result;
        }   // GET: /Admin/Users/
        #endregion
        #region Section 'Actions - Create'
        /// <summary>
        ///     View to create a new User in the system
        /// </summary>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult Create(LoggedInUser currentUser)
        {
            this.log.Info("Create", "Start of function");
            this.LoadDropDowns();
            
            User u = new DataAccess.Models.User();
            u.IsActive = true;
            ViewBag.IsNewUser = true;
            
            ViewResult result = View("Create", u);
            this.log.Info("Create", "View created and returning");
            return result;
        }   // GET: /Admin/Users/Create
        /// <summary>
        ///     Submission of a User from the UI
        /// </summary>
        /// <param name="obj">The new object to insert into the database</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(User obj, LoggedInUser currentUser)
        {
            this.log.Info("Create [POST]", "Start of function");
            if (obj.Id == 0)
                ModelState["UserId"].Errors.Clear();
                
            
            if (!work.UserRepository.IsUniqueEmailAddress(obj.EmailAddress, obj))
                ModelState.AddModelError("Email Address", "The Email Address provided already exists in the database.");
                
            if (!string.IsNullOrWhiteSpace(obj.Password))
                obj.Password = TBADevMembershipProvider.EncodePassword(obj.Password);

            this.BeforeValidate(obj);
            if (ModelState.IsValid) 
            {
                this.BeforeCreate(obj);
                work.UserRepository.InsertOrUpdate(obj, currentUser.User);
                work.Save();
                TempData["WasCreated"] = true;
                this.log.Info("Create [POST]", "Object saved and redirecting");
                this.AfterCreate(obj);
                return RedirectToAction("Edit", new { id = obj.Id });
            } 
            else
            {
                this.SetErrors();
                this.LoadDropDowns();
                this.log.Info("Create [POST]", "Unable to save object");
                ViewBag.IsNewUser = true;
                ViewBag.IsPost = true;
                return View("Create", obj);
            }
        }  // POST: /Admin/Users/Create
        #endregion
        #region Section 'Actions - Edit'
        /// <summary>
        ///     View to edit an existing User record in the system
        /// </summary>
        /// <param name="id">The id of the record to edit</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult Edit(int id, LoggedInUser currentUser)
        {
            this.log.Info("Edit", "Start of function");
            this.LoadDropDowns();
            ViewBag.IsCurrentUser = (id == currentUser.User.Id);
	    
            if (TempData["WasCreated"] != null)
                ViewBag.IsPost = true;
            
            ViewResult result = View("Edit", work.UserRepository.Find(id));
            this.log.Info("Edit", "View created and returning");
            return result;
        }  // GET: /Admin/Users/Edit/{id}
        /// <summary>
        ///     Submission of the editing model back from the UI
        /// </summary>
        /// <param name="obj">The edited model</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(User obj, LoggedInUser currentUser)
        {
            this.log.Info("Edit [POST]", "Start of function");
            this.LoadDropDowns();
            
            if (!work.UserRepository.IsUniqueEmailAddress(obj.EmailAddress, obj))
                ModelState.AddModelError("Email Address", "The Email Address provided already exists in the database.");

            this.BeforeValidate(obj);
            if (ModelState.IsValid) 
            {
                User oldObj = work.UserRepository.Find(obj.Id);
                obj.SyncExcludedFields(oldObj);
                
                this.BeforeEdit(oldObj, obj);            
                work.UserRepository.InsertOrUpdate(obj, currentUser.User);
                work.Save();
                obj = work.UserRepository.Find(obj.Id); //need to rehydrate the FKs
                this.AfterEdit(obj);
                this.log.Info("Edit [POST]", "Object saved and returning");
            } 
            else 
            {
                this.SetErrors();
                this.log.Info("Edit [POST]", "Unable to save object");
            }
            
            ViewBag.IsCurrentUser = (obj.Id == currentUser.User.Id);
            ViewBag.IsPost = true;
            return View("_CreateOrEdit", obj);
        }  // POST: /Admin/Users/Edit/{id}
        #endregion
        #region Section 'Actions - MyAccount'
        /// <summary>
        ///     View to edit the current logged in user
        /// </summary>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult MyAccount(LoggedInUser currentUser)
        {
            return Edit(currentUser.User.Id, currentUser);
        }  // GET: /Admin/Users/MyAccount
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
            ViewResult result = View(work.UserRepository.Find(id));
            this.log.Info("Delete", "View created and returning");
            return result;
        }  // GET: /Admin/Users/Delete/5
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
            work.UserRepository.Delete(id);
            work.Save();
            this.AfterDelete(id);
            this.log.Info("Delete [POST]", "Object has been deleted");            
            return RedirectToAction("Index");
        }  // POST: /Admin/Users/Delete/{id}
        #endregion
        #region Section 'Actions - Inline'
        /// <summary>
        ///     Retrieve view to create/edit an existing User record in the system
        /// </summary>
        /// <param name="id">The id of the record to edit</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult QueryItem(int id, LoggedInUser currentUser)
        {
            this.log.Info("QueryItem", "Start of function");
            this.LoadDropDowns();

            User obj = null;
            if (id != 0)
                obj = work.UserRepository.Find(id);

            ViewBag.IsCurrentUser = (id == currentUser.User.Id);
            ViewResult result = View("_CreateOrEdit", obj);
            this.log.Info("QueryItem", "View created and returning");
            return result;
        }  // GET: /Admin/Users/QueryItem/{id}
	    /// <summary>
        ///     Submission of the editing model back from the UI
        /// </summary>
        /// <param name="obj">The edited model</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public MvcHtmlString InsertOrUpdate(User obj, LoggedInUser currentUser)
        {
            this.log.Info("Edit [POST]", "Start of function");
            if (obj.Id == 0)
                ModelState["UserId"].Errors.Clear();
                
            
            bool isNew = false;
            AjaxResult returnObj;
            if (ModelState.IsValid)
            {
                if (obj.Id != 0)
                {
                    User oldObj = work.UserRepository.Find(obj.Id);
                    obj.SyncExcludedFields(oldObj);
                    isNew = true;
                    this.BeforeEdit(oldObj, obj);
                }
                else
                    this.BeforeCreate(obj);

                work.UserRepository.InsertOrUpdate(obj, currentUser.User);
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
            ViewResult result = View("_Display", work.UserRepository.Find(id));
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
                work.UserRepository.Delete(id);
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
	    #region Section 'User specific functions'
        /// <summary>
        ///     Updates the user's password using the provided information
        /// </summary>
        /// <param name="id">The user's id for the password change</param>
        /// <param name="settings">The settings that the password is to be set to</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePassword(int id, PasswordChangeSettings settings, LoggedInUser currentUser)
        {
            this.log.Info("ChangePassword [POST]", "Start of function");
            User u = work.UserRepository.Find(id);
            PasswordChangeResults results = u.ChangePassword(settings, currentUser.User.Id);
            if (results.IsValid)
            {
                work.UserRepository.InsertOrUpdate(u, currentUser.User);
                work.Save();
                this.log.Info("ChangePassword [POST]", "User was saved");
            }
            else
                this.log.Info("ChangePassword [POST]", "Unable to save user");

            return Json(results);
        }  // POST: /Admin/Users/ChangePassword/{id}
        /// <summary>
        ///     Resets the user's password and emails the new password to that user's email
        /// </summary>
        /// <param name="id">The user's id for the password reset</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResetPassword(int id, LoggedInUser currentUser)
        {
            this.log.Info("ResetPassword [POST]", "Start of function");
            bool result = false;
            try
            {
                User u = work.UserRepository.Find(id);
                u.ResetPassword(true, currentUser.User.Id);
                work.Save();
                result = true;
                this.log.Info("ResetPassword [POST]", "User was saved");
            }
            catch (Exception ex)
            {
                this.log.Info("ResetPassword [POST]", "Unable to save user");
                Utilities.SendEmail(work.SettingRepository.ErrorEmail, "Error reseting password", Utilities.CreateMessage(ex));
            }
            return Json(result);
        }  // POST: /Admin/Users/ResetPassword/{id}
        #endregion
	
        #region Section 'Parent Foriegn Keys'
        #endregion
        
        private void LoadDropDowns()
        {
            ViewBag.PossibleUsers = work.UserRepository.All;
            ViewBag.PossibleUsers = work.UserRepository.All;
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