using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Models
{
    public class AirlineRepository : IAirline
    {
        public List<Airline> _airlines;

        public List<Inventory> _inventoryList;

        public List<Admin> admins;

        public AirlineRepository()
        {
            _airlines = new List<Airline>();
            _airlines.Add(new Airline { FlightName = "AirIndia", Address = "adcd", Contact = 1234 });
            _airlines.Add(new Airline { FlightName = "Spice Jet", Address = "efgh", Contact = 5678 });
            _airlines.Add(new Airline { FlightName = "Emirates", Address = "ijkl", Contact = 9012 });
            _airlines.Add(new Airline { FlightName = "Indian Airlines", Address = "mnop", Contact = 3456 });

            _inventoryList = new List<Inventory>();
            _inventoryList.Add(new Inventory { FlightNo = 0001, Airline = "AirIndia", FlightSource = "Mumbai", FlightDestination = "Chennai", StartTime = new DateTime(2022, 5, 10, 7, 30, 00),  EndTime = new DateTime(2022, 5, 10, 9, 30, 00 ), ScheduledDays = "All Days", BusinessClassSeats = 15, GeneralSeats = 120, Cost = 5000, Meals = "Veg/Non-veg", OnewayorRoundtrip = "Roundtrip", FlightStatus = "Active"});
            _inventoryList.Add(new Inventory { FlightNo = 0002, Airline = "Spice Jet", FlightSource = "Bangalore", FlightDestination = "Chennai", StartTime = new DateTime(2022, 5, 8, 12, 30, 00), EndTime = new DateTime(2022, 5, 8, 13, 30, 00), ScheduledDays = "Saturday/Sunday", BusinessClassSeats = 12, GeneralSeats = 130, Cost = 4000, Meals = "Veg/Non-veg", OnewayorRoundtrip = "Roundtrip", FlightStatus = "Active" });
            _inventoryList.Add(new Inventory { FlightNo = 0003, Airline = "Emirates", FlightSource = "Delhi", FlightDestination = "Chennai", StartTime = new DateTime(2022, 5, 7, 17, 00, 00), EndTime = new DateTime(2022, 5, 7, 20, 00, 00), ScheduledDays = "Monday/Wedsnesday/Sunday", BusinessClassSeats = 10, GeneralSeats = 140, Cost = 7000, Meals = "Veg/Non-veg", OnewayorRoundtrip = "Oneway", FlightStatus = "Active" });
            _inventoryList.Add(new Inventory { FlightNo = 0004, Airline = "Indian Airlines", FlightSource = "Kolkata", FlightDestination = "Chennai", StartTime = new DateTime(2022, 5, 9, 14, 30, 00), EndTime = new DateTime(2022, 5, 9, 17, 00, 00), ScheduledDays = "All Days", BusinessClassSeats = 8, GeneralSeats = 150, Cost = 8000, Meals = "Veg/Non-veg", OnewayorRoundtrip = "Oneway", FlightStatus = "Active" });

            admins = new List<Admin>();
            admins.Add(new Admin { FirstName = "Subash", LastName = "Rajendran", EmailID = "224@ghj.com", AdminID = 224, Password = "SSD#1" });
            admins.Add(new Admin { FirstName = "Raja", LastName = "Vikram", EmailID = "254@ghj.com", AdminID = 254, Password = "GHT%3" });
            admins.Add(new Admin { FirstName = "Vijay", LastName = "Deva", EmailID = "578@ghj.com", AdminID = 578, Password = "qDF!6" });
        }

        public void AddAirline(Airline airline)
        {
            _airlines.Add(airline);
        }

        public List<Airline> GetAirlines()
        {
            return _airlines;
        }

        public string AddFlightInventory(Inventory inventory)
        {
            List<Inventory> existingList = _inventoryList.Where(a => a.FlightNo == inventory.FlightNo).ToList();
            if (existingList.Count == 0)
            {
                _inventoryList.Add(inventory);
                return "Flight inventory have been added successfully";
            }
            return "Please give the valid details";
        }

        public List<Inventory> GetFlightInventoryList()
        {
            return _inventoryList;
        }

        public List<Inventory> SearchFlights(string source, string destination, string onewayOrRoundtrip)
        {
            return _inventoryList.Where(a => (a.FlightSource == source) && (a.FlightDestination == destination) && (a.OnewayorRoundtrip == onewayOrRoundtrip)).ToList();
        }

        public List<Admin> GetAdminDetails()
        {
            return admins;
        }

        public List<Inventory> SearchFlightsByID(int id)
        {
            return _inventoryList.Where(a => a.FlightNo == id).ToList();
        }

        public string AdminLogin(Login login)
        {

            Admin existingAdmin = admins.FirstOrDefault(a => (a.EmailID == login.EmailID) && (a.Password == login.Password));
            if (existingAdmin != null)
            {
                return "You have been logged successfully";
            }
            return "Please enter valid id or password";
        }

        public string BlockAirline(int id, bool isActive)
        {
            var airline = _inventoryList.FirstOrDefault(a => a.FlightNo == id);

            if(airline == null)
            {
                return "Please enter the valid flight number";
            }

            if (!isActive)
            {
                airline.FlightStatus = "Blocked";
                return "Flight " + id.ToString() + " have been blocked successfully";
            }
            else
            {
                airline.FlightStatus = "Active";
                return "Flight " + id.ToString() + " have been activated successfully";
            }
        
        }
    }
}
