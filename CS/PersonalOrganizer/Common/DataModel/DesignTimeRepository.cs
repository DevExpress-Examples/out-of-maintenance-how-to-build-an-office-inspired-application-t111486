using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using DevExpress.Mvvm;
using PersonalOrganizer.Common.Utils;

namespace PersonalOrganizer.Common.DataModel {
    public abstract class DesignTimeRepository<TEntity, TPrimaryKey> : DesignTimeReadOnlyRepository<TEntity>, IRepository<TEntity, TPrimaryKey>
        where TEntity : class {
        Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression;
        Func<TEntity, TPrimaryKey> getPrimaryKeyFunction;
        Action<TEntity, TPrimaryKey> setPrimaryKeyAction;

        public DesignTimeRepository(Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression, Action<TEntity, TPrimaryKey> setPrimaryKeyAction = null) {
            this.getPrimaryKeyExpression = getPrimaryKeyExpression;
            this.setPrimaryKeyAction = setPrimaryKeyAction;
            if(this.setPrimaryKeyAction == null)
                this.setPrimaryKeyAction = getPrimaryKeyExpression.SetValueAction().Compile();
        }
        protected virtual TEntity CreateCore() {
            return DesignTimeHelper.CreateDesignTimeObject<TEntity>();
        }
        protected virtual TEntity FindCore(TPrimaryKey key) {
            throw new InvalidOperationException();
        }
        protected virtual void RemoveCore(TEntity entity) {
            throw new InvalidOperationException();
        }
        protected virtual TEntity ReloadCore(TEntity entity) {
            throw new InvalidOperationException();
        }
        protected virtual TPrimaryKey GetPrimaryKeyCore(TEntity entity) {
            if(getPrimaryKeyFunction == null)
                getPrimaryKeyFunction = getPrimaryKeyExpression.Compile();
            return getPrimaryKeyFunction(entity);
        }
        protected virtual void SetPrimaryKeyCore(TEntity entity, TPrimaryKey key) {
            setPrimaryKeyAction(entity, key);
        }
        #region IRepository
        TEntity IRepository<TEntity, TPrimaryKey>.Find(TPrimaryKey key) {
            return FindCore(key);
        }
        void IRepository<TEntity, TPrimaryKey>.Remove(TEntity entity) {
            RemoveCore(entity);
        }
        TEntity IRepository<TEntity, TPrimaryKey>.Create() {
            return CreateCore();
        }
        TEntity IRepository<TEntity, TPrimaryKey>.Reload(TEntity entity) {
            return ReloadCore(entity);
        }
        Expression<Func<TEntity, TPrimaryKey>> IRepository<TEntity, TPrimaryKey>.GetPrimaryKeyExpression {
            get { return getPrimaryKeyExpression; }
        }
        TPrimaryKey IRepository<TEntity, TPrimaryKey>.GetPrimaryKey(TEntity entity) {
            return GetPrimaryKeyCore(entity);
        }
        void IRepository<TEntity, TPrimaryKey>.SetPrimaryKey(TEntity entity, TPrimaryKey key) {
            SetPrimaryKeyCore(entity, key);
        }
        #endregion
    }
}