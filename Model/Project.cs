using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Project
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Created_by_ID { get; set; }
        public string Contact_ID { get; set; }
        public int Project_status_ID { get; set; }
        public string Project_description { get; set; }
        public string Street_Name { get; set; }
        public DateTime Start_time { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }

        // override object.Equals
        // The purpose is that when testing it makes sense to compare with the .Equals method, hence requiring this method.
        public override bool Equals(object obj)
        {
            Project other = obj as Project;      

            if (this.ID != other.ID ||
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
