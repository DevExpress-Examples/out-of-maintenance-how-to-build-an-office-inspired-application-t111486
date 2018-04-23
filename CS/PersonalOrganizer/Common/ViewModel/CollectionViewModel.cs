using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using PersonalOrganizer.Common.Utils;
using PersonalOrganizer.Common.DataModel;
using MessageBoxButton = System.Windows.MessageBoxButton;
using MessageBoxImage = System.Windows.MessageBoxImage;
using MessageBoxResult = System.Windows.MessageBoxResult;

namespace PersonalOrganizer.Common.ViewModel {
    public abstract partial class CollectionViewModel<TEntity, TPrimaryKey> : CollectionViewModelBase<TEntity, TPrimaryKey> where TEntity : class {
        protected CollectionViewModel(Expression<Func<TEntity, bool>> filterExpression = null)
            : base(filterExpression) {
        }
    }
    public abstract class CollectionViewModelBase<TEntity, TPrimaryKey> : ReadOnlyCollectionViewModel<TEntity> where TEntity : class {
        protected CollectionViewModelBase(Expression<Func<TEntity, bool>> filterExpression)
            : base(filterExpression) {
        }
        protected override void OnInitializeInRuntime() {
            base.OnInitializeInRuntime();
            Messenger.Default.Register<EntityMessage<TEntity>>(this, x => OnMessage(x));
        }

        protected new IRepository<TEntity, TPrimaryKey> Repository { get { return (IRepository<TEntity, TPrimaryKey>)base.Repository; } }

        [Required]
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }
        [ServiceProperty(ServiceSearchMode.PreferParents)]
        protected virtual IDocumentManagerService DocumentManagerService { get { return null; } }

        public virtual void New() {
            IDocument document = CreateDocument(null);
            if(document != null)
                document.Show();
        }
        public virtual void Edit(TEntity entity) {
            TPrimaryKey primaryKey = GetPrimaryKey(entity);
            entity = Repository.Reload(entity);
            if(entity == null || Repository.UnitOfWork.GetState(entity) == EntityState.Detached) {
                DestroyDocument(FindEntityDocument(primaryKey));
                return;
            }
            ShowDocument(GetPrimaryKey(entity));
        }
        public bool CanEdit(TEntity entity) {
            return entity != null;
        }
        public void Delete(TEntity entity) {
            if(MessageBoxService.Show(string.Format("Do you want to delete this {0}?", typeof(TEntity).Name), "Confirmation", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            try {
                Entities.Remove(entity);
                Repository.Remove(entity);
                Repository.UnitOfWork.SaveChanges();
                Messenger.Default.Send(new EntityMessage<TEntity>(entity, EntityMessageType.Deleted));
            } catch(DbException e) {
                Refresh();
                MessageBoxService.Show(e.ErrorMessage, e.ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public bool CanDelete(TEntity entity) {
            return entity != null;
        }
        public override void Refresh() {
            TEntity entity = SelectedEntity;
            base.Refresh();
            if(entity != null && EntityHasPrimaryKey(entity))
                SelectedEntity = FindEntity(GetPrimaryKey(entity));
        }
        [Display(AutoGenerateField = false)]
        public void Save(TEntity entity) {
            try {
                Repository.UnitOfWork.Update(entity);
                Repository.UnitOfWork.SaveChanges();
                Messenger.Default.Send(new EntityMessage<TEntity>(entity, EntityMessageType.Changed));
            } catch(DbException e) {
                MessageBoxService.Show(e.ErrorMessage, e.ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public bool CanSave(TEntity entity) {
            return entity != null;
        }
        [Display(AutoGenerateField = false)]
        public virtual void UpdateSelectedEntity() {
            this.RaisePropertyChanged(x => x.SelectedEntity);
        }

        void OnMessage(EntityMessage<TEntity> message) {
            if(!EntityHasPrimaryKey(message.Entity))
                return;
            TPrimaryKey key = GetPrimaryKey(message.Entity);
            switch(message.MessageType) {
                case EntityMessageType.Added:
                    OnEntityAdded(key);
                    break;
                case EntityMessageType.Changed:
                    OnEntityChanged(key);
                    break;
                case EntityMessageType.Deleted:
                    OnEntityDeleted(key);
                    break;
            }
        }
        protected virtual TEntity OnEntityAdded(TPrimaryKey key) {
            return FindEntity(key);
        }
        protected virtual TEntity OnEntityChanged(TPrimaryKey key) {
            TEntity entity = FindEntity(key);
            if(entity == null) return null;
            entity = Repository.Reload(entity);
            int index = Repository.Local.IndexOf(entity);
            if(index >= 0)
                Repository.Local.Move(index, index);
            if(object.ReferenceEquals(entity, SelectedEntity))
                UpdateSelectedEntity();
            return entity;
        }
        protected virtual void OnEntityDeleted(TPrimaryKey key) {
            TEntity entity = Repository.Local.FirstOrDefault(x => object.Equals(Repository.GetPrimaryKey(x), key));
            if(entity != null) {
                Repository.Remove(entity);
                Repository.UnitOfWork.Detach(entity);
            }
        }
        protected TEntity FindEntity(TPrimaryKey key) {
            if(FilterExpression == null)
                return Repository.Find(key);
            return Repository.GetEntities().Where(Repository.GetPrimaryKeyExpression.ValueEquals(key)).Where(FilterExpression).FirstOrDefault();
        }

        protected override void OnSelectedEntityChanged() {
            base.OnSelectedEntityChanged();
            this.RaiseCanExecuteChanged(x => x.Edit(SelectedEntity));
            this.RaiseCanExecuteChanged(x => x.Delete(SelectedEntity));
            this.RaiseCanExecuteChanged(x => x.Save(SelectedEntity));
        }
        protected virtual bool EntityHasPrimaryKey(TEntity entity) { return true; }
        protected virtual TPrimaryKey GetPrimaryKey(TEntity entity) {
            return Repository.GetPrimaryKey(entity);
        }

        void ShowDocument(TPrimaryKey key) {
            IDocument document = FindEntityDocument(key) ?? CreateDocument(key);
            if(document != null)
                document.Show();
        }
        protected virtual IDocument CreateDocument(object parameter) {
            if(DocumentManagerService == null) return null;
            return DocumentManagerService.CreateDocument(typeof(TEntity).Name + "View", parameter, this);
        }
        protected void DestroyDocument(IDocument document) {
            if(document != null)
                document.Close();
        }
        protected IDocument FindEntityDocument(TPrimaryKey key) {
            if(DocumentManagerService == null) return null;
            foreach(IDocument document in DocumentManagerService.Documents) {
                ISingleObjectViewModel<TEntity, TPrimaryKey> entityViewModel = document.Content as ISingleObjectViewModel<TEntity, TPrimaryKey>;
                if(entityViewModel != null && object.Equals(entityViewModel.PrimaryKey, key))
                    return document;
            }
            return null;
        }
    }
}