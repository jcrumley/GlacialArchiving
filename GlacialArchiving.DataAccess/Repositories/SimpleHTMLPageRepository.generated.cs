using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlacialArchiving.DataAccess.Base;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;

namespace GlacialArchiving.DataAccess.Repositories
{
    public partial class SimpleHTMLPageRepository : GenericRepository<SimpleHTMLPage>
    {
        public SimpleHTMLPageRepository(DataContext context)
            : base(context)
        {
        }
    }
}
