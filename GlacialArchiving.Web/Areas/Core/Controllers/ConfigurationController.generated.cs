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
    ///     Controller for various functions used for configurating the system
    /// </summary>
    public partial class ConfigurationController : BaseController
	{
        /// <summary>
        /// Basic user interface for configuration
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }   // GET: /Core/Configuration/

        /// <summary>
        ///     Fires the seeding methods to put data in the database
        /// </summary>
        /// <returns></returns>
        public ActionResult FireSeed()
        {
            DataAccess.Migrations.Seeding seed = new DataAccess.Migrations.Seeding();
            //seed.ResetData();

            return View("Index");
        }  // GET: /Core/Configuration/FireSeed

        /// <summary>
        /// Creates a default simple page for a menu item to target
        /// </summary>
        /// <param name="title"></param>
        /// <param name="bodyHTML"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        protected int CreateSimplePage(string title, string bodyHTML, LoggedInUser currentUser)
        {
            SimpleHTMLPage page = new SimpleHTMLPage() { BodyHTML = bodyHTML, Title = title };

            work.SimpleHTMLPageRepository.InsertOrUpdate(page, currentUser.User);
            work.Save();

            return page.SimpleHTMLPageId;
        }

        /// <summary>
        /// Add a new Nav Item
        /// </summary>
        /// <param name="parentId">NavigationId of the parent item</param>
        /// <param name="title"></param>
        /// <param name="url"></param>
        /// <param name="sequence"></param>
        /// <param name="currentUser"></param>
        public void AddNavItem(int parentId, string title, string url, int sequence, LoggedInUser currentUser)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                int pageId = this.CreateSimplePage(title, title, currentUser);
                url = "/Core/SimpleHTMLPages/Display/" + pageId.ToString();
            }


            NavItem navItem = new NavItem()
            {
                Sequence = sequence,
                Title = title,
                TopLevelId = parentId,
                URL = url
            };

            work.NavItemRepository.InsertOrUpdate(navItem, currentUser.User);
            work.Save();

            return;
        }

        /// <summary>
        /// Add a new NavSubItem
        /// </summary>
        /// <param name="parentId">NavItemId of the NavItem under which this sits</param>
        /// <param name="title"></param>
        /// <param name="url"></param>
        /// <param name="sequence"></param>
        /// <param name="currentUser"></param>
        public void AddNavSubItem(int parentId, string title, string url, int sequence, LoggedInUser currentUser)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                int pageId = this.CreateSimplePage(title, title, currentUser);
                url = "/Core/SimpleHTMLPages/Display/" + pageId.ToString();
            }

            NavSubItem item = new NavSubItem()
            {
                Sequence = sequence,
                Title = title,
                NavItemId = parentId,
                URL = url
            };

            work.NavSubItemRepository.InsertOrUpdate(item, currentUser.User);
            work.Save();

            return;
        }

        /// <summary>
        /// Edit a NavItem
        /// </summary>
        /// <param name="id">NavItemId to be edited</param>
        /// <param name="title"></param>
        /// <param name="url"></param>
        /// <param name="sequence"></param>
        /// <param name="currentUser"></param>
        public void EditNavItem(int id, string title, string url, int sequence, LoggedInUser currentUser)
        {
            NavItem navItem = work.NavItemRepository.Find(id);
            if (navItem == null) return;

            navItem.Title = title;
            navItem.Sequence = sequence;
            navItem.URL = url;
            work.NavItemRepository.InsertOrUpdate(navItem, currentUser.User);
            work.Save();
        }

        /// <summary>
        /// Edit a NavSubItem
        /// </summary>
        /// <param name="id">NavSubItemId to be edited</param>
        /// <param name="title"></param>
        /// <param name="url"></param>
        /// <param name="sequence"></param>
        /// <param name="currentUser"></param>
        public void EditNavSubItem(int id, string title, string url, int sequence, LoggedInUser currentUser)
        {
            NavSubItem navItem = work.NavSubItemRepository.Find(id);
            if (navItem == null) return;

            navItem.Title = title;
            navItem.Sequence = sequence;
            navItem.URL = url;
            work.NavSubItemRepository.InsertOrUpdate(navItem, currentUser.User);
            work.Save();
        }

        /// <summary>
        /// Delete a NavItem
        /// </summary>
        /// <param name="id">NavItemId to be deleted</param>
        /// <param name="currentUser"></param>
        public void DeleteNavItem(int id, LoggedInUser currentUser)
        {
            work.NavItemRepository.Delete(id);
            work.Save();
        }

        /// <summary>
        /// Delete a NavSubItem
        /// </summary>
        /// <param name="id">NavSubItemId to be deleted</param>
        /// <param name="currentUser"></param>
        public void DeleteNavSubItem(int id, LoggedInUser currentUser)
        {
            work.NavSubItemRepository.Delete(id);
            work.Save();
        }
	}
}