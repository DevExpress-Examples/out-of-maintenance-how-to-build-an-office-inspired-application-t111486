using System;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;
using PersonalOrganizer.Common.Utils;
using PersonalOrganizer.Model;
using PersonalOrganizer.Common.DataModel;
using PersonalOrganizer.Common.DataModel.EntityFramework;

namespace PersonalOrganizer.ContactContextDataModel {
    public class ContactRepository : DbRepository<Contact, int, ContactContext>, IContactRepository {
        public ContactRepository(DbUnitOfWork<ContactContext> unitOfWork)
            : base(unitOfWork, context => context.Set<Contact>(), x => x.Id) {
        }
    }
}