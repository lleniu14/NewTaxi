using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Models
{
    public class TaxiAPIContext : DbContext
    {
       
        public TaxiAPIContext(DbContextOptions<TaxiAPIContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<DriverOrder> DriversOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here
            modelBuilder.Entity<DriverOrder>().HasOne(x => x.Driver)
                .WithMany(x => x.DriversOrders).HasForeignKey(x => x.DriverId);

            modelBuilder.Entity<DriverOrder>().HasOne(x => x.Order)
                .WithMany(x => x.DriversOrders).HasForeignKey(x => x.OrderId);
        }
    }
}
