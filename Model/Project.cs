using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
        public int ProjectStatusID { get; set; }
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

        public Project(string id, List<string> tags, string description, User client, List<User> artisans, string address)
        {
            this.Id = Convert.ToInt32(id);
            this.Tags = tags;
            this.ProjectDescription = description;
            this.StreetName = address;
        }

        public Project()
        {
            //empty one also
        }
    }
}
