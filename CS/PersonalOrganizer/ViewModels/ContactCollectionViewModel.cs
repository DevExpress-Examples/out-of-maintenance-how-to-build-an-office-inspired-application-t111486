using PersonalOrganizer.Common.DataModel;
using PersonalOrganizer.Common.ViewModel;
using PersonalOrganizer.ContactContextDataModel;
using PersonalOrganizer.Model;

namespace PersonalOrganizer.ViewModels {
    public partial class ContactCollectionViewModel : CollectionViewModel<Contact, int> {
        readonly IUnitOfWorkFactory unitOfWorkFactory;

        public ContactCollectionViewModel()
            : this(UnitOfWorkSource.GetUnitOfWorkFactory()) {
        }
        public ContactCollectionViewModel(IUnitOfWorkFactory unitOfWorkFactory) {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }
        protected override IReadOnlyRepository<Contact> GetRepository() {
            return unitOfWorkFactory.CreateUnitOfWork().Contacts;
        }
    }
}