﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
namespace Model
{
    [ServiceContract]
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
        public Material item { get; set; }
    }
}