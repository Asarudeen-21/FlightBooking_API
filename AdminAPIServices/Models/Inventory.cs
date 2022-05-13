using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Models
{
    public class Inventory
    {
        [Key]
        public int FlightNo { get; set; }

        public string Airline { get; set; }

        public string FlightSource { get; set; }

        public string FlightDestination { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string ScheduledDays { get; set; }

        public int BusinessClassSeats { get; set; }

        public int GeneralSeats { get; set; }

        public string Meals { get; set; }

        public int Cost { get; set; }

        public string FlightStatus { get; set; }

        public string OnewayorRoundtrip { get; set; }

    }
}
