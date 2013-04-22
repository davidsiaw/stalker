using System.Runtime.Serialization;

namespace KilnApi {
    /// <summary>
    /// Describes a contiguous ordered set (block) of lines
    /// in a file at the revision responsible for those lines.
    /// </summary>
    [DataContract]
    public class Annotation {
        /// <summary>
        /// The size of the block in the number of lines.
        /// </summary>
        [DataMember(Name = "nLines", IsRequired = true)]
        public int LinesCount { get; private set; }

        /// <summary>
        /// The revision responsible for the block.
        /// </summary>
        [DataMember(Name = "rev", IsRequired = true)]
        public string Revision { get; private set; }

        /// <summary>
        /// The block author's name, as configured
        /// in the author's .hgrc.
        /// </summary>
        [DataMember(Name = "sAuthor", IsRequired = true)]
        public string Author { get; private set; }
    }
}
