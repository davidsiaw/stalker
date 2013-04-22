using System.Runtime.Serialization;

namespace KilnApi {
    /// <summary>
    /// Represents the file in a given revision.
    /// </summary>
    [DataContract]
    public class Cat {
        /// <summary>
        /// A list of lines in the file.
        /// </summary>
        [DataMember(Name = "bsLines", IsRequired = true)]
        public string[] Lines { get; private set; }

        /// <summary>
        /// A list of annotations.
        /// </summary>
        [DataMember(Name = "annotations")]
        public Annotation[] Annotations { get; private set; }
    }
}
