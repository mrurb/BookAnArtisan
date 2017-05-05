using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.ServiceModel;

namespace WCF
{
    [ServiceContract]
    interface IRentingService
    {
        // Implementing CRUD as a starting point.
        [OperationContract]
        Booking CreateBooking(Booking t);
        [OperationContract]
        Booking ReadBooking(Booking t);
        [OperationContract]
        Booking UpdateBooking(Booking t);
        [OperationContract]
        Booking DeleteBooking(Booking t);
        // Implementing ReadAll
        [OperationContract]
        List<Booking> ReadAllBooking();
    }
}
