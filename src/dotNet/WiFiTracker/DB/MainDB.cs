using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiFiTracker.DB
{
    public class MainDB:DbContext
    {
        public MainDB(DbContextOptions<MainDB> options) : base(options)
        {
            
        }

        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<Point> Points { get; set; }
    }
}
