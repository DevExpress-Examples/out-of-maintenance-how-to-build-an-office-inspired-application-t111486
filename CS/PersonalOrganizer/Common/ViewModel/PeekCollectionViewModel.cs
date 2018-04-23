using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using PersonalOrganizer.Common.Utils;
using PersonalOrganizer.Common.DataModel;

namespace PersonalOrganizer.Common.ViewModel {
    public partial class PeekCollectionViewModel<TNavigationToken, TEntity, TPrimaryKey, TUnitOfWork> : CollectionViewModelBase<TEntity, TEntity, TPrimaryKey, TUnitOfWork>
        where TEntity : class
        where TUnitOfWork : IUnitOfWork {

        public static PeekCollectionViewModel<TNavigationToken, TEntity, TPrimaryKey, TUnitOfWork> Create(
            TNavigationToken navigationToken,
            IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory,
            Func<TUnitOfWork, IRepository<TEntity, TPrimaryKey>> getRepositoryFunc,
            Func<IRepositoryQuery<TEntity>, IQueryable<TEntity>> projection = null) {
            return ViewModelSource.Create(() => new PeekCollectionViewModel<TNavigationToken, TEntity, TPrimaryKey, TUnitOfWork>(navigationToken, unitOfWorkFactory, getRepositoryFunc, projection));
        }

        TNavigationToken navigationToken;
        TEntity pickedEntity;

        protected PeekCollectionViewModel(
            TNavigationToken navigationToken,
            IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory,
            Func<TUnitOfWork, IRepository<TEntity, TPrimaryKey>> getRepositoryFunc,
            Func<IRepositoryQuery<TEntity>, IQueryable<TEntity>> projection = null
            ) : base(unitOfWorkFactory, getRepositoryFunc, projection, null, true) {
            this.navigationToken = navigationToken;
        }

        [Display(AutoGenerateField = false)]
        public void Navigate(TEntity projectionEntity) {
            pickedEntity = projectionEntity;
            SendSelectEntityMessage();
            Messenger.Default.Send(new NavigateMessage<TNavigationToken>(navigationToken), navigationToken);
        }

        public bool CanNavigate(TEntity projectionEntity) {
            return projectionEntity != null;
        }

        protected override void OnInitializeInRuntime() {
            base.OnInitializeInRuntime();
            Messenger.Default.Register<SelectedEntityRequest>(this, x => SendSelectEntityMessage());
        }

        void SendSelectEntityMessage() {
            if(IsLoaded && pickedEntity != null)
                Messenger.Default.Send(new SelectEntityMessage(CreateRepository().GetProjectionPrimaryKey(pickedEntity)));
        }
    }
}