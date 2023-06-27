using Microsoft.EntityFrameworkCore;
using ParkingGarages_API.Models;

namespace ParkingGarages_API.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {         
        }

        public DbSet<ParkingGarage> ParkingGarages { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Parking> Parkings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DateTime localDateTime = DateTime.Now;
            DateTime localDateTimePlusOneHour = DateTime.Now.AddHours(1);
            DateTime utcDateTime = localDateTime.ToUniversalTime();
            DateTime utcDateTimePlusOneHour = localDateTimePlusOneHour.ToUniversalTime();

            modelBuilder.Entity<ParkingGarage>().HasData(
                new ParkingGarage()
                {
                    Id = 1,
                    Address = "Cím 1",
                    NumberOfSpaces = 10
                },
                new ParkingGarage()
                {
                    Id = 2,
                    Address = "Cím 2",
                    NumberOfSpaces = 30
                },
                new ParkingGarage()
                {
                    Id = 3,
                    Address = "Cím 3",
                    NumberOfSpaces = 15
                }
                );

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 1,
                    Plate = "LOL-404",
                    Color = "Piros",
                    Type = "Mazda CX-30"
                },
                new Car()
                {
                    Id = 2,
                    Plate = "BAD-400",
                    Color = "Fehér",
                    Type = "Toyota Corolla"
                },
                new Car()
                {
                    Id = 3,
                    Plate = "OKE-200",
                    Color = "Szürke",
                    Type = "Mercedes-Benz CLA 250"
                }
                );

            modelBuilder.Entity<Parking>().HasData(
                new Parking()
                {
                    Id = 1,
                    ParkingGarageId = 1,
                    CarId = 1,
                    StartOfParking = utcDateTime,
                    EndOfParking = utcDateTimePlusOneHour
                },
                new Parking()
                {
                    Id = 2,
                    ParkingGarageId = 2,
                    CarId = 3,
                    StartOfParking = utcDateTime,
                    EndOfParking = utcDateTimePlusOneHour
                },
                new Parking()
                {
                    Id = 3,
                    ParkingGarageId = 3,
                    CarId = 2,
                    StartOfParking = utcDateTime,
                    EndOfParking = utcDateTimePlusOneHour
                }
                );
        }
    }
}
