using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public string email { get; set; }
        public string id { get; set; }
        public string address { get; set; }
        public string password { get; set; }
        public string phonenumber { get; set; }
        public string name { get; set; }
        public string companyname { get; set; }
        public string profession { get; set; }
        public User(string id, string name, string email, string password, string phonenumber, string address)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.password = password;
            this.phonenumber = phonenumber;
            this.address = address;
        }

    }
}
