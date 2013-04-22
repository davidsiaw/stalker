using System.Runtime.Serialization;

namespace KilnApi {
    /// <summary>
    /// Represents repository alias.
    /// </summary>
    [DataContract]
    public class Alias {
        /// <summary>
        /// Unique identifier of the alias.
        /// </summary>
        [DataMember(Name = "ixRepoAlias", IsRequired = true)]
        public int ID { get; private set; }

        /// <summary>
        /// The unique identifier of the repository
        /// to which the alias is attached.
        /// </summary>
        [DataMember(Name = "ixRepo", IsRequired = true)]
        public int RepoID { get; private set; }

        /// <summary>
        /// The user-provided name of the alias.
        /// </summary>
        [DataMember(Name = "sName", IsRequired = true)]
        public string Name { get; private set; }

        /// <summary>
        /// The slugified name (name that is valid to use as part of the URL).
        /// This slug must be unique within each project.
        /// </summary>
        [DataMember(Name = "sSlug", IsRequired = true)]
        public string Slug { get; private set; }
    }
}
