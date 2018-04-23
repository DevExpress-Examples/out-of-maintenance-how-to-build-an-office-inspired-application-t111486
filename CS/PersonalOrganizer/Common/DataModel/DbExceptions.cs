using System;
using System.Linq;

namespace PersonalOrganizer.Common.DataModel {
    public class DbException : Exception {
        public DbException(string errorMessage, string errorCaption, Exception innerException)
            : base(innerException.Message, innerException) {
            ErrorMessage = errorMessage;
            ErrorCaption = errorCaption;
        }
        public string ErrorMessage { get; private set; }
        public string ErrorCaption { get; private set; }
    }
}