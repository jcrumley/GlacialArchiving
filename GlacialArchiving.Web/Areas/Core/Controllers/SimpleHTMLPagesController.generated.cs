using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBADev.MVC.Entity;
using TBADev.MVC.Tools;
using GlacialArchiving.Web.Common;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.Web.Areas.Core.Controllers
{   
    /// <summary>
    ///     The definition of SimpleHTMLPagesController
    /// </summary>
    public partial class SimpleHTMLPagesController : BaseController
    {
        /// <summary>
        ///     Details page for SimpleHTMLPage by id
        /// </summary>
        /// <param name="id">The id for the SimpleHTMLPage record to display</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        public ActionResult Display(int id, LoggedInUser currentUser)
        {
            this.log.Info("Display", "Start of function");
            ViewBag.AllowEdit = true;
            SimpleHTMLPage obj = work.SimpleHTMLPageRepository.Find(id);
            this.log.Info("Display", (obj != null ? "Found object in repository" : "Object NOT found in repository"));
            ViewResult result = View(obj);
            this.log.Info("Display", "View created and returning");
            return result;
        }  // GET: /Core/SimpleHTMLPages/Display/{id}
		
		/// <summary>
        ///     Submission of the editing model back from the UI
        /// </summary>
        /// <param name="obj">The edited model</param>
        /// <param name="currentUser">The current logged in user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(SimpleHTMLPage obj, LoggedInUser currentUser)
        {
            this.log.Info("Edit [POST]", "Start of function");

            SimpleHTMLPage oldObj = work.SimpleHTMLPageRepository.Find(obj.Id);
            oldObj.BodyHTML = obj.BodyHTML;
            work.Save();
            this.log.Info("Edit [POST]", "Object saved and returning");
            return RedirectToAction("Display", new { obj.Id });

        }  // POST: /SimpleHTMLPages/Edit/{id}
    }
}