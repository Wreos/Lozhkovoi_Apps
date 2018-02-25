using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    public class MobileContext:DbContext
    {
        public DbSet<Type> Types { get; set; }
        public DbSet< Worker> Workers { get; set; }


        public MobileContext ()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Mobile.db");
        }

    }
}
