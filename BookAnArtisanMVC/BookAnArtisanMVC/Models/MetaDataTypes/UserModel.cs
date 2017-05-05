using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookAnArtisanMVC.ServiceReference
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
    }

    public class UserMetaData
    {
        [Display(Name = "Telefon")]
        public string PhoneNumber;

        [Display(Name = "Brugernavn")]
        public string UserName;

        [Display(Name = "Addresse")]
        public string Address;

        [Display(Name = "Email")]
        public string Email;

        [Display(Name = "Fornavn")]
        public string FirstName;

        [Display(Name = "Efternavn")]
        public string LastName;
    }
}

