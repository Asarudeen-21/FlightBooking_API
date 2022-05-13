using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPIServices.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<tblBooking> Bookings { get; set; }

        public DbSet<tblTicketDetails> TicketDetails { get; set; }

        public DbSet<tblDiscount> Discounts { get; set; }

        public DbSet<tblInventory> Inventory { get; set; }

        public DbSet<tblLoginInformation> LoginInfo { get; set; }


    }
}
