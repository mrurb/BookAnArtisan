using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Model
{
    public class User
    {

        public User(string id, string first_name, string last_name, string email, string password, string phonenumber, string address)
        {
            this.id = id;
            this.first_name = first_name;
            this.email = email;
            this.password = password;
            this.phonenumber = phonenumber;
            this.address = address;
            this.last_name = last_name;
        }
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        // Do we want that?? Doesn't seem secure.
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // I'm aware phonenumber is listed twice. TO FIX!
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ApiKey { get; set; }
        

        public override bool Equals(object obj)
        {
            User other = obj as User;

            if( !this.Id.Equals(other.Id) ||
                !this.Email.Equals(other.Email) ||
                this.EmailConfirmed != other.EmailConfirmed ||
                !this.PasswordHash.Equals(other.PasswordHash) ||
                !this.SecurityStamp.Equals(other.SecurityStamp) ||
                !this.PhoneNumber.Equals(other.PhoneNumber) ||
                this.PhoneNumberConfirmed != other.PhoneNumberConfirmed ||
                this.TwoFactorEnabled != other.TwoFactorEnabled ||
                !this.LockoutEndDateUtc.Equals(other.LockoutEndDateUtc) ||
                this.LockoutEnabled != other.LockoutEnabled ||
                this.AccessFailedCount != other.AccessFailedCount ||
                !this.UserName.Equals(other.UserName) ||
                !this.FirstName.Equals(other.FirstName) ||
                !this.LastName.Equals(other.LastName) ||
                !this.Phone.Equals(other.Phone) ||
                !this.Address.Equals(other.Address) ||
                !this.ApiKey.Equals(other.ApiKey)
                )
            {
                return false;
            }

            return true;
        }
    }
}
