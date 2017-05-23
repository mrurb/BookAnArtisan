using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	[DataContract(Name = "{0}Page")]
	public class Page<T>
	{
		[DataMember]
		public IList<T> PageList { get; set; }
		[DataMember]
		public int TotalItems { get; private set; }
		[DataMember]
		public int CurrentPage { get; private set; }
		[DataMember]
		public int PageSize { get; private set; }
		[DataMember]
		public int TotalPages { get; private set; }
		[DataMember]
		public int StartPage { get; private set; }
		[DataMember]
		public int EndPage { get; private set; }
		public Page(int totalItems, IList<T> pageList, int? page = 1, int? pageSize = 10)
		{
			// calculate total, start and end pages
			PageList = pageList;
			PageSize = pageSize ?? 10;
			var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
			var currentPage = page ?? 1;
			var startPage = currentPage - 5;
			var endPage = currentPage + 4;
			if (startPage <= 0)
			{
				endPage -= (startPage - 1);
				startPage = 1;
			}
			if (endPage > totalPages)
			{
				endPage = totalPages;
				if (endPage > 10)
				{
					startPage = endPage - 9;
				}
			}

			TotalItems = totalItems;
			CurrentPage = currentPage;
			TotalPages = totalPages;
			StartPage = startPage;
			EndPage = endPage;
		}
	}
}
