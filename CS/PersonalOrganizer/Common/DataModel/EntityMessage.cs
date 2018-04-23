using System;
using System.Linq;

namespace PersonalOrganizer.Common.DataModel {
    public enum EntityState {
        Detached = 1,
        Unchanged = 2,
        Added = 4,
        Deleted = 8,
        Modified = 16,
    }
    public enum EntityMessageType {
        Added, Deleted, Changed
    }
    public class EntityMessage<TEntity> {
        public TEntity Entity { get; private set; }
        public EntityMessageType MessageType { get; private set; }
        public EntityMessage(TEntity entity, EntityMessageType messageType) {
            this.Entity = entity;
            this.MessageType = messageType;
        }
    }
}
