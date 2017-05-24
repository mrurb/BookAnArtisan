using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model
{
	[DataContract]
	public class Project
	{
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public User CreatedBy { get; set; }
		[DataMember]
		public User Contact { get; set; }
		[DataMember]
		public int ProjectStatusId { get; set; }
		[DataMember]
		public string ProjectDescription { get; set; }
		[DataMember]
		public string StreetName { get; set; }
		[DataMember]
		public DateTime StartTime { get; set; }
		[DataMember]
		public DateTime Created { get; set; }
		[DataMember]
		public DateTime Modified { get; set; }
		[DataMember]
		public bool Deleted { get; set; }
		public List<string> Tags { get; set; }

		public Project(string id, List<string> tags, string description, string address)
		{
			Id = Convert.ToInt32(id);
			Tags = tags;
			ProjectDescription = description;
			StreetName = address;
		}

		public Project()
		{
			//empty constructor
		}
	}
}
