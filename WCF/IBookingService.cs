﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.ServiceModel;

namespace WCF
{
    [ServiceContract]
    interface IBookingService
    {
        // Implementing CRUD as a starting point.
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
        // Implementing ReadAll
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
        List<Booking> ReadAllBooking();
	    [OperationContract]
		Page<Booking> ReadPageBooking(int? page, int? pageSize);
    }
}