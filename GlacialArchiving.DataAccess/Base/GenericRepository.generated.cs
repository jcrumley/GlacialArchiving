using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GlacialArchiving.DataAccess.Models;
using TBADev.MVC.Entity;

//found at : http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
namespace GlacialArchiving.DataAccess.Base
{
    public partial class GenericRepository<TEntity> where TEntity : class, ITBAEntity
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;

        public virtual IQueryable<TEntity> All
        {
            get { return context.Set<TEntity>(); }
        }

        public GenericRepository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IList<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
                return orderBy(query).ToList();
            else
                return query.ToList();
        }
        public IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);
            return query;
        }

        public virtual TEntity Find(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void BeforeInsertOrUpdate(TEntity entity, User currentUser) { }
        public virtual void AfterInsertOrUpdate(TEntity entity, User currentUser) { }
        
        public virtual void InsertOrUpdate(TEntity entity, User currentUser)
        {
            this.BeforeInsertOrUpdate(entity, currentUser);
            if (entity.Id == default(int))  // New entity
            {
                if (entity is Trackable)
                {
                    if (currentUser != null)
                        (entity as Trackable).CreatedBy_UserId = currentUser.Id;
                    (entity as Trackable).CreatedOn = DateTime.Now;
                }
                dbSet.Add(entity);
            }
            else   // Existing entity
            {
                TEntity oldEntity = this.Find(entity.Id);

                if (entity is Trackable)
                {
                    if (oldEntity != null)
                    {
                        (entity as Trackable).CreatedBy_UserId = (oldEntity as Trackable).CreatedBy_UserId;
                        (entity as Trackable).CreatedOn = (oldEntity as Trackable).CreatedOn;
                    }

                    if (currentUser != null)
                        (entity as Trackable).ModifiedBy_UserId = currentUser.Id;
                    (entity as Trackable).ModifiedOn = DateTime.Now;
                }
                
                if (context.Entry(entity).State == System.Data.Entity.EntityState.Detached)
                {
                    if (oldEntity != null)
                    {
                        context.Entry(oldEntity).CurrentValues.SetValues(entity);
                        context.Entry(oldEntity).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        dbSet.Attach(entity);
                        context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                else
                {
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                }
            }
            this.AfterInsertOrUpdate(entity, currentUser);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (entityToDelete == null)
                throw new Exception(string.Format("Object with id : {0}, not found", id));
            this.Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == System.Data.Entity.EntityState.Detached)
                dbSet.Attach(entityToDelete);
            dbSet.Remove(entityToDelete);
        }
    }
}