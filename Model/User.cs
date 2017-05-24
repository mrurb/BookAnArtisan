using System;
using System.Runtime.Serialization;

namespace Model
{
    public class User
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public bool EmailConfirmed { get; set; }
        [DataMember]
        public string PasswordHash { get; set; }
        [DataMember]
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Address { get; set; }
        public string ApiKey { get; set; }
        public User(string id, string firstName, string lastName, string email, string password, string phonenumber, string address)
        {
            Id = id;
            FirstName = firstName;
            Email = email;
            PasswordHash = password;
            PhoneNumber = phonenumber;
            Address = address;
            LastName = lastName;
        }

        public User()
        {
            //empty constructor
        }
    }
}
