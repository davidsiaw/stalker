using System;
using System.Runtime.Serialization;

namespace KilnApi {
    /// <summary>
    /// Represents Kiln code review case.
    /// </summary>
    [DataContract]
	public class Review {
		/// <summary>
		/// The reviewers of this review
		/// </summary>
		[DataMember(Name = "reviewers", IsRequired = true)]
		public Person[] Reviewers { get; private set; }

        /// <summary>
        /// A unique review identifier that also doubles as
        /// the FogBugz case number for usage in the FogBugz API.
        /// </summary>
		[DataMember(Name = "sReview", IsRequired = true)]
        public string ID { get; private set; }

        /// <summary>
        /// The repositories to which the review belongs.
        /// </summary>
		[DataMember(Name = "ixRepos", IsRequired = true)]
        public int[] RepoID { get; private set; }

        /// <summary>
        /// The status of the review in the backend.
        /// </summary>
        public ReviewStatus Status {
            get {
                ReviewStatus status;
                Enum.TryParse<ReviewStatus>(m_status, true, out status);
                return status;
            }
        }

        // Since DataContractJsonSerializer doesn't serialize enumerations based
        // on strings properly, it's just a way to get around this restriction.
        [DataMember(Name = "sStatus", IsRequired = true)]
        private string m_status;

        /// <summary>
        /// The current title of the review.
        /// </summary>
        [DataMember(Name = "sTitle", IsRequired = true)]
        public string Title { get; private set; }

        /// <summary>
        /// A list of all the changeset records that
        /// currently belong to the review.
        /// </summary>
        [DataMember(Name = "changesets")]
        public Changeset[] Changesets { get; private set; }
    }
}
