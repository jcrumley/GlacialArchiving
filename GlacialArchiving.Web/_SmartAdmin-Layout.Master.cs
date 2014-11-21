using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TBADev.MVC.DataAttributes;

namespace GlacialArchiving.Web
{
    public partial class _SmartAdmin_Layout: System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BreadcrumbAttribute.SetBreadcrumbs(this.breadcrumb, Request);
        }
    }
}