using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.Web.Common
{
    /// <summary>
    ///     Controls outside verification
    /// </summary>
    public static partial class AuthConfig
    {
        /// <summary>
        ///     To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
        ///     you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166
        /// </summary>
        public static void RegisterAuth()
        {
            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}