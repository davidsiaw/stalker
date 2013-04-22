using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KilnApi.Helpers {
    /// <summary>
    /// Simple wrapper to be able to deserialize Kiln API 1.0
    /// errors message in one step.
    /// </summary>
    [DataContract]
    public class KilnErrorsWrapper {
        /// <summary>
        /// List of deserialized KilnApiError
        /// </summary>
        [DataMember(Name = "errors")]
        public List<KilnError> Errors { get; private set; }
    }
}