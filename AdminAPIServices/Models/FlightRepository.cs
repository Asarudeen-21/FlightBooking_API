using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Models
{
    public class FlightRepository : IFlight
    {
        //private readonly InventoryContext inventoryContext;
        public InventoryContext inventoryContext;
        public FlightRepository(InventoryContext inventoryContext)
        {
            this.inventoryContext = inventoryContext;
        }

        public string AddAirline(tblAirline airline)
        {
            List<tblAirline> existingList = this.inventoryContext.RegisteredAirlines.Where(a => a.FlightName == airline.FlightName).ToList();
            if (existingList.Count == 0)
            {
                this.inventoryContext.RegisteredAirlines.Add(airline);
                this.inventoryContext.SaveChanges();
                return "Registered";
            }
            return "please enter the valid details";
        }

        public string AddDiscounts(UserAPIServices.Models.tblDiscount discounts)
        {
            List<UserAPIServices.Models.tblDiscount> existingList = this.inventoryContext.Discounts.Where(a => a.CouponCode == discounts.CouponCode).ToList();
            if (existingList.Count == 0)
            {
                this.inventoryContext.Discounts.Add(discounts);
                this.inventoryContext.SaveChanges();
                return "Added";
            }
            return "This discount code has already been used.";
        }

        public string AddFlightInventory(tblInventory inventory)
        {
            List<tblInventory> existingList = this.inventoryContext.Inventory.Where(a => a.Airline == inventory.Airline).ToList();
            if (existingList.Count == 0)
            {
                this.inventoryContext.Inventory.Add(inventory);
                this.inventoryContext.SaveChanges();
                return "Added";
                //return "Flight inventory have been added successfully";
            }
            return "Invalid";
        }

        public string AdminLogin(Login login)
        {
            tblLoginInformation existingInfo = this.inventoryContext.LoginInfo.FirstOrDefault(a => (a.EmailID == login.EmailID) && (a.Password == login.Password) && (a.Role == login.Role));
            //tblAdmin existingAdmin = this.inventoryContext.Admins.FirstOrDefault(a => (a.EmailID == login.EmailID) && (a.Password == login.Password));
            if (existingInfo != null)
            { 
                return "Success";
            }
            return "Please enter valid id or password or role";
        }

        //public string AdminRegistration(tblAdmin admin)
        //{
        //    List<tblAdmin> existingmail = this.inventoryContext.Admins.Where(a => a.EmailID == admin.EmailID).ToList();

        //    List<tblAdmin> existingadmin = this.inventoryContext.Admins.Where(a => a.AdminID == admin.AdminID).ToList();

        //    if (existingmail.Count == 0 && existingadmin.Count == 0)
        //    {
        //        this.inventoryContext.Admins.Add(admin);
        //        this.inventoryContext.SaveChanges();

        //        return "Registered";
        //    }
        //    else if (existingadmin.Count > 0)
        //    {
        //        return "This admin id is not valid";
        //    }
        //    else
        //    {
        //        return "This email id is already registered";
        //    }
        //}

        public string AdminRegistration(tblLoginInformation loginInfo)
        {
            List<tblLoginInformation> existingmail = this.inventoryContext.LoginInfo.Where(a => a.EmailID == loginInfo.EmailID).ToList();

            List<tblLoginInformation> existingInfo = this.inventoryContext.LoginInfo.Where(a => a.LoginID == loginInfo.LoginID).ToList();

            if (existingmail.Count == 0 && existingInfo.Count == 0)
            {
                this.inventoryContext.LoginInfo.Add(loginInfo);
                this.inventoryContext.SaveChanges();

                return "Registered";
            }
            else if (existingInfo.Count > 0)
            {
                return "This login id is not valid";
            }
            else
            {
                return "This email id is already registered";
            }
        }

        public string BlockAirline(int id, bool isActive)
        {
            var airline = this.inventoryContext.Inventory.FirstOrDefault(a => a.FlightNo == id);

            if (airline == null)
            {
                return "Invalid";
                //return "Please enter the valid flight number";
            }

            if (!isActive && airline.FlightStatus == "Active")
            {
                airline.FlightStatus = "Blocked";
                this.inventoryContext.SaveChanges();
                return "Blocked";
                //return "Flight " + id.ToString() + " have been blocked successfully";
            }
            else if (isActive && airline.FlightStatus == "Blocked")
            {
                airline.FlightStatus = "Active";
                this.inventoryContext.SaveChanges();
                return "Active";
                //return "Flight " + id.ToString() + " have been activated successfully";
            }
            else
            {
                return "No changes";
                //return "The flight is already in " + airline.FlightStatus.ToString() + " state";
            }
        }

        public string DeleteAdmin(int id)
        {
            //List<tblAdmin> admin = new List<tblAdmin>();
            //admin = this.inventoryContext.Admins.Where(a => a.AdminID == id).ToList();
            //if (admin.Count >0 &&  this.inventoryContext.Admins.Contains(admin[0]))
            //{
            //    this.inventoryContext.Admins.Remove(admin[0]);
            //    this.inventoryContext.SaveChanges();
            //    return "AdminDeleted";
            //}
            //return "Invalid";
            List<tblLoginInformation> login = new List<tblLoginInformation>();
            login = this.inventoryContext.LoginInfo.Where(a => a.LoginID == id).ToList();
            if (login.Count > 0 && this.inventoryContext.LoginInfo.Contains(login[0]))
            {
                this.inventoryContext.LoginInfo.Remove(login[0]);
                this.inventoryContext.SaveChanges();
                return "InfoDeleted";
            }
            return "Invalid";
        }

        public string DeleteAirlines(string flightName)
        {
            List<tblAirline> user = new List<tblAirline>();
            user = this.inventoryContext.RegisteredAirlines.Where(a => a.FlightName == flightName).ToList();
            if (user.Count > 0 && this.inventoryContext.RegisteredAirlines.Contains(user[0]))
            {
                this.inventoryContext.RegisteredAirlines.Remove(user[0]);
                this.inventoryContext.SaveChanges();
                return "Deleted";
            }
            return "Invalid";
        }

        public string DeleteDiscount(string couponCode)
        {
            List<UserAPIServices.Models.tblDiscount> coupons = new List<UserAPIServices.Models.tblDiscount>();
            coupons = this.inventoryContext.Discounts.Where(a => a.CouponCode == couponCode).ToList();
            if (coupons.Count > 0 && this.inventoryContext.Discounts.Contains(coupons[0]))
            {
                this.inventoryContext.Discounts.Remove(coupons[0]);
                this.inventoryContext.SaveChanges();
                return "CouponDeleted";
            }
            return "Invalid";
        }

        public string DeleteInventory(int id)
        {
            List<tblInventory> inventory = new List<tblInventory>();
            inventory = this.inventoryContext.Inventory.Where(a => a.FlightNo == id).ToList();
            if (inventory.Count > 0 && this.inventoryContext.Inventory.Contains(inventory[0]))
            {
                this.inventoryContext.Inventory.Remove(inventory[0]);
                this.inventoryContext.SaveChanges();
                return "InventoryDeleted";
            }
            return "Please enter the valid id.";
        }

        public tblLoginInformation GetLoginInfo(string emailID)
        {
            return this.inventoryContext.LoginInfo.FirstOrDefault(a => a.EmailID == emailID);
        }

        public List<tblLoginInformation> GetAdminDetails()
        {
            return this.inventoryContext.LoginInfo.ToList();
        }

        public List<tblAirline> GetAirlines()
        {
            return this.inventoryContext.RegisteredAirlines.ToList();
        }

        public List<tblInventory> GetFlightInventoryList()
        {
            return this.inventoryContext.Inventory.ToList();
        }

        public List<tblInventory> SearchFlights(string source, string destination, string onewayOrRoundtrip)
        {
            return this.inventoryContext.Inventory.Where(a => (a.FlightSource == source) && (a.FlightDestination == destination) && (a.OnewayorRoundtrip == onewayOrRoundtrip)).ToList();
        }

        public List<tblInventory> SearchFlightsByID(int id)
        {
            return this.inventoryContext.Inventory.Where(a => a.FlightNo == id).ToList();
        }

        public string UpdateAirlines(tblAirline tblAirline)
        {
            List<tblAirline> list = new List<tblAirline>();
            list = this.inventoryContext.RegisteredAirlines.Where(a => (a.FlightName == tblAirline.FlightName)&&
            (a.Contact == tblAirline.Contact) && (a.Address == tblAirline.Address)).ToList();
            if (list.Count > 0 && this.inventoryContext.RegisteredAirlines.Contains(list[0]))
            {
                return "Added";
            }
            return "Please enter the valid details.";
        }
    }
}
