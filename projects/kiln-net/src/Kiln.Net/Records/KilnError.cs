using System;
using System.Runtime.Serialization;

namespace KilnApi {
    /// <summary>
    /// Represents an error which occurs on the Kiln side,
    /// as a consequence of an incorrect call.
    /// </summary>
    [DataContract]
    public sealed class KilnError {
        /// <summary>
        /// A short machine code for the error.
        /// </summary>
        public KilnErrorCode Code {
            get {
                KilnErrorCode code;
                Enum.TryParse<KilnErrorCode>(m_code, true, out code);
                return code;
            }
        }

        // Since DataContractJsonSerializer doesn't serialize enumerations based
        // on strings properly, it's just a way to get around this restriction.
        [DataMember(Name = "codeError", IsRequired = true)]
        private string m_code;

        /// <summary>
        /// The descriptive error message string.
        /// </summary>
        [DataMember(Name = "sError", IsRequired = true)]
        public string Description { get; private set; }
    }
}