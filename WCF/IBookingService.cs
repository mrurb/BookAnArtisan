using System;
using System.Collections.Generic;
using System.ServiceModel;
using Model;

namespace WCF
{
	[ServiceContract]
	internal interface IBookingService
	{
		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		Booking CreateBooking(Booking t);

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		Booking ReadBooking(Booking t);

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		Booking UpdateBooking(Booking t);

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		Booking DeleteBooking(Booking t);

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		List<Booking> ReadAllBooking();

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		Page<Booking> ReadPageBooking(int? page, int? pageSize);
		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		Page<Booking> ReadPageForUserBooking(string userId, int? page, int? pageSize);
	}
}