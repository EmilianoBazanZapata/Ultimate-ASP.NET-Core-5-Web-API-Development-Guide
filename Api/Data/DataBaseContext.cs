using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }
        //referencia a la clase Country
        public DbSet<Country> Countries { get; set; }
        //referencia a la clase Hotel
        public DbSet<Hotel> hotels { get; set; }
        //datos que gregare en la base de datos desde una migracion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                new Country
                {
                    Id = 2,
                    Name = "Bahamas",
                    ShortName = "BS"
                },
                new Country
                {
                    Id = 3,
                    Name = "Cayman Island",
                    ShortName = "CI"
                }
            );
            builder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Sandals Resort and Spa",
                    Address = "Negril",
                    CountryId = 1,
                    Rating = 4.5
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Comfort Suites",
                    Address = "George Town",
                    CountryId = 2,
                    Rating = 5
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Grand Palldium",
                    Address = "Nassua",
                    CountryId = 3,
                    Rating = 2
                }
            );
        }
    }
}