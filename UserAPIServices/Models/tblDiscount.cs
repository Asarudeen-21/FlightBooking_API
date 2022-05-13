using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPIServices.Models
{
    public class tblDiscount
    {
        [Key]
        public string CouponCode { get; set; }

        public int DiscountValue { get; set; }

    }

    public class tblInventory
    {
        [Key]
        public int FlightNo { get; set; }

        public string Airline { get; set; }

        //[Display(Name = "Flight source")]
        public string FlightSource { get; set; }

        //[Display(Name = "Flight destination")]
        public string FlightDestination { get; set; }

        //[Display(Name = "Start time")]
        public DateTime StartTime { get; set; }

        //[Display(Name = "End time")]
        public DateTime EndTime { get; set; }

        //[Display(Name = "Scheduled days")]
        public string ScheduledDays { get; set; }

        //[Display(Name = "Business class seats")]
        public int BusinessClassSeats { get; set; }

        //[Display(Name = "General seats")]
        public int GeneralSeats { get; set; }

        public string Meals { get; set; }

        public int Cost { get; set; }

        //[Display(Name = "Flight status")]
        public string FlightStatus { get; set; }

        //[Display(Name = "oneway or round trip")]
        public string OnewayorRoundtrip { get; set; }
    }
}
