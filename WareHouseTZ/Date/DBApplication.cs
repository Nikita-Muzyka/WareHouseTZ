using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseTZ.Modal;

namespace WareHouseTZ.Date
{
    public class DBApplication : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Coming> Comings { get; set; }
        public DbSet<Consumption> Consumptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=WarehouseDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;";
        }
    }
}
