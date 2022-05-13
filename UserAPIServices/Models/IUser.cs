using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPIServices.Models
{
    public interface IUser
    {
        string BookTicket(tblBooking user);

        List<tblBooking> GetUserHistory(string emailID);

        List<tblBooking> GetTicketDetails(string pnr);

        string CancelTicket(string pnr);

        List<tblTicketDetails> GetBookingHistory(string emailID);

        string CancelBooking(string pnr);

        List<tblDiscount> GetDiscounts();
    }
}
