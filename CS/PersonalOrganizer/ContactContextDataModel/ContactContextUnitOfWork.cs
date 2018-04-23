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
    /// <summary>
    /// A ContactContextUnitOfWork instance that represents the run-time implementation of the IContactContextUnitOfWork interface.
    /// </summary>
    public class ContactContextUnitOfWork : DbUnitOfWork<ContactContext>, IContactContextUnitOfWork {

        public ContactContextUnitOfWork(Func<ContactContext> contextFactory)
            : base(contextFactory) {
        }

        IRepository<Contact, int> IContactContextUnitOfWork.Contacts {
            get { return GetRepository(x => x.Set<Contact>(), x => x.Id); }
        }
    }
}
