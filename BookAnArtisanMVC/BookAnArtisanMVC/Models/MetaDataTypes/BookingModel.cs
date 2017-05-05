﻿using System;
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
        [Display(Name = "Booket Fra")]
        public DateTime StartTime;

        [Display(Name = "Booket Til")]
        public DateTime EndTime;

        [Display(Name ="Slettet")]
        public bool Deleted;

    }

}