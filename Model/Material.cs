using System.Runtime.Serialization;

namespace Model
{
	[DataContract]
	public class Material
	{
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string Description { get; set; }
		[DataMember]
		public User Owner { get; set; }
		[DataMember]
		public string Condition { get; set; }
		[DataMember]
		public bool Deleted { get; set; }
		[DataMember]
		public bool Available { get; set; }

	}
}
