using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model
{
	[DataContract]
	public class Meeting
	{
		[DataMember]
		public string Title { get; set; }
		[DataMember]
		public string Description { get; set; }
		[DataMember]
		public DateTime StartTime { get; set; }
		[DataMember]
		public DateTime EndTime { get; set; }
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public bool Deleted { get; set; }
		[DataMember]
		public User CreatedBy { get; set; }
		[DataMember]
		public User Contact { get; set; }

		public List<User> AppendedUsers { get; set; }
	}
}
