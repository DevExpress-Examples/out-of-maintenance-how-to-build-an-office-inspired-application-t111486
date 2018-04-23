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
    /// <summary>
    /// Provides methods to obtain the relevant IUnitOfWorkFactory.
    /// </summary>
    public static class UnitOfWorkSource {

        #region inner classes
        class DbUnitOfWorkFactory : IUnitOfWorkFactory<IContactContextUnitOfWork> {
            public static readonly IUnitOfWorkFactory<IContactContextUnitOfWork> Instance = new DbUnitOfWorkFactory();
            DbUnitOfWorkFactory() { }
            IContactContextUnitOfWork IUnitOfWorkFactory<IContactContextUnitOfWork>.CreateUnitOfWork() {
                return new ContactContextUnitOfWork(() => new ContactContext());
            }
        }

        class DesignUnitOfWorkFactory : IUnitOfWorkFactory<IContactContextUnitOfWork> {
            public static readonly IUnitOfWorkFactory<IContactContextUnitOfWork> Instance = new DesignUnitOfWorkFactory();
            DesignUnitOfWorkFactory() { }
            IContactContextUnitOfWork IUnitOfWorkFactory<IContactContextUnitOfWork>.CreateUnitOfWork() {
                return new ContactContextDesignTimeUnitOfWork();
            }
        }
        #endregion

        /// <summary>
        /// Returns the IUnitOfWorkFactory implementation based on the current mode (run-time or design-time).
        /// </summary>
        public static IUnitOfWorkFactory<IContactContextUnitOfWork> GetUnitOfWorkFactory() {
            return GetUnitOfWorkFactory(ViewModelBase.IsInDesignMode);
        }

        /// <summary>
        /// Returns the IUnitOfWorkFactory implementation based on the given mode (run-time or design-time).
        /// </summary>
        /// <param name="isInDesignTime">Used to determine which implementation of IUnitOfWorkFactory should be returned.</param>
        public static IUnitOfWorkFactory<IContactContextUnitOfWork> GetUnitOfWorkFactory(bool isInDesignTime) {
            return isInDesignTime ? DesignUnitOfWorkFactory.Instance : DbUnitOfWorkFactory.Instance;
        }
    }
}