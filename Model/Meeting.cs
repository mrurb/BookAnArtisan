using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Meeting
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Id { get; set; }
        public string CreatedById { get; set; }
        public string ContactId { get; set; }

        public string CreatedBy { get; set; }
        public string Contact { get; set; }

        public List<User> AppendedUsers { get; set; }
    }
}
