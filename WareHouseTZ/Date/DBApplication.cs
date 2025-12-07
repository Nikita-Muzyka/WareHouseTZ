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
        public DbSet<Product> Product { get; set; }
        public DbSet<Coming> Coming { get; set; }
        public DbSet<Consumption> Consumption { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=MYZUKA\\SQLEXPRESS;Database=WareHouseTZ;Trusted_Connection=true;TrustServerCertificate=true;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
