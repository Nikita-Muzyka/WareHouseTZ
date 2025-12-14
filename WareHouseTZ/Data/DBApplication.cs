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
        public DBApplication(DbContextOptions<DBApplication> options) : base(options) { }
        public DbSet<Product> Product { get; set; }
        public DbSet<Coming> Coming { get; set; }
        public DbSet<Consumption> Consumption { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }
    }
}
