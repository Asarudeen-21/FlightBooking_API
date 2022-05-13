using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPIServices.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserAPIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private Result result = new Result();
        public IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpPost]
        [Route("Search")]
        public string Search()
        {
            return "Search the flights";
        }

        [HttpPost]
        [Route("Booking")]
        public Result Booking([FromBody] tblBooking user)
        {
            result.message = _user.BookTicket(user);
            return result;
            //return _user.BookTicket(user);
        }

        [HttpGet]
        [Route("TicketDetails")]
        public List<tblBooking> TicketDetails(string pnr)
        {
            return _user.GetTicketDetails(pnr);
        }

        [HttpGet]
        [Route("History")]
        public List<tblBooking> UserHistory(string emailID)
        {
            return _user.GetUserHistory(emailID);
        }

        [HttpGet]
        [Route("BookingHistory")]
        public List<tblTicketDetails> BookingHistory(string emailID)
        {
            return _user.GetBookingHistory(emailID);
        }

        [HttpGet]
        [Route("GetDiscounts")]
        public List<tblDiscount> GetDiscounts()
        {
            return _user.GetDiscounts();
        }

        [HttpDelete]
        [Route("Cancel")]
        public Result Cancellation(string pnr)
        {
            result.message = _user.CancelTicket(pnr);
            return result;
            //return _user.CancelTicket(pnr);
        }

        [HttpDelete]
        [Route("CancelBooking")]
        public Result CancelBooking(string pnr)
        {
            result.message = _user.CancelBooking(pnr);
            return result;
        }

        [HttpGet("Ping")]
        public string Ping()
        {
            return "Hello User";
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
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
