using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
        public string OwnerId { get; set; }
        [DataMember]
        public string Condition { get; set; }
        [DataMember]
        public bool Deleted { get; set; }
        [DataMember]
        public bool Available { get; set; }

    }
}
