using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookAnArtisanMVC.RentingServiceReference
{
    [MetadataType(typeof(BookingMetaData))]
    public partial class Booking
    {
       
    }
    public class BookingMetaData
    {
        [Required()]
        /*[DisplayFormat(DataFormatString = "{0:d}")]*/
        /*[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]*/
        [Display(Name = "Booket Fra", Order = -9)]
        public DateTime StartTime;

        [Required()]
        [Display(Name = "Booket Til", Order = -8)]
        public DateTime EndTime;

        [Display(Name = "Slettet", Order = -7)]
        public bool Deleted;
        [Required()]
        public User User { get; set; }
        [Required()]
        public Material item { get; set; }

    }

}