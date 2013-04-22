using System.Runtime.Serialization;

namespace KilnApi {
    /// <summary>
    /// Represents a registered user of the Kiln account.
    /// </summary>
    [DataContract]
    public class Person {
        /// <summary>
        /// A unique person identifier.
        /// </summary>
        [DataMember(Name = "ixPerson", IsRequired = true)]
        public int ID { get; private set; }

        /// <summary>
        /// The person's name.
        /// </summary>
        [DataMember(Name = "sName", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// The person's email address.
        /// </summary>
        [DataMember(Name = "sEmail", IsRequired = true)]
        public string Email { get; set; }

		public override string ToString() {
			return Name;
		}
    }
}