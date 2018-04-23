using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace PersonalOrganizer.Common.DataModel.EntityFramework {
    public abstract class DbUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext {
        public DbUnitOfWork(TContext context) {
            Context = context;
        }
        public TContext Context { get; private set; }
        void IUnitOfWork.SaveChanges() {
            try {
                Context.SaveChanges();
            } catch(DbEntityValidationException ex) {
                throw DbExceptionsConverter.Convert(ex);
            } catch(DbUpdateException ex) {
                throw DbExceptionsConverter.Convert(ex);
            }
        }
        EntityState IUnitOfWork.GetState(object entity) {
            return GetEntityState(Context.Entry(entity).State);
        }
        void IUnitOfWork.Update(object entity) { }
        void IUnitOfWork.Detach(object entity) {
            Context.Entry(entity).State = System.Data.Entity.EntityState.Detached;
        }
        EntityState GetEntityState(System.Data.Entity.EntityState entityStates) {
            switch(entityStates) {
                case System.Data.Entity.EntityState.Added:
                    return EntityState.Added;
                case System.Data.Entity.EntityState.Deleted:
                    return EntityState.Deleted;
                case System.Data.Entity.EntityState.Detached:
                    return EntityState.Detached;
                case System.Data.Entity.EntityState.Modified:
                    return EntityState.Modified;
                case System.Data.Entity.EntityState.Unchanged:
                    return EntityState.Unchanged;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}