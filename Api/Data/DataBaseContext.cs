using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Configurations.Entities;

namespace Api.Data
{
    public class DataBaseContext : IdentityDbContext<ApiUser>
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
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new HotelConfiguration());
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}