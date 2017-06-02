using System.Collections.Generic;

namespace BookAnArtisanMVC.Models
{
	public interface IPager<T>
	{


		List<T> PageList { get; set; }
		int TotalItems { get; set; }
		int CurrentPage { get; set; }
		int PageSize { get;  set; }
		int TotalPages { get; set; }
		int StartPage { get; set; }
		int EndPage { get; set; }

	}
}