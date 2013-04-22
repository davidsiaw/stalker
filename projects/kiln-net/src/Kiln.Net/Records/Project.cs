using System;
using System.Runtime.Serialization;

namespace KilnApi {
    /// <summary>
    /// Represents a Kiln project.
    /// </summary>
    [DataContract]
    public class Project {
        /// <summary>
        /// A unique project identifier.
        /// </summary>
        [DataMember(Name = "ixProject", IsRequired = true)]
        public int ID { get; private set; }

        /// <summary>
        /// The project name.
        /// </summary>
        [DataMember(Name = "sName", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// A unique project URL slug.
        /// </summary>
        [DataMember(Name = "sSlug", IsRequired = true)]
        public string Slug { get; private set; }

        /// <summary>
        /// The project description.
        /// </summary>
        [DataMember(Name = "sDescription")]
        public string Description { get; set; }

        /// <summary>
        /// The default project permission.
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
        /// A list of repository groups belonging to the project.
        /// </summary>
        [DataMember(Name = "repoGroups")]
        public RepositoryGroup[] Groups { get; private set; }

		public override string ToString() {
			return Name;
		}
    }
}