using System;
using System.Linq;
using System.ComponentModel;

namespace PersonalOrganizer.Common.ViewModel {
    /// <summary>
    /// Represents the type of entity state change notification when the IUnitOfWork.SaveChanges method has been called.
    /// </summary>
    public enum EntityMessageType {

        /// <summary>
        /// The new entity has been added to the unit of work. 
        /// </summary>
        Added,

        /// <summary>
        /// The object has been deleted from the unit of work.
        /// </summary>
        Deleted,

        /// <summary>
        /// One of the properties on the object has been modified. 
        /// </summary>
        Changed
    }

    public class EntityMessage<TEntity, TPrimaryKey> {

        public TPrimaryKey PrimaryKey { get; private set; }

        /// <summary>
        /// The entity state change notification type.
        /// </summary>
        public EntityMessageType MessageType { get; private set; }

        public EntityMessage(TPrimaryKey primaryKey, EntityMessageType messageType) {
            this.PrimaryKey = primaryKey;
            this.MessageType = messageType;
        }
    }

    public class SaveAllMessage {
    }

    public class CloseAllMessage {
        readonly CancelEventArgs cancelEventArgs;
        public CloseAllMessage(CancelEventArgs cancelEventArgs) {
            this.cancelEventArgs = cancelEventArgs;
        }
        public bool Cancel {
            get { return cancelEventArgs.Cancel; }
            set { cancelEventArgs.Cancel = value; }
        }
    }

    public class NavigateMessage<TNavigationToken> {
        public NavigateMessage(TNavigationToken token) {
            Token = token;
        }
        public TNavigationToken Token { get; private set; }
    }
}
