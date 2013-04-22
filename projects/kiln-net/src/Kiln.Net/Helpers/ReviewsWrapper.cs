using System.Runtime.Serialization;

namespace KilnApi.Helpers {
    /// <summary>
    /// A wrapper for reviews assigned to you and opened by you.
    /// </summary>
    [DataContract]
    public class ReviewsWrapper {

        /// <summary>
		/// A list of reviews that the authenticating user has starred
        /// </summary>
		[DataMember(Name = "reviewsStarred")]
		public Review[] Starred { get; private set; }

        /// <summary>
		/// A list of reviews on which the authenticating user is a reviewer, and is undecided
        /// </summary>
		[DataMember(Name = "reviewsAwaitingReview")]
		public Review[] AwaitingReview { get; private set; }

		/// <summary>
		/// A list of reviews on which the authenticating user is a reviewer, and has approved the review
        /// </summary>
		[DataMember(Name = "reviewsApproved")]
		public Review[] Approved { get; private set; }

		/// <summary>
		/// A list of reviews on which the authenticating user is a reviewer, and has rejected the review
		/// </summary>
		[DataMember(Name = "reviewsRejected")]
		public Review[] Rejected { get; private set; }

		/// <summary>
		/// A list of reviews containing changesets authored by the authenticating user
		/// </summary>
		[DataMember(Name = "reviewsAuthor")]
		public Review[] Author { get; private set; }
		
    }
}
