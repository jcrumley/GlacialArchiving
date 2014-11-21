using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using TBADev.MVC.DataAttributes;
using TBADev.MVC.Entity;
using TBADev.MVC.Tools;
using GlacialArchiving.DataAccess.Base;
using GlacialArchiving.DataAccess.Models;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.DataAccess.DAL
{
    //http://blogs.msdn.com/b/adonet/archive/2009/06/16/using-repository-and-unit-of-work-patterns-with-entity-framework-4-0.aspx
    //http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    //http://code.msdn.microsoft.com/ASPNET-MVC-Application-b01a9fe8/sourcecode?fileId=66049&pathId=1124259665
    public partial class UnitOfWork : IDisposable
    {
        private DataContext context;

        public UnitOfWork()
            : this(new DataContext())
        {
        }
        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        #region Property 'ClientRepository'
        private ClientRepository m_ClientRepository;
        public ClientRepository ClientRepository
        {
            get
            {
                if (this.m_ClientRepository == null)
                    this.m_ClientRepository = new ClientRepository(context);
                return m_ClientRepository;
            }
        }
        #endregion
        #region Property 'CustomerCoverageRepository'
        private CustomerCoverageRepository m_CustomerCoverageRepository;
        public CustomerCoverageRepository CustomerCoverageRepository
        {
            get
            {
                if (this.m_CustomerCoverageRepository == null)
                    this.m_CustomerCoverageRepository = new CustomerCoverageRepository(context);
                return m_CustomerCoverageRepository;
            }
        }
        #endregion
        #region Property 'DataTagRepository'
        private DataTagRepository m_DataTagRepository;
        public DataTagRepository DataTagRepository
        {
            get
            {
                if (this.m_DataTagRepository == null)
                    this.m_DataTagRepository = new DataTagRepository(context);
                return m_DataTagRepository;
            }
        }
        #endregion
        #region Property 'FileDataRepository'
        private FileDataRepository m_FileDataRepository;
        public FileDataRepository FileDataRepository
        {
            get
            {
                if (this.m_FileDataRepository == null)
                    this.m_FileDataRepository = new FileDataRepository(context);
                return m_FileDataRepository;
            }
        }
        #endregion
        #region Property 'FileTypeRepository'
        private FileTypeRepository m_FileTypeRepository;
        public FileTypeRepository FileTypeRepository
        {
            get
            {
                if (this.m_FileTypeRepository == null)
                    this.m_FileTypeRepository = new FileTypeRepository(context);
                return m_FileTypeRepository;
            }
        }
        #endregion
        #region Property 'HistoryLogRepository'
        private HistoryLogRepository m_HistoryLogRepository;
        public HistoryLogRepository HistoryLogRepository
        {
            get
            {
                if (this.m_HistoryLogRepository == null)
                    this.m_HistoryLogRepository = new HistoryLogRepository(context);
                return m_HistoryLogRepository;
            }
        }
        #endregion
        #region Property 'MembershipRepository'
        private MembershipRepository m_MembershipRepository;
        public MembershipRepository MembershipRepository
        {
            get
            {
                if (this.m_MembershipRepository == null)
                    this.m_MembershipRepository = new MembershipRepository(context);
                return m_MembershipRepository;
            }
        }
        #endregion
        #region Property 'NavIconRepository'
        private NavIconRepository m_NavIconRepository;
        public NavIconRepository NavIconRepository
        {
            get
            {
                if (this.m_NavIconRepository == null)
                    this.m_NavIconRepository = new NavIconRepository(context);
                return m_NavIconRepository;
            }
        }
        #endregion
        #region Property 'NavigationRepository'
        private NavigationRepository m_NavigationRepository;
        public NavigationRepository NavigationRepository
        {
            get
            {
                if (this.m_NavigationRepository == null)
                    this.m_NavigationRepository = new NavigationRepository(context);
                return m_NavigationRepository;
            }
        }
        #endregion
        #region Property 'NavItemRepository'
        private NavItemRepository m_NavItemRepository;
        public NavItemRepository NavItemRepository
        {
            get
            {
                if (this.m_NavItemRepository == null)
                    this.m_NavItemRepository = new NavItemRepository(context);
                return m_NavItemRepository;
            }
        }
        #endregion
        #region Property 'NavSubItemRepository'
        private NavSubItemRepository m_NavSubItemRepository;
        public NavSubItemRepository NavSubItemRepository
        {
            get
            {
                if (this.m_NavSubItemRepository == null)
                    this.m_NavSubItemRepository = new NavSubItemRepository(context);
                return m_NavSubItemRepository;
            }
        }
        #endregion
        #region Property 'PayloadTypeRepository'
        private PayloadTypeRepository m_PayloadTypeRepository;
        public PayloadTypeRepository PayloadTypeRepository
        {
            get
            {
                if (this.m_PayloadTypeRepository == null)
                    this.m_PayloadTypeRepository = new PayloadTypeRepository(context);
                return m_PayloadTypeRepository;
            }
        }
        #endregion
        #region Property 'ReplacementRepository'
        private ReplacementRepository m_ReplacementRepository;
        public ReplacementRepository ReplacementRepository
        {
            get
            {
                if (this.m_ReplacementRepository == null)
                    this.m_ReplacementRepository = new ReplacementRepository(context);
                return m_ReplacementRepository;
            }
        }
        #endregion
        #region Property 'SettingRepository'
        private SettingRepository m_SettingRepository;
        public SettingRepository SettingRepository
        {
            get
            {
                if (this.m_SettingRepository == null)
                    this.m_SettingRepository = new SettingRepository(context);
                return m_SettingRepository;
            }
        }
        #endregion
        #region Property 'SimpleHTMLPageRepository'
        private SimpleHTMLPageRepository m_SimpleHTMLPageRepository;
        public SimpleHTMLPageRepository SimpleHTMLPageRepository
        {
            get
            {
                if (this.m_SimpleHTMLPageRepository == null)
                    this.m_SimpleHTMLPageRepository = new SimpleHTMLPageRepository(context);
                return m_SimpleHTMLPageRepository;
            }
        }
        #endregion
        #region Property 'StoredFileRepository'
        private StoredFileRepository m_StoredFileRepository;
        public StoredFileRepository StoredFileRepository
        {
            get
            {
                if (this.m_StoredFileRepository == null)
                    this.m_StoredFileRepository = new StoredFileRepository(context);
                return m_StoredFileRepository;
            }
        }
        #endregion
        #region Property 'TradingUnitCoverageRepository'
        private TradingUnitCoverageRepository m_TradingUnitCoverageRepository;
        public TradingUnitCoverageRepository TradingUnitCoverageRepository
        {
            get
            {
                if (this.m_TradingUnitCoverageRepository == null)
                    this.m_TradingUnitCoverageRepository = new TradingUnitCoverageRepository(context);
                return m_TradingUnitCoverageRepository;
            }
        }
        #endregion
        #region Property 'UploadedItemRepository'
        private UploadedItemRepository m_UploadedItemRepository;
        public UploadedItemRepository UploadedItemRepository
        {
            get
            {
                if (this.m_UploadedItemRepository == null)
                    this.m_UploadedItemRepository = new UploadedItemRepository(context);
                return m_UploadedItemRepository;
            }
        }
        #endregion
        #region Property 'UserRepository'
        private UserRepository m_UserRepository;
        public UserRepository UserRepository
        {
            get
            {
                if (this.m_UserRepository == null)
                    this.m_UserRepository = new UserRepository(context);
                return m_UserRepository;
            }
        }
        #endregion
        #region Property 'UserGroupRepository'
        private UserGroupRepository m_UserGroupRepository;
        public UserGroupRepository UserGroupRepository
        {
            get
            {
                if (this.m_UserGroupRepository == null)
                    this.m_UserGroupRepository = new UserGroupRepository(context);
                return m_UserGroupRepository;
            }
        }
        #endregion
        #region Property 'fredRepository'
        private fredRepository m_fredRepository;
        public fredRepository fredRepository
        {
            get
            {
                if (this.m_fredRepository == null)
                    this.m_fredRepository = new fredRepository(context);
                return m_fredRepository;
            }
        }
        #endregion
        #region Property 'UploadedItemValidationRepository'
        private UploadedItemValidationRepository m_UploadedItemValidationRepository;
        public UploadedItemValidationRepository UploadedItemValidationRepository
        {
            get
            {
                if (this.m_UploadedItemValidationRepository == null)
                    this.m_UploadedItemValidationRepository = new UploadedItemValidationRepository(context);
                return m_UploadedItemValidationRepository;
            }
        }
        #endregion
        #region Property 'ValidationErrorRepository'
        private ValidationErrorRepository m_ValidationErrorRepository;
        public ValidationErrorRepository ValidationErrorRepository
        {
            get
            {
                if (this.m_ValidationErrorRepository == null)
                    this.m_ValidationErrorRepository = new ValidationErrorRepository(context);
                return m_ValidationErrorRepository;
            }
        }
        #endregion
        #region Property 'GlacierStorageRepository'
        private GlacierStorageRepository m_GlacierStorageRepository;
        public GlacierStorageRepository GlacierStorageRepository
        {
            get
            {
                if (this.m_GlacierStorageRepository == null)
                    this.m_GlacierStorageRepository = new GlacierStorageRepository(context);
                return m_GlacierStorageRepository;
            }
        }
        #endregion
        #region Property 'UploadedItemStatusRepository'
        private UploadedItemStatusRepository m_UploadedItemStatusRepository;
        public UploadedItemStatusRepository UploadedItemStatusRepository
        {
            get
            {
                if (this.m_UploadedItemStatusRepository == null)
                    this.m_UploadedItemStatusRepository = new UploadedItemStatusRepository(context);
                return m_UploadedItemStatusRepository;
            }
        }
        #endregion

        public void Save()
        {
            this.TrackChanges();
            context.SaveChanges();
        }
        
        private void TrackChanges()
        {
            //http://www.shujaat.net/2012/03/entity-framework-code-first-change.html
            IEnumerable<DbEntityEntry> entities = this.context.ChangeTracker.Entries();
            foreach (DbEntityEntry entity in entities)
            {
                if (entity.State != EntityState.Modified)
                    continue;

                try
                {
                    #region Initialize variables
                    Type entityType = entity.Entity.GetType().BaseType;
                    int entityId = (entity.Entity as ITBAEntity).Id;

                    int? modifyingUserId = null;
                    if (entity.Entity is Trackable)
                        modifyingUserId = (entity.Entity as Trackable).ModifiedBy_UserId;
                    #endregion

                    //Check to see if object should be tracked
                    var atts = entityType.GetCustomAttributes(typeof(TBADev.MVC.DataAttributes.TrackHistoryAttribute), false);
                    if (atts.Length > 0)
                    {
                        #region Determine new and old values/objects
                        DbPropertyValues oldValues = entity.OriginalValues;
                        object oldObject = entity.OriginalValues.ToObject();

                        DbPropertyValues newValues = entity.CurrentValues;
                        object newObject = entity.CurrentValues.ToObject();
                        #endregion

                        foreach (PropertyInfo pi in entityType.GetProperties())
                        {
                            #region Check each property for changes
                            if (!oldValues.PropertyNames.Contains(pi.Name))
                                continue;

                            if (pi.GetCustomAttribute(typeof(DoNotTrackAttribute), false) != null)
                                continue;

                            object obj1 = oldValues.GetValue<object>(pi.Name);
                            object obj2 = newValues.GetValue<object>(pi.Name);

                            if (obj1 == null && obj2 == null)
                                continue;

                            if ((obj1 == null && obj2 != null) ||
                                (obj1 != null && obj2 == null) ||
                                !obj1.Equals(obj2))
                            {
                                #region Create Log Item
                                HistoryLog item = new HistoryLog();
                                item.Model = entityType.Name;
                                item.ModelId = entityId;

                                DisplayAttribute display = pi.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                                if (display != null)
                                    item.PropertyName = display.Name;
                                else
                                    item.PropertyName = pi.Name;

                                ForeignKeyAttribute fkAttr = pi.GetCustomAttribute(typeof(ForeignKeyAttribute)) as ForeignKeyAttribute;
                                if (fkAttr != null && entityType.GetProperty(fkAttr.Name) != null)
                                {
                                    #region Process for foreign keys
                                    DisplayPropertyAttribute displayProperty = entityType.GetProperty(fkAttr.Name).PropertyType.GetCustomAttribute(typeof(DisplayPropertyAttribute)) as DisplayPropertyAttribute;
                                    string displayProp = (displayProperty != null ? displayProperty.PropertyName : "Id");

                                    if (obj1 != null && (int)obj1 != default(int))
                                    {
                                        PropertyInfo objProp = oldObject.GetType().GetProperty(fkAttr.Name);
                                        object fkObject = this.context.Set(objProp.PropertyType).Find(obj1);

                                        if (fkObject != null)
                                        {
                                            PropertyInfo fkProp = fkObject.GetType().GetProperty(displayProp);
                                            object fkPropVal = fkProp.GetValue(fkObject);
                                            if (fkPropVal != null)
                                                item.OldValue = fkPropVal.ToString();
                                            else
                                                item.OldValue = obj1.ToString();
                                            item.OldModelId = (int)obj1;
                                        }
                                    }

                                    if (obj2 != null && (int)obj2 != default(int))
                                    {
                                        PropertyInfo objProp = oldObject.GetType().GetProperty(fkAttr.Name);
                                        object fkObject = this.context.Set(objProp.PropertyType).Find(obj2);

                                        if (fkObject != null)
                                        {
                                            PropertyInfo fkProp = fkObject.GetType().GetProperty(displayProp);
                                            object fkPropVal = fkProp.GetValue(fkObject);
                                            if (fkPropVal != null)
                                                item.NewValue = fkPropVal.ToString();
                                            else
                                                item.NewValue = obj2.ToString();
                                            item.NewModelId = (int)obj2;
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region Process for regular properties
                                    if (obj1 != null)
                                        item.OldValue = obj1.ToString();
                                    if (obj2 != null)
                                        item.NewValue = obj2.ToString();
                                    #endregion
                                }

                                //Set created on and created by
                                item.CreatedOn = DateTime.Now;
                                item.CreatedBy_UserId = modifyingUserId;

                                this.context.HistoryLogs.Add(item);
                                #endregion
                            }
                            #endregion
                        }
                    }
                }
                catch (Exception ex)
                {
                    Setting errorEmail = this.context.Settings.Where(x => x.SettingId == (int)SettingEnum.ErrorEmail).FirstOrDefault();
                    if (errorEmail != null)
                        Utilities.SendEmail(errorEmail.Data, "Unable to save history log", Utilities.CreateMessage(ex));
                }
            }
        }
        
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
