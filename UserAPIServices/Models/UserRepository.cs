using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPIServices.Models
{
    public class UserRepository : IUser
    {
        //public List<Booking> _user;

        //public List<Discount> _discounts;

        public UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            this._userContext = userContext;
            //_user = new List<Booking>();
            //_discounts = new List<Discount>();
            //_discounts.Add(new Discount { CouponCode = "A500", DiscountValue = 500 });
            //_discounts.Add(new Discount { CouponCode = "A1000", DiscountValue = 100 });
            //_discounts.Add(new Discount { CouponCode = "A250", DiscountValue = 200 });
            //_discounts.Add(new Discount { CouponCode = "A750", DiscountValue = 300 });
        }

        //public string BookTicket(Booking user)
        //{
        //    string pnr = "";
        //    var dateTime = DateTime.Now;
        //    List<string> existingList = new List<string>();
        //    foreach (Booking item in _user)
        //    {
        //        //if (item.SelectSeatNo.SequenceEqual(user.SelectSeatNo))
        //        //{
        //        //    return "Please select the available seats";
        //        //}
        //        if (item.SelectSeatNo != null && user.SelectSeatNo != null)
        //        {
        //            existingList = item.SelectSeatNo.Intersect(user.SelectSeatNo, StringComparer.OrdinalIgnoreCase).ToList();
        //            if (existingList.Count > 0)
        //            {
        //                return "Please select the available seats";
        //            }
        //        }
        //    }

        //    if (existingList.Count == 0)
        //    {
        //        _user.Add(user);
        //        string ticketCount = _user.Count().ToString();
        //        if (ticketCount.Length == 1)
        //        {
        //            ticketCount = "000" + ticketCount;
        //        }
        //        else if (ticketCount.Length == 2)
        //        {
        //            ticketCount = "00" + ticketCount;
        //        }
        //        else if (ticketCount.Length == 3)
        //        {
        //            ticketCount = "0" + ticketCount;
        //        }

        //        pnr = dateTime.Year.ToString() + dateTime.Month.ToString() + dateTime.Day.ToString() + user.FlightNo.ToString() + ticketCount;

        //        user.PNR = pnr;
        //        user.Status = "Confirmed";

        //        return "Your tickets have been successfully reserved.\n\n" + "Please find the PNR number for more details: " + pnr;
        //    }

        //    return "Please enter the valid details";
        //}

        //public string CancelTicket(string pnr)
        //{
        //    List<Booking> user = new List<Booking>();
        //    user = _user.Where(a => a.PNR == pnr).ToList();
        //    if (_user.Contains(user[0]))
        //    {
        //        //_user.Remove(user[0]);
        //        user[0].Status = "Cancelled";
        //        user[0].SelectSeatNo = null;
        //        return "Your tickets have been successfully cancelled";
        //    }

        //    return "Please enter the correct PNR number.";
        //}

        public string BookTicket(tblBooking user)
        {
            string pnr = "";
            var dateTime = DateTime.Now;

            List<tblInventory> list = this._userContext.Inventory.Where(a => (a.Airline == user.FlightName) && 
                                        (a.FlightNo == user.FlightNo) && (a.FlightSource == user.FlightSource) && (a.FlightDestination == user.FlightDestination)
                                        && (a.StartTime == user.StartTime) && (a.EndTime == user.EndTime)).ToList();

           tblLoginInformation loginInfo = this._userContext.LoginInfo.FirstOrDefault(a => (a.EmailID == user.EmailID) && (a.Role == "User"));
            if (list != null && list.Count > 0 && loginInfo != null)
            {
               List <tblBooking> _user = this._userContext.Bookings.Where(b => (b.FlightName == user.FlightName) &&
                                        (b.FlightNo == user.FlightNo) && (b.FlightSource == user.FlightSource) && (b.FlightDestination == user.FlightDestination)
                                        && (b.StartTime == user.StartTime) && (b.EndTime == user.EndTime) && (b.PassengerDetails == user.PassengerDetails)).ToList();
                if (_user.Count > 0)
                {
                    return "Please select the available seats or passenger details have";
                }

                pnr = dateTime.Year.ToString("0000") + dateTime.Month.ToString("00") + dateTime.Day.ToString("00") + dateTime.Hour.ToString("00") + dateTime.Minute.ToString("00") + dateTime.Second.ToString("00");

                user.PNR = pnr;
                user.Status = "Confirmed";

                //this._userContext.Bookings.Add(user);
                //this._userContext.SaveChanges();

                tblTicketDetails tblTicketDetails = new tblTicketDetails();
                tblTicketDetails.EmailID = user.EmailID;
                tblTicketDetails.NumberOfSeats = user.NumberOfSeats;
                tblTicketDetails.FlightNo = user.FlightNo;
                tblTicketDetails.FlightName = user.FlightName;
                tblTicketDetails.FlightSource = user.FlightSource;
                tblTicketDetails.FlightDestination = user.FlightDestination;
                tblTicketDetails.StartTime = user.StartTime;
                tblTicketDetails.EndTime = user.EndTime;
                tblTicketDetails.PassengerDetails = user.PassengerDetails;
                tblTicketDetails.Meals = user.MealPreference;
                tblTicketDetails.PNR = user.PNR;
                tblTicketDetails.Status = user.Status;
                tblTicketDetails.TotalCost = list[0].Cost;
                tblTicketDetails.AmountPaid = list[0].Cost;

                tblDiscount discount = this._userContext.Discounts.FirstOrDefault(a => (a.CouponCode == user.ApplyCoupon));
                if(discount !=null)
                {
                    tblTicketDetails.TotalCost = list[0].Cost;
                    tblTicketDetails.AmountPaid = list[0].Cost - discount.DiscountValue;
                    tblTicketDetails.Discount = discount.DiscountValue;
                }

                this._userContext.TicketDetails.Add(tblTicketDetails);
                this._userContext.SaveChanges();

                return "Reserved";
                //return "Your tickets have been successfully reserved.\n\n" + "Please find the PNR number for more details: " + pnr;
            }

            return "Please enter the valid details";
        }

        public string CancelBooking(string pnr)
        {
            List<tblTicketDetails> user = new List<tblTicketDetails>();
            user = this._userContext.TicketDetails.Where(a => a.PNR == pnr).ToList();
            if (user.Count > 0 && this._userContext.TicketDetails.Contains(user[0]))
            {
                this._userContext.TicketDetails.Remove(user[0]);
                this._userContext.SaveChanges();
                return "Cancelled";
            }
            return "Please enter the correct PNR number.";
        }

        public string CancelTicket(string pnr)
        {
            List<tblBooking> user = new List<tblBooking>();
            user = this._userContext.Bookings.Where(a => a.PNR == pnr).ToList();
            if (user.Count > 0 && this._userContext.Bookings.Contains(user[0]))
            {
                this._userContext.Bookings.Remove(user[0]);
                this._userContext.SaveChanges();
                //user[0].Status = "Cancelled";
                //user[0].SelectSeatNo = null;
                return "Cancelled";
            }
            return "Please enter the correct PNR number.";
        }

        public List<tblTicketDetails> GetBookingHistory(string emailID)
        {
            return this._userContext.TicketDetails.Where(a => a.EmailID == emailID).ToList();
        }

        public List<tblDiscount> GetDiscounts()
        {
            return this._userContext.Discounts.ToList();
        }

        public List<tblBooking> GetTicketDetails(string pnr)
        {
            return this._userContext.Bookings.Where(a => a.PNR == pnr).ToList();
        }

        public List<tblBooking> GetUserHistory(string emailID)
        {
            List<tblBooking> books = this._userContext.Bookings.Where(a => a.EmailID == emailID).ToList();
            return this._userContext.Bookings.Where(a => a.EmailID == emailID).ToList();
        }
    }
}
