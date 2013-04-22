using System;
using System.Collections.Generic;
using System.Linq;

namespace KilnApi {
    /// <summary>
    /// Represents errors occur during interacting with Kiln.
    /// </summary>
    public class KilnApiException : Exception {
        /// <summary>
        /// Errors occurred on the Kiln side during the call.
        /// </summary>
        public List<KilnError> Errors { get; private set; }

        #region Overloaded Constructors
        public KilnApiException()
            : this(new List<KilnError>()) {
        }

        public KilnApiException(IEnumerable<KilnError> errors) :
            this(errors, null) {
        }

        public KilnApiException(IEnumerable<KilnError> errors, string message)
            : this(errors, message, null) {
        }

        public KilnApiException(IEnumerable<KilnError> errors, string message, Exception innerException) :
            base(message, innerException) {
            Errors = errors.ToList();
        }
        #endregion
    }
}