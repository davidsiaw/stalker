namespace KilnApi {
    /// <summary>
    /// Permissions that can be applied to the Kiln API objects
    /// </summary>
    /// <remarks>
    /// Most objects in the Kiln API have permissions. Certain users may only be
    /// only able to perform certain actions on them. To get detailed information
    /// about permissions in Kiln and how particular permission behaves, see
    /// https://developers.fogbugz.com/default.asp?W171
    /// </remarks>
    public enum Permission {
        /// <summary>
        /// Special reserved permission. Means that the library doesn't
        /// know anything about recieved permission value.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Grants no privileges. The repository or project
        /// is unreadable and unwritable for the user.
        /// </summary>
        None,

        /// <summary>
        /// Grants the user permission to make any API call
        /// that doesn't modify the project or repository's data.
        /// </summary>
        Read,

        /// <summary>
        /// Grants the ability to modify the data.
        /// </summary>
        Write,

        /// <summary>
        /// Default project permission of the project
        /// the repository belongs to.
        /// </summary>
        Inherit,

        /// <summary>
        /// In addition to write permission, administrator has
        /// the ability to create/delete projects and central
        /// repositories, modify permissions of other persons.
        /// </summary>
        Admin
    }
}