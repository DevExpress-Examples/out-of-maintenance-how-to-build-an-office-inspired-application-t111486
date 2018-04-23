using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PersonalOrganizer.Common.DataModel.EntityFramework {
    public abstract class DbReadOnlyRepository<TEntity, TDbContext> : IReadOnlyRepository<TEntity>
        where TEntity : class
        where TDbContext : DbContext {
        readonly Func<TDbContext, DbSet<TEntity>> dbSetAccessor;
        readonly DbUnitOfWork<TDbContext> unitOfWork;
        DbSet<TEntity> dbSet;

        public DbReadOnlyRepository(DbUnitOfWork<TDbContext> unitOfWork, Func<TDbContext, DbSet<TEntity>> dbSetAccessor) {
            this.dbSetAccessor = dbSetAccessor;
            this.unitOfWork = unitOfWork;
        }
        protected DbSet<TEntity> DbSet {
            get {
                if(dbSet == null) {
                    dbSet = dbSetAccessor(unitOfWork.Context);
                    //dbSet.Load();
                }
                return dbSet;
            }
        }
        protected TDbContext Context {
            get { return unitOfWork.Context; }
        }
        protected virtual IQueryable<TEntity> GetEntities() {
            return DbSet;
        }
        #region IReadOnlyRepository
        IQueryable<TEntity> IReadOnlyRepository<TEntity>.GetEntities() {
            return GetEntities();
        }
        IUnitOfWork IReadOnlyRepository<TEntity>.UnitOfWork {
            get { return unitOfWork; }
        }
        ObservableCollection<TEntity> IReadOnlyRepository<TEntity>.Local {
            get { return DbSet.Local; }
        }
        #endregion
    }
}
