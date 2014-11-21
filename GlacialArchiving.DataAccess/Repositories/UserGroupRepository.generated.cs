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
    public partial class UserGroupRepository : GenericRepository<UserGroup>
    {
        public UserGroupRepository(DataContext context)
            : base(context)
        {
        }
    }
}
