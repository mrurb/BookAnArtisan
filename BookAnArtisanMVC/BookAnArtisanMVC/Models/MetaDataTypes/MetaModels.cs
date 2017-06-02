﻿using System;
using System.ComponentModel.DataAnnotations;
using BookAnArtisanMVC.Models;

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

	[MetadataType(typeof(MaterialMetaData))]
	public partial class Material
	{
	}

	internal class MaterialMetaData
	{
		[Required]
		[Display(Name = "Materialenavn", Order = -9)]
		public string Name { get; set; }
		[Required]
		[Display(Name = "Beskrivelse", Order = -8)]
		public string Description { get; set; }
		[Required]
		[Display(Name = "Ejer", Order = -6)]
		public User Owner { get; set; }
		[Required]
		[Display(Name = "Tilstand", Order = -7)]
		public string Condition { get; set; }
		[Display(Name = "Slettet")]
		public bool Deleted { get; set; }
		[Display(Name = "Tilgængelig", Order = -5)]
		public bool Available { get; set; }



	}

	[MetadataType(typeof(BookingMetaData))]
	public partial class Booking
	{


	}
	public class BookingMetaData
	{
		[Required]
		[Display(Name = "Booket Fra", Order = -9)]
		public DateTime StartTime;

		[Required]
		[Display(Name = "Booket Til", Order = -8)]
		public DateTime EndTime;

		[Display(Name = "Slettet", Order = -7)]
		public bool Deleted;
		[Required]
		[Display(Name = "Lejer")]
		public User User { get; set; }
		[Required]
		[Display(Name = "Materiale")]
		public Material Item { get; set; }

	}

	[MetadataType(typeof(MeetingMetaData))]
	public partial class Meeting
	{
	}
	public class MeetingMetaData
	{
		[Display(Name = "Titel")]
		public string Title { get; set; }
		[Display(Name = "Beskrivelse")]
		public string Description { get; set; }
		[Display(Name = "Starttid")]
		public DateTime StartTime { get; set; }
		[Display(Name = "Forventet sluttid")]
		public DateTime EndTime { get; set; }
		[Display(Name = "Møde-Id")]
		public int Id { get; set; }
		[Display(Name = "Slettet")]
		public bool Deleted { get; set; }
		[Display(Name = "Oprettet af")]
		public User CreatedBy { get; set; }
		[Display(Name = "Kontakt")]
		public User Contact { get; set; }
	}

	[MetadataType(typeof(ProjectMetaData))]
	public partial class Project
	{

	}
	public class ProjectMetaData
	{
		[Display(Name = "Titel")]
		public string Name { get; set; }
		[Display(Name = "Oprettet af")]
		public User CreatedBy { get; set; }
		[Display(Name = "Kontakt")]
		public User Contact { get; set; }
		[Display(Name = "Status")]
		public int ProjectStatusId { get; set; }
		[Display(Name = "Beskrivelse")]
		public string ProjectDescription { get; set; }
		[Display(Name = "Vejnavn")]
		public string StreetName { get; set; }
		[Display(Name = "Start")]
		public DateTime StartTime { get; set; }
		[Display(Name = "Oprettet")]
		public DateTime Created { get; set; }
		[Display(Name = "Ændret")]
		public DateTime Modified { get; set; }
		[Display(Name = "Slettet")]
		public bool Deleted { get; set; }
	}

	[MetadataType(typeof(TagMetaData))]
	public partial class Tag
	{

	}
	public class TagMetaData
	{

	}

	public partial class BookingPage : IPager<Booking>
	{

	}

	public partial class ProjectPage : IPager<Project>
	{

	}

	public partial class MeetingPage : IPager<Meeting>
	{

	}

	public partial class MaterialPage : IPager<Material>
	{

	}

}