using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using PersonalOrganizer.Common.Utils;
using PersonalOrganizer.Common.DataModel;

namespace PersonalOrganizer.Common.ViewModel {
    public abstract class DocumentsViewModel<TModule, TUnitOfWork>
        where TModule : ModuleDescription<TModule>
        where TUnitOfWork : IUnitOfWork {

        protected readonly IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory;

        protected DocumentsViewModel(IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory) {
            this.unitOfWorkFactory = unitOfWorkFactory;
            Modules = CreateModules().ToArray();
            foreach(var module in Modules)
                Messenger.Default.Register<NavigateMessage<TModule>>(this, module, x => Show(x.Token));
        }

        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }

        protected IDocumentManagerService WorkspaceDocumentManagerService { get { return this.GetService<IDocumentManagerService>("WorkspaceDocumentManagerService"); } }

        public TModule[] Modules { get; private set; }

        protected virtual TModule DefaultModule { get { return Modules.First(); } }

        public virtual TModule SelectedModule { get; set; }

        public virtual TModule ActiveModule { get; protected set; }

        public void SaveAll() {
            Messenger.Default.Send(new SaveAllMessage());
        }

        public void OnClosing(CancelEventArgs cancelEventArgs) {
            Messenger.Default.Send(new CloseAllMessage(cancelEventArgs));
        }

        public void Show(TModule module) {
            if(module == null || DocumentManagerService == null)
                return;
            IDocument document = DocumentManagerService.FindDocumentByIdOrCreate(module, x => CreateDocument(module));
            document.Show();
        }

        protected bool IsLoaded { get; private set; }

        public virtual void OnLoaded() {
            IsLoaded = true;
            DocumentManagerService.ActiveDocumentChanged += OnActiveDocumentChanged;
            Show(DefaultModule);
        }

        void OnActiveDocumentChanged(object sender, ActiveDocumentChangedEventArgs e) {
            ActiveModule = e.NewDocument == null ? null : e.NewDocument.Id as TModule;
        }

        protected virtual void OnSelectedModuleChanged(TModule oldModule) {
            if(IsLoaded)
                Show(SelectedModule);
        }

        protected virtual void OnActiveModuleChanged(TModule oldModule) {
            SelectedModule = ActiveModule;
        }

        IDocument CreateDocument(TModule module) {
            var document = DocumentManagerService.CreateDocument(module.DocumentType, null, this);
            document.Title = GetModuleTitle(module);
            document.DestroyOnClose = false;
            return document;
        }

        protected virtual string GetModuleTitle(TModule module) {
            return module.ModuleTitle;
        }

        public void PinPeekCollectionView(TModule module) {
            if(WorkspaceDocumentManagerService == null)
                return;
            IDocument document = WorkspaceDocumentManagerService.FindDocumentByIdOrCreate(module, x => CreatePinnedPeekCollectionDocument(module));
            document.Show();
        }

        IDocument CreatePinnedPeekCollectionDocument(TModule module) {
            var document = WorkspaceDocumentManagerService.CreateDocument("PeekCollectionView", module.CreatePeekCollectionViewModel());
            document.Title = module.ModuleTitle;
            return document;
        }

        protected Func<TModule, object> GetPeekCollectionViewModelFactory<TEntity, TPrimaryKey>(Func<TUnitOfWork, IRepository<TEntity, TPrimaryKey>> getRepositoryFunc) where TEntity : class {
            return module => PeekCollectionViewModel<TModule, TEntity, TPrimaryKey, TUnitOfWork>.Create(module, unitOfWorkFactory, getRepositoryFunc).SetParentViewModel(this);
        }

        protected abstract TModule[] CreateModules();

        protected TUnitOfWork CreateUnitOfWork() {
            return unitOfWorkFactory.CreateUnitOfWork();
        }

        public virtual NavigationPaneVisibility NavigationPaneVisibility { get; set; }
    }

    public abstract partial class ModuleDescription<TModule> where TModule : ModuleDescription<TModule> {
        Func<TModule, object> peekCollectionViewModelFactory;

        object peekCollectionViewModel;

        protected ModuleDescription(string title, string documentType, string group, Func<TModule, object> peekCollectionViewModelFactory) {
            ModuleTitle = title;
            ModuleGroup = group;
            DocumentType = documentType;
            this.peekCollectionViewModelFactory = peekCollectionViewModelFactory;
        }

        public string ModuleTitle { get; private set; }

        public string ModuleGroup { get; private set; }

        public string DocumentType { get; private set; }

        public object PeekCollectionViewModel {
            get {
                if(peekCollectionViewModelFactory == null)
                    return null;
                if(peekCollectionViewModel == null)
                    peekCollectionViewModel = CreatePeekCollectionViewModel();
                return peekCollectionViewModel;
            }
        }

        public object CreatePeekCollectionViewModel() {
            return peekCollectionViewModelFactory((TModule)this);
        }
    }

    public enum NavigationPaneVisibility {
        Minimized,
        Normal,
        Off
    }
}