using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.Web.Common
{
    public partial class BaseController
    {
		protected Navigation getNavigation(LoggedInUser user)
        {
            return work.NavigationRepository.All.FirstOrDefault();
        }
    }
}