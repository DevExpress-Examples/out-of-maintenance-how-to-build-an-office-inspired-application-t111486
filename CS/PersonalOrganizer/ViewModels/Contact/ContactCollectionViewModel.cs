using System;
using System.Linq;
using DevExpress.Mvvm.POCO;
using PersonalOrganizer.Common.Utils;
using PersonalOrganizer.ContactContextDataModel;
using PersonalOrganizer.Common.DataModel;
using PersonalOrganizer.Model;
using PersonalOrganizer.Common.ViewModel;

namespace PersonalOrganizer.ViewModels {
    /// <summary>
    /// Represents the Contacts collection view model.
    /// </summary>
    public partial class ContactCollectionViewModel : CollectionViewModel<Contact, int, IContactContextUnitOfWork> {

        /// <summary>
        /// Creates a new instance of ContactCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static ContactCollectionViewModel Create(IUnitOfWorkFactory<IContactContextUnitOfWork> unitOfWorkFactory = null) {
            return ViewModelSource.Create(() => new ContactCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the ContactCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the ContactCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected ContactCollectionViewModel(IUnitOfWorkFactory<IContactContextUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Contacts) {
        }
    }
}