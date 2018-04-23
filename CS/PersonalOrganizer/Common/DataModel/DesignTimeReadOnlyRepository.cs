using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;

namespace PersonalOrganizer.Common.DataModel {
    public abstract class DesignTimeReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class {
        IQueryable<TEntity> queryableEntities;

        protected virtual IQueryable<TEntity> GetEntitiesCore() {
            if(queryableEntities == null)
                queryableEntities = DesignTimeHelper.CreateDesignTimeObjects<TEntity>(2).AsQueryable();
            return queryableEntities;
        }
        #region IReadOnlyRepository
        IQueryable<TEntity> IReadOnlyRepository<TEntity>.GetEntities() {
            return GetEntitiesCore();
        }
        IUnitOfWork IReadOnlyRepository<TEntity>.UnitOfWork {
            get { return DesignTimeUnitOfWork.Instance; }
        }
        ObservableCollection<TEntity> IReadOnlyRepository<TEntity>.Local {
            get { return new ObservableCollection<TEntity>(GetEntitiesCore()); }
        }
        #endregion
    }
}