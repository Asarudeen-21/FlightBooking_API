using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPIServices.Models
{
    public class tblBooking
    {
        [Key]
        public string EmailID { get; set; }

        public int NumberOfSeats { get; set; }

        public int FlightNo { get; set; }

        public string FlightName { get; set; }

        public string FlightSource { get; set; }

        public string FlightDestination { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        //public List<Passenger> PassengerDetails { get; set; }
        public string PassengerDetails { get; set; }

        public string MealPreference { get; set; }

        //public List<string> SelectSeatNo { get; set; }

        public string PNR { get; set; }

        public string Status { get; set; }

        public string ApplyCoupon { get; set; }
    }

    public class tblTicketDetails
    {
        [Key]
        public string EmailID { get; set; }

        public int NumberOfSeats { get; set; }

        public int FlightNo { get; set; }

        public string FlightName { get; set; }

        public string FlightSource { get; set; }

        public string FlightDestination { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string PassengerDetails { get; set; }

        public string Meals { get; set; }

        public string PNR { get; set; }

        public string Status { get; set; }

        public double TotalCost { get; set; }

        public double AmountPaid { get; set; }

        public double Discount { get; set; }
    }

    public class tblLoginInformation
    {
        [Key]
        public int LoginID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public string EmailID { get; set; }

        public long PhoneNumber { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
