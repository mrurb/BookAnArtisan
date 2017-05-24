using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Booking
    {

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime StartTime { get; set; }
        [DataMember]
        public DateTime EndTime { get; set; }
        [DataMember]
        public User User { get; set; }
        [DataMember]
        public bool Deleted { get; set; }
        [DataMember]
        public Material Item { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public DateTime Updated { get; set; }
    }
}
