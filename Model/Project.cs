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
        public string Created_by_ID { get; set; }
        [DataMember]
        public string Contact_ID { get; set; }
        [DataMember]
        public int Project_status_ID { get; set; }
        [DataMember]
        public string Project_description { get; set; }
        [DataMember]
        public string Street_Name { get; set; }
        [DataMember]
        public DateTime Start_time { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public DateTime Modified { get; set; }
        [DataMember]
        public bool Deleted { get; set; }
        public List<string> tags { get; set; }
        public User client { get; set; }
        public List<User> artisans { get; set; }
        public string description { get; set; }
        public string address { get; set; }

        public Project(string id, List<string> tags, string description, User client, List<User> artisans, string address)
        {
            this.Id = Convert.ToInt32(id);
            this.tags = tags;
            this.description = description;
            this.client = client;
            this.address = address;
            this.artisans = artisans;
        }

        public Project()
        {
            //empty one also
        }

        // override object.Equals
        // The purpose is that when testing it makes sense to compare with the .Equals method, hence requiring this method.
        public override bool Equals(object obj)
        {
            Project other = obj as Project;      

            if (this.Id != other.Id ||
                !this.Name.Equals(other.Name) ||
                !this.Created_by_ID.Equals(other.Created_by_ID) ||
                !this.Contact_ID.Equals(other.Contact_ID) ||
                this.Project_status_ID != other.Project_status_ID ||
                !this.Project_description.Equals(other.Project_description) ||
                !this.Street_Name.Equals(other.Street_Name) ||
                !this.Start_time.Equals(other.Start_time) ||
                !this.Created.Equals(other.Created) ||
                !this.Modified.Equals(other.Modified) ||
                this.Deleted != other.Deleted)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
