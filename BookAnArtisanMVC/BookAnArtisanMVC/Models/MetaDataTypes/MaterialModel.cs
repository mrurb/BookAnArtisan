using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookAnArtisanMVC.MaterialServiceReference
{
    [MetadataType(typeof(MaterialMetaData))]
    public partial class Material
    {
    }

    internal class MaterialMetaData
    {
        [Required()]
        [Display(Name = "Navn", Order = -9)]
        public string Name { get; set; }
        [Required()]
        [Display(Name ="Beskrivelse", Order = -8)]
        public string Description { get; set; }
        [Required()]
        [Display(Name = "Ejer", Order = -6)]
        public User Owner { get; set; }
        [Required()]
        [Display(Name = "Tilstand", Order = -7)]
        public string Condition { get; set; }
        [Display(Name = "Slettet")]
        public bool Deleted { get; set; }
        [Display(Name = "Tilgænglig", Order = -5)]
        public bool Available { get; set; }



    }
}