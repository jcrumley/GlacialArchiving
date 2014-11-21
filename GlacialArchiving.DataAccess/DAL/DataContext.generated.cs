using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Objects;
using System.Linq;
using System.Web;
using GlacialArchiving.DataAccess.Models;

namespace GlacialArchiving.DataAccess.DAL
{
    public partial class DataContext : DbContext
    {
        #region Section 'DbSets'
        public DbSet<GlacialArchiving.DataAccess.Models.Setting> Settings { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.User> Users { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.UserGroup> UserGroups { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.Membership> Memberships { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.Navigation> Navigations { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.NavItem> NavItems { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.NavSubItem> NavSubItems { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.SimpleHTMLPage> SimpleHTMLPages { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.NavIcon> NavIcons { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.StoredFile> StoredFile { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.FileType> FileTypes { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.FileData> FileData { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.HistoryLog> HistoryLogs { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.UploadedItem> UploadedItems { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.PayloadType> PayloadTypes { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.Replacement> Replacements { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.CustomerCoverage> CustomerCoverages { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.TradingUnitCoverage> TradingUnitCoverages { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.DataTag> DataTags { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.Client> Clients { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.fred> freds { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.UploadedItemValidation> UploadedItemValidations { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.ValidationError> ValidationErrors { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.GlacierStorage> GlacierStorages { get; set; }
        public DbSet<GlacialArchiving.DataAccess.Models.UploadedItemStatus> UploadedItemStatuss { get; set; }
        #endregion
        
        public DataContext()
            : base() //@"Data Source=CYPTBADEV1\TBADEV2008;Initial Catalog=GlacialArchiving;Persist Security Info=True;User ID=GlacialArchiving;Password=Glacial45Archiving!!;MultipleActiveResultSets=True;")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.BeforeModelCreate(modelBuilder);
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<User>()
                .HasOptional(u => u.CreatedBy).WithMany();

            modelBuilder.Entity<User>()
                .HasOptional(u => u.ModifiedBy).WithMany();
                
                
            this.AfterModelCreate(modelBuilder);
        }
        partial void BeforeModelCreate(DbModelBuilder modelBuilder);
        partial void AfterModelCreate(DbModelBuilder modelBuilder);
    }
}
