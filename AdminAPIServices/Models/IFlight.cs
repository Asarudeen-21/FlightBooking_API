using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Models
{
    public interface IFlight
    {
        List<tblAirline> GetAirlines();

        string AddAirline(tblAirline airline);

        string DeleteAirlines(string flightName);

        string UpdateAirlines(tblAirline tblAirline);

        string BlockAirline(int id, bool isActive);

        List<tblInventory> GetFlightInventoryList();

        string AddFlightInventory(tblInventory inventory);

        string DeleteInventory(int id);

        List<tblInventory> SearchFlights(string source, string destination, string onewayOrRoundtrip);

        List<tblInventory> SearchFlightsByID(int id);

        //List<tblAdmin> GetAdminDetails();
        List<tblLoginInformation> GetAdminDetails();

        string DeleteAdmin(int id);

        //string AdminRegistration(tblAdmin admin);
        string AdminRegistration(tblLoginInformation loginInfo);

        string AdminLogin(Login login);

        tblLoginInformation GetLoginInfo(string emailID);

        string AddDiscounts(UserAPIServices.Models.tblDiscount discounts);

        string DeleteDiscount(string couponCode);

    }
}
