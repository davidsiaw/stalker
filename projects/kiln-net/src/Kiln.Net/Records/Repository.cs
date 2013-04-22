using System;
using System.Runtime.Serialization;

namespace KilnApi {
    /// <summary>
    /// Represents a Mercurial repository stored in Kiln.
    /// </summary>
    [DataContract]
    public class Repository {
        /// <summary>
        /// A unique repository identifier.
        /// </summary>
        [DataMember(Name = "ixRepo", IsRequired = true)]
        public int ID { get; private set; }

        /// <summary>
        /// The repository name.
        /// </summary>
        [DataMember(Name = "sName", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// The repository description.
        /// </summary>
        [DataMember(Name = "sDescription")]
        public string Descripton { get; set; }

        /// <summary>
        /// The repository group the repository belongs to.
        /// </summary>
        [DataMember(Name = "ixRepoGroup", IsRequired = true)]
        public int GroupID { get; set; }

        /// <summary>
        /// The repository this repository was branched from.
        /// </summary>
        /// <remarks>
        /// Null if this repository was not branched from any repository.
        /// </remarks>
        [DataMember(Name = "ixParent")]
        public int? ParentID { get; set; }

        /// <summary>
        /// Indicates that the repository is central.
        /// </summary>
        [DataMember(Name = "fCentral", IsRequired = true)]
        public bool IsCentral { get; set; }

        /// <summary>
        /// The size of the repository in bytes.
        /// </summary>
        /// <remarks>
        /// May be null if the Kiln backend has yet to tally this information.
        /// </remarks>
        [DataMember(Name = "bytesSize")]
        public long? Size { get; private set; }

        /// <summary>
        /// A unique repository URL slug.
        /// </summary>
        [DataMember(Name = "sSlug", IsRequired = true)]
        public string Slug { get; private set; }

        /// <summary>
        /// The repository's repository group unique URL slug.
        /// </summary>
        [DataMember(Name = "sGroupSlug", IsRequired = true)]
        public string GroupSlug { get; private set; }

        /// <summary>
        /// The repository's project unique URL slug.
        /// </summary>
        [DataMember(Name = "sProjectSlug", IsRequired = true)]
        public string ProjectSlug { get; private set; }

        /// <summary>
        /// The default repository permission.
        /// </summary>
        public Permission DefaultPermission {
            get {
                Permission permission;
                Enum.TryParse<Permission>(m_permission, true, out permission);
                return permission;
            }
        }

        // Since DataContractJsonSerializer doesn't serialize enumerations based
        // on strings properly, it's just a way to get around this restriction.
        [DataMember(Name = "permissionDefault", IsRequired = true)]
        private string m_permission;

        /// <summary>
        /// The creator of the repository.
        /// </summary>
        [DataMember(Name = "personCreator", IsRequired = true)]
        public Person Creator { get; private set; }

        /// <summary>
        /// The status of the repository in the backend.
        /// </summary>
        public RepositoryStatus Status {
            get {
                RepositoryStatus status;
                Enum.TryParse<RepositoryStatus>(m_status, true, out status);
                return status;
            }
        }

        // Since DataContractJsonSerializer doesn't serialize enumerations based
        // on strings properly, it's just a way to get around this restriction.
        [DataMember(Name = "sStatus")]
        private string m_status;

        /// <summary>
        /// A list of repository records that are branches of this repository.
        /// </summary>
        /// <remarks>
        /// The list is empty if this repository is not a central repository
        /// (only central repositories may have branch repositories, same as
        /// the website user interface).
        /// </remarks>
        [DataMember(Name = "repoBranches")]
        public Repository[] Branches { get; private set; }
    }
}