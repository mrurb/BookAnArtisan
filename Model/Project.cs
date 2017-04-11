using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Project
    {
        public string id { get; set; }
        public string description { get; set; }
        public User client { get; set; }
        public List<User> artisans { get; set; }
        public string address { get; set; }
        public List<string> tags { get; set; }
        public Project(string id, List<string> tags, string description, User client, List<User> artisans, string address)
        {
            this.id = id;
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
    }
}
