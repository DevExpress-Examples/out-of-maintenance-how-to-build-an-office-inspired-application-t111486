using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using PersonalOrganizer.Common.Utils;
using PersonalOrganizer.Common.DataModel;

namespace PersonalOrganizer.Common.ViewModel {
    /// <summary>
    /// The base interface for view models representing a single entity.
    /// </summary>
    /// <typeparam name="TEntity">An entity type.</typeparam>
    /// <typeparam name="TPrimaryKey">An entity primary key type.</typeparam>
    public interface ISingleObjectViewModel<TEntity, TPrimaryKey> {

        /// <summary>
        /// The entity represented by a view model.
        /// </summary>
        /// <returns></returns>
        TEntity Entity { get; }

        /// <summary>
        /// The entity primary key value.
        /// </summary>
        /// <returns></returns>
        TPrimaryKey PrimaryKey { get; }
    }
}