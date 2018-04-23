using System;
using System.Linq;
using System.Collections.Generic;

namespace PersonalOrganizer.Common.DataModel {
    public class DesignTimeUnitOfWork : IUnitOfWork {
        public static readonly IUnitOfWork Instance = new DesignTimeUnitOfWork();

        void IUnitOfWork.SaveChanges() { }
        EntityState IUnitOfWork.GetState(object entity) {
            return EntityState.Detached;
        }
        void IUnitOfWork.Update(object entity) { }
        void IUnitOfWork.Detach(object entity) { }
    }
}