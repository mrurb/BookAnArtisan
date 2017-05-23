using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookAnArtisanMVC.ServiceReference;

namespace BookAnArtisanMVC.Models
{
	public interface IPager<T>
	{


		IList<T> PageList { get; set; }
		int TotalItems { get; set; }
		int CurrentPage { get; set; }
		int PageSize { get;  set; }
		int TotalPages { get; set; }
		int StartPage { get; set; }
		int EndPage { get; set; }

	}
}