using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using PersonalOrganizer.Common.Utils;
using PersonalOrganizer.Common.DataModel;

namespace PersonalOrganizer.Common.ViewModel {
    public abstract partial class ReadOnlyCollectionViewModel<TEntity> : ReadOnlyCollectionViewModelBase<TEntity> where TEntity : class {
        public ReadOnlyCollectionViewModel(Expression<Func<TEntity, bool>> filterExpression = null)
            : base(filterExpression) {
        }
    }
    [POCOViewModel]
    public abstract class ReadOnlyCollectionViewModelBase<TEntity> where TEntity : class {
        bool refreshOnFilterExpressionChanged = false;
        IReadOnlyRepository<TEntity> repository;
        IList<TEntity> entities;

        public ReadOnlyCollectionViewModelBase(Expression<Func<TEntity, bool>> filterExpression) {
            FilterExpression = filterExpression;
            this.refreshOnFilterExpressionChanged = true;
            if(!this.IsInDesignMode())
                OnInitializeInRuntime();
        }
        protected virtual void OnInitializeInRuntime() { }
        public virtual void Refresh() {
            this.repository = GetRepository();
            this.entities = GetEntities();
            this.RaisePropertyChanged(x => Entities);
        }

        protected IReadOnlyRepository<TEntity> Repository {
            get {
                if(repository == null)
                    repository = GetRepository();
                return repository;
            }
        }
        public IList<TEntity> Entities {
            get {
                if(entities == null)
                    entities = GetEntities();
                return entities;
            }
        }
        public virtual TEntity SelectedEntity { get; set; }
        protected virtual void OnSelectedEntityChanged() { }
        public virtual Expression<Func<TEntity, bool>> FilterExpression { get; set; }
        protected virtual void OnFilterExpressionChanged() {
            if(refreshOnFilterExpressionChanged)
                Refresh();
        }
        protected abstract IReadOnlyRepository<TEntity> GetRepository();
        protected virtual IList<TEntity> GetEntities() {
            var queryable = Repository.GetEntities();
            if(FilterExpression != null)
                queryable = queryable.Where(FilterExpression);
            queryable.Load();
            return Repository.Local;
        }
    }
}