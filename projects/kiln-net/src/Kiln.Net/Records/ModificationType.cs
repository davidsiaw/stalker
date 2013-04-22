namespace KilnApi {
    /// <summary>
    /// The enumeration of modifications that
    /// a diff record can take in the backend.
    /// </summary>
    public enum ModificationType {
        /// <summary>
        /// Special reserved type. Means that the library doesn't
        /// know anything about recieved type value or
        /// that the value is null.
        /// </summary>
        Unknown = 0,

        Rename,
        Copy,
        Modechange,
        Creation,
        Deletion
    }
}