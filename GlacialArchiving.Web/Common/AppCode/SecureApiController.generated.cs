using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.Web.Common
{
    /// <summary>
    ///     The controller used for the secure web api controllers 
    /// </summary>
    [Authorize]
    public partial class SecureApiController : BaseApiController
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
                        if (loggedIn.User == null)
                            throw new Exception("SecurePage.UserDetails : No User found for email = " + User.Identity.Name);
                        m_UserDetails = loggedIn.User;
                    }
                    catch { }
                }
                return m_UserDetails;
            }
        }
        #endregion

        public SecureApiController()
        {
            this.log.SetUser(this.UserDetails);
            //partial void OnLoad();
        }
    }
}
