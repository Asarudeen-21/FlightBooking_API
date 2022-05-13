using AdminAPIServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AdminAPIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        Result result = new Result();
        //private readonly FlightRepository flightRepository;
        public IFlight flightRepository;
        public ValuesController(IFlight repository)
        {
            this.flightRepository = repository;
        }

        [HttpGet]
        [Route("Airlines")]
        public List<tblAirline> GetAirlines()
        {
            return flightRepository.GetAirlines();
        }

        [HttpPost]
        [Route("AddingAirlines")]
        public Result AddAirline([FromBody] tblAirline airline)
        {
            result.message = flightRepository.AddAirline(airline);
            return result;
        }

        [HttpPut]
        [Route("Block")]
        public Result BlockAirlines(int id, bool isActive)
        {
            result.message = flightRepository.BlockAirline(id, isActive); 
            return result;
        }

        [HttpGet]
        [Route("GetInventories")]
        public List<tblInventory> GetFlightInventoryList()
        {
            return flightRepository.GetFlightInventoryList();
        }

        [HttpPost]
        [Route("AddInventory")]
        public Result AddFlightInventory([FromBody]tblInventory inventory)
        {
            result.message = this.flightRepository.AddFlightInventory(inventory);
            return result;
            //return this.flightRepository.AddFlightInventory(inventory);
        }

        [HttpGet]
        [Route("Search")]
        public List<tblInventory> SearchFlights(string source, string destination, string onewayOrRoundtrip)
        {
            return flightRepository.SearchFlights(source, destination, onewayOrRoundtrip);
        }

        [HttpGet]
        [Route("SearchByID")]
        public List<tblInventory> SearchFlightsByID(int id)
        {
            return flightRepository.SearchFlightsByID(id);
        }

        [HttpGet]
        [Route("GetAdmin")]
        public List<tblLoginInformation> GetAdmin()
        {
            return flightRepository.GetAdminDetails();
        }

        [HttpGet]
        [Route("GetLoginInfo")]
        public tblLoginInformation GetLoginInfo(string emailID)
        {
            return flightRepository.GetLoginInfo(emailID);
        }

        [HttpDelete]
        [Route("DeleteAdmin")]
        public Result DeleteAdmin(int id)
        {
            result.message = flightRepository.DeleteAdmin(id);
            return result;
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        public Result AdminRegistration([FromBody]tblLoginInformation tblLoginInformation)
        {
            result.message = flightRepository.AdminRegistration(tblLoginInformation);
            return result;
        }

        [HttpDelete()]
        [Route("DeleteAirlines")]
        public Result DeleteAirline(string flightName)
        {
            result.message = flightRepository.DeleteAirlines(flightName);
            return result;
        }

        [HttpDelete()]
        [Route("DeleteDiscount")]
        public Result DeleteDiscount(string couponCode)
        {
            result.message = flightRepository.DeleteDiscount(couponCode);
            return result;
        }

        [HttpDelete()]
        [Route("DeleteInventory")]
        public Result DeleteInventory(int id)
        {
            result.message = flightRepository.DeleteInventory(id);
            return result;
        }

        [HttpPost]
        [Route("Login")]
        public Result Login([FromBody] Login login)
        {
            result.message = flightRepository.AdminLogin(login);
            return result;
            //return flightRepository.AdminLogin(login);
        }

        [HttpPost]
        [Route("AddDiscounts")]
        public Result AddDiscounts([FromBody] UserAPIServices.Models.tblDiscount discount)
        {
            result.message = this.flightRepository.AddDiscounts(discount);
            return result;
        }

        [HttpGet("Ping")]
        public string Ping()
        {
            return "Hello World";
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Result 
    {
        public string message;

    }
}
