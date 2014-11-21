using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using TBADev.MVC.Security;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.Web.Common
{
    public partial class SecurePage : BasePage
    {
        #region Property 'UserDetails'
        private User m_UserDetails = null;
        /// <summary>
        ///     The user currently logged in
        /// </summary>
        public User UserDetails
        {
            get
            {
                if (m_UserDetails == null)
                {
                    try
                    {
                        if (!this.User.Identity.IsAuthenticated)
                            throw new Exception("SecurePage.UserDetails : No User is authenicated");
                            
                        LoggedInUser loggedIn = new LoggedInUser(work, User.Identity.Name);
                        if(loggedIn.User == null)
                            throw new Exception("SecurePage.UserDetails : No User found for email = " + User.Identity.Name);
                        m_UserDetails = loggedIn.User;
                    }
                    catch
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                }
                return m_UserDetails;
            }
        }
        #endregion
        
        public SecurePage()
            : base()
        {

            this.Init += new EventHandler(SecurePage_Init);
            this.Load += new EventHandler(SecurePage_Load);
        }

        partial void OnInit();
        partial void OnLoad();

        protected void SecurePage_Init(object sender, EventArgs e)
        {
            this.OnInit();
        }
        protected void SecurePage_Load(object sender, EventArgs e)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                Session["m"] = "You are not signed in";
                FormsAuthentication.RedirectToLoginPage();
            }

            this.log.SetUser(this.UserDetails);
            this.CheckForGrids(this.Controls); 
            
            this.OnLoad();
        }
        
        protected void CheckForGrids(ControlCollection cc)
        {
            foreach (Control c in cc)
            {
                if (c is TBADev.MVC.WebControls.Grid)
                {
                    (c as TBADev.MVC.WebControls.Grid).User = this.UserDetails;
                }
                if (c.Controls.Count > 0)
                    this.CheckForGrids(c.Controls);
            }
        }
    }
}