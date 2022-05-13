using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Models
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {

        }

        public DbSet<tblInventory> Inventory { get; set; }

        public DbSet<tblAirline> RegisteredAirlines { get; set; }

        public DbSet<tblAdmin> Admins { get; set; }

        public DbSet<tblLoginInformation> LoginInfo { get; set; }

        public DbSet<UserAPIServices.Models.tblDiscount> Discounts { get; set; }
    }
}
