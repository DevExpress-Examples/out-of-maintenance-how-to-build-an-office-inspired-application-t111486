using System;
using System.Linq;

namespace PersonalOrganizer.Common.DataModel {
    public interface IUnitOfWork {
        void SaveChanges();
        EntityState GetState(object entity);
        void Update(object entity);
        void Detach(object entity);
    }
    public interface IUnitOfWorkFactory<out TUnitOfWork> where TUnitOfWork : IUnitOfWork {
        TUnitOfWork CreateUnitOfWork();
    }
}
