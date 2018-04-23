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
    public class ContactContextDesignTimeUnitOfWork : DesignTimeUnitOfWork, IContactContextUnitOfWork {
        static DesignTimeContactRepository contactsRepository = new DesignTimeContactRepository();

        public ContactContextDesignTimeUnitOfWork() {
        }

        bool IContactContextUnitOfWork.HasChanges() {
            return false;
        }
        IContactRepository IContactContextUnitOfWork.Contacts {
            get { return contactsRepository; }
        }
        public void Detach(object entity) {
        }
    }
}
