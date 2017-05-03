using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
namespace Model
{
    [ServiceContract]
    public class Rented
    {

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime StartTime { get; set; }
        [DataMember]
        public DateTime EndTime { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public object Deleted { get; set; }
    }
}
