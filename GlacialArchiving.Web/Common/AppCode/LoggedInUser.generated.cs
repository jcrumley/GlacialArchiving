using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBADev.MVC.Configuration;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.Web.Common
{
    /// <summary>
    ///     Handles the setting for the current logged in user
    /// </summary>
    [System.Web.Mvc.ModelBinder(typeof(LoggedInUserBinder))]
    [System.Web.Http.ModelBinding.ModelBinder(typeof(LoggedInUserBinder))]
    public partial class LoggedInUser
    {
        /// <summary>
        ///     Model for the user currently logged in
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///     Constructor to initialize object with user
        /// </summary>
        /// <param name="u">The user record that is logged in</param>
        public LoggedInUser(User u)
        {
            this.User = u;
        }
        /// <summary>
        ///     Constructor that will look up the user 
        /// </summary>
        /// <param name="work">The unit of work to use to query for the user</param>
        /// <param name="userName">The user that should be queried</param>
        public LoggedInUser(UnitOfWork work, string userName)
        {
            this.User = work.UserRepository.GetByEmailAddress(userName);
            if (this.User == null)
            {
                TBADevSection section = TBADevSection.GetSection();
                if (section != null)
                {
                    foreach(string domain in section.EmailDomains)
                    {
                        this.User = work.UserRepository.GetByEmailAddress(userName + "@" + domain);
                        if (this.User != null)
                            break;
                    }
                }
            }
        }
    }
}