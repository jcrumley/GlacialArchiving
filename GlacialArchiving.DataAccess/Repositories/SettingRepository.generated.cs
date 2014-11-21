using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBADev.MVC.DataAccess.Repositories;
using GlacialArchiving.DataAccess.Base;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;

namespace GlacialArchiving.DataAccess.Repositories
{
    public partial class SettingRepository : GenericRepository<Setting>
    {
        public SettingRepository()
            : this(new DataContext())
        {
        }
        public SettingRepository(DataContext context)
            : base(context)
        {
        }
        
        private void UpdateValue(Setting s, string data)
        {
            s.Data = data;
            this.InsertOrUpdate(s, null);
        }
        
        #region Setting 'ErrorEmail'
        public string ErrorEmail
        {
            get
			{
				Setting s = this.Find((int)SettingEnum.ErrorEmail);
                if (s == null) throw new Exception("Setting : ErrorEmail is not a valid setting in the database.");
				if (s.Data == null) return null;
                return s.Data.Replace("\\n", "<br />");
			}
			set
			{
				Setting s = this.Find((int)SettingEnum.ErrorEmail);
                if (s == null) throw new Exception("Setting : ErrorEmail is not a valid setting in the database.");
                this.UpdateValue(s, value);
			}
        }
        #endregion
        #region Setting 'WebUserId'
        public int WebUserId
        {
            get
			{
				Setting s = this.Find((int)SettingEnum.WebUserId);
                if (s == null) throw new Exception("Setting : WebUserId is not a valid setting in the database.");
				if (s.Data == null) throw new Exception("Setting : WebUserId is set to NULL in the database, which is not a valid value.");
                return Convert.ToInt32(s.Data);
			}
			set
			{
				Setting s = this.Find((int)SettingEnum.WebUserId);
                if (s == null) throw new Exception("Setting : WebUserId is not a valid setting in the database.");
                this.UpdateValue(s, value.ToString());
			}
}
        #endregion
    }
}
