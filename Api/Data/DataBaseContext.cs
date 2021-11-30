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
    }
}