using System;
using System.Runtime.Serialization;

namespace KilnApi {
    /// <summary>
    /// Represents a Mercurial changeset.
    /// </summary>
    [DataContract]
    public class Changeset {

        /// <summary>
        /// The changeset's revision.
        /// </summary>
        [DataMember(Name = "rev", IsRequired = true)]
        public string Revision { get; private set; }

        /// <summary>
        /// The author's name, as configured in the author's .hgrc file.
        /// </summary>
        [DataMember(Name = "sAuthor", IsRequired = true)]
        public string Author { get; private set; }

        /// <summary>
        /// The changeset's message.
        /// </summary>
        [DataMember(Name = "sDescription")]
        public string Description { get; private set; }

        /// <summary>
        /// The changeset's date.
        /// </summary>
        public DateTime Date {
            get {
                DateTime date;
                DateTime.TryParse(m_date, out date);
                return date;
            }
        }

        // Since DataContractJsonSerializer tries to deserialize values for
        // DateTime type using JSON standard for date/time values (\/Date(...)\/)
        // and Kiln API returns date/time values in ISO8601 format we need to add
        // proxy member to get around this discord.
        [DataMember(Name = "dt", IsRequired = true)]
        private string m_date;

        /// <summary>
        /// The changeset's parent revision
        /// (null if the changeset is a tail changeset).
        /// </summary>
        [DataMember(Name = "revParent1")]
        public string Parent1 { get; private set; }

        /// <summary>
        /// The changeset's second parent revision
        /// (null if there is none; non-null if the changeset is a merge).
        /// </summary>
        [DataMember(Name = "revParent2")]
        public string Parent2 { get; private set; }

        /// <summary>
        /// A list of diff records, one per file modified.
        /// </summary>
        [DataMember(Name = "diffs")]
        public Diff[] Diffs { get; set; }

		/// <summary>
		/// A list of fogbugz case numbers corresponding to all the cases with which
		/// the changeset is associated
		/// </summary>
		[DataMember(Name = "ixBugs")]
		public int[] AssociatedCases { get; set; }

    }
}
