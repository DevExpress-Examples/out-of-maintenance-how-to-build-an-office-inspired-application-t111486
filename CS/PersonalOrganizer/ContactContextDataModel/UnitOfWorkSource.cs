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
using DevExpress.Mvvm;

namespace PersonalOrganizer.ContactContextDataModel {
    public static class UnitOfWorkSource {
        #region inner classes
        class DbUnitOfWorkFactory : IUnitOfWorkFactory {
            public static readonly IUnitOfWorkFactory Instance = new DbUnitOfWorkFactory();
            DbUnitOfWorkFactory() { }
            IContactContextUnitOfWork IUnitOfWorkFactory<IContactContextUnitOfWork>.CreateUnitOfWork() {
                return new ContactContextUnitOfWork(new ContactContext());
            }
        }
        class DesignUnitOfWorkFactory : IUnitOfWorkFactory {
            public static readonly IUnitOfWorkFactory Instance = new DesignUnitOfWorkFactory();

            readonly IContactContextUnitOfWork UnitOfWork = new ContactContextDesignTimeUnitOfWork();
            DesignUnitOfWorkFactory() { }
            IContactContextUnitOfWork IUnitOfWorkFactory<IContactContextUnitOfWork>.CreateUnitOfWork() {
                return UnitOfWork;
            }
        }
        #endregion
        public static IUnitOfWorkFactory GetUnitOfWorkFactory() {
            return GetUnitOfWorkFactory(ViewModelBase.IsInDesignMode);
        }
        public static IUnitOfWorkFactory GetUnitOfWorkFactory(bool isInDesignTime) {
            return isInDesignTime ? DesignUnitOfWorkFactory.Instance : DbUnitOfWorkFactory.Instance;
        }
        public static IContactContextUnitOfWork CreateUnitOfWork(bool isInDesignTime = false) {
            return GetUnitOfWorkFactory(isInDesignTime).CreateUnitOfWork();
        }
    }
}