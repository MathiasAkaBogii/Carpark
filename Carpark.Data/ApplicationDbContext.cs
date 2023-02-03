using Carpark.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Carpark.Business.Repositories
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Car> Car { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 1,
                    Manufacturer = "Audi",
                    Model = "A4",
                    TicketBought = true,
                },
                new Car()
                {
                    Id = 2,
                    Manufacturer = "VW",
                    Model = "Golf",
                    TicketBought = false,
                });
        }
    }
}