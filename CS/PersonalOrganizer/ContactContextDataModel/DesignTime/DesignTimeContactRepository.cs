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
    public class DesignTimeContactRepository : DesignTimeRepository<Contact, int>, IContactRepository {
        public DesignTimeContactRepository()
            : base(x => x.Id) {
        }
    }
}