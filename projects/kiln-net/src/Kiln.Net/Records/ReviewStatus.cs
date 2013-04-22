namespace KilnApi {
    /// <summary>
    /// The enumeration of statuses that a review
    /// status can take in the backend.
    /// </summary>
    public enum ReviewStatus {
        /// <summary>
        /// Special reserved status. Means that the library doesn't
        /// know anything about recieved status value.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Review is approved.
        /// </summary>
        Approved,

        /// <summary>
        /// Review is rejected.
        /// </summary>
        Rejected,

        /// <summary>
        /// Changesets will not be reviewed.
        /// </summary>
        WillNotReview,

        /// <summary>
        /// Review is currently in process.
        /// </summary>
        Active
    }
}
