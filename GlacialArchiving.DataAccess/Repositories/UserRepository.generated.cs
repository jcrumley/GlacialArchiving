using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBADev.MVC.DataAccess.Repositories;
using TBADev.MVC.Security;
using GlacialArchiving.DataAccess.Base;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;

namespace GlacialArchiving.DataAccess.Repositories
{
    public partial class UserRepository : GenericRepository<User>, ITBADevUserRepository
    {
        public UserRepository()
            : this(new DataContext())
        {
        }
        public UserRepository(DataContext context)
            : base(context)
        {
        }
        
        public virtual User GetByEmailAddress(string email)
        {
            List<User> users = this.Get(x => x.EmailAddress == email, null, null).ToList<User>();
            if (users.Count > 0)
                return users[0];
            return null;
        }
        public virtual bool IsUniqueEmailAddress(string email, User currentUser)
        {
            User dbUser = this.GetByEmailAddress(email);
            if (dbUser != null && (currentUser == null || dbUser.Id != currentUser.Id))
                return false;
            return true;
        }
        public virtual ITBADevUser GetByUserName(string userName)
        {
            return this.GetByEmailAddress(userName);
        }
        public bool ValidateLogin(string username, string password, string encodedPassword, bool recordLogin)
        {
            User u = this.GetByEmailAddress(username);
            if (u == null)
                return false;

            if (u.Password == password)
                u.Password = encodedPassword;

            if (u.Password == encodedPassword && u.Enabled)
            {
                if (recordLogin)
                {
                    u.LastLoginDate = DateTime.Now;
                    this.context.SaveChanges();
                }
                return true;
            }

            return false;
        }
    }
}
