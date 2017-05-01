using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
        public string CreatedById { get; set; }
        [DataMember]
        public string ContactId { get; set; }
        [DataMember]
        public bool Deleted { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public string Contact { get; set; }

        public List<User> AppendedUsers { get; set; }
    }
}
