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
    public partial class FileTypeRepository : GenericRepository<FileType>
    {
        public FileTypeRepository(DataContext context)
            : base(context)
        {
        }
        
		public FileType GetByExtension(string extension)
        {
            IList<FileType> collection = this.Get(x => string.Compare(extension, x.Extension, true) == 0);
            if (collection.Count == 0)
                return null;
            return collection[0];
        }
    }
}