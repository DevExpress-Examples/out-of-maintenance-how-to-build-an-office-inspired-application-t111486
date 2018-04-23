using System;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;
using PersonalOrganizer.Common.Utils;
using PersonalOrganizer.Common.DataModel;
using PersonalOrganizer.Common.DataModel.EntityFramework;
using PersonalOrganizer.Model;

namespace PersonalOrganizer.ContactContextDataModel {
    public class ContactContextUnitOfWork : DbUnitOfWork<ContactContext>, IContactContextUnitOfWork {
        Lazy<IContactRepository> contactsRepository;

        public ContactContextUnitOfWork(ContactContext context)
            : base(context) {
            contactsRepository = new Lazy<IContactRepository>(() => new ContactRepository(this));
        }
        bool IContactContextUnitOfWork.HasChanges() {
            return Context.ChangeTracker.HasChanges();
        }
        IContactRepository IContactContextUnitOfWork.Contacts {
            get { return contactsRepository.Value; }
        }
    }
}
