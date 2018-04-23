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
    public interface IUnitOfWorkFactory : IUnitOfWorkFactory<IContactContextUnitOfWork> {
    }
}