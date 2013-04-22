using System.Runtime.Serialization;

namespace KilnApi {
    /// <summary>
    /// Represents a group of repositories in Kiln.
    /// </summary>
    [DataContract]
    public class RepositoryGroup {
        /// <summary>
        /// A unique repository group identifier.
        /// </summary>
        [DataMember(Name = "ixRepoGroup", IsRequired = true)]
        public int ID { get; private set; }

        /// <summary>
        /// The project the repository group belongs to.
        /// </summary>
        [DataMember(Name = "ixProject", IsRequired = true)]
        public int ProjectID { get; private set; }

        /// <summary>
        /// A unique repository group URL slug.
        /// </summary>
        [DataMember(Name = "sSlug", IsRequired = true)]
        public string Slug { get; private set; }

        /// <summary>
        /// The repository group name.
        /// </summary>
        [DataMember(Name = "sName", IsRequired = true)]
        public string Name { get; private set; }

        /// <summary>
        /// List of repository records belonging to the repository group.
        /// </summary>
        [DataMember(Name = "repos")]
        public Repository[] Repositories { get; private set; }
    }
}