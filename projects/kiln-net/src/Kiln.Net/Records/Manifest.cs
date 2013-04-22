using System.Runtime.Serialization;

namespace KilnApi {
    /// <summary>
    /// Describes file location.
    /// </summary>
    [DataContract]
    public class Manifest {
        /// <summary>
        /// The path to the file as a byte path.
        /// </summary>
        /// <remarks>
        /// About byte path see in Kiln API idioms here:
        /// https://developers.fogbugz.com/default.asp?W159
        /// </remarks>
        [DataMember(Name = "bpPath", IsRequired = true)]
        public string BytePath { get; private set; }

        /// <summary>
        /// The path to the file in UTF-8.
        /// </summary>
        [DataMember(Name = "sPath")]
        public string Path { get; private set; }
    }
}
