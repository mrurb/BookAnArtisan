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
    }
}
