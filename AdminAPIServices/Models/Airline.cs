using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Models
{
    public class Airline
    {
        [Key]
        public string FlightName { get; set; }

        public long Contact { get; set; }

        public string Address { get; set; }
    }
}
