namespace KilnApi {
    /// <summary>
    /// The enumeration of statuses that a repository
    /// status can take in the backend.
    /// </summary>
    public enum RepositoryStatus {
        /// <summary>
        /// Special reserved status. Means that the library doesn't
        /// know anything about recieved status value.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The backend is now creating the repository.
        /// </summary>
        New,

        /// <summary>
        /// The repository is OK and ready for interaction.
        /// </summary>
        Good,

        Conflicted,
        Error,

        /// <summary>
        /// The repository is deleted.
        /// </summary>
        Deleted
    }
}
