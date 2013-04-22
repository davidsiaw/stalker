using System;
using System.Runtime.Serialization;

namespace KilnApi {
    /// <summary>
    /// Describes the modifications to exactly
    /// one file in a given revision.
    /// </summary>
    [DataContract]
    public class Diff {
        /// <summary>
        /// The lines of the diff corresponding to a list of bytestrings.
        /// </summary>
        [DataMember(Name = "bsLines", IsRequired = true)]
        public string[] Lines { get; private set; }

        /// <summary>
        /// The byte path to the file.
        /// </summary>
        [DataMember(Name = "bpPath", IsRequired = true)]
        public string BytePath { get; private set; }

        /// <summary>
        /// Type of modification.
        /// </summary>
        public ModificationType Type {
            get {
                ModificationType type;
                Enum.TryParse<ModificationType>(m_type, true, out type);
                return type;
            }
        }

        [DataMember(Name = "sType", IsRequired = true)]
        private string m_type;

        /// <summary>
        /// The file path before modification.
        /// </summary>
        [DataMember(Name = "bpOldPath")]
        public string OldPath { get; private set; }

        /// <summary>
        /// Two-element list whose first element is the file mode
        /// before modification and whose second element is
        /// the file mode after modification.
        /// </summary>
        [DataMember(Name = "modeChange")]
        public string[] ModeChange { get; private set; }
    }
}
