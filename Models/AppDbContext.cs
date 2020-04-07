using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EurocomV2.Models.Classes;
using Microsoft.AspNetCore.Identity;



namespace EurocomV2.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public Microsoft.EntityFrameworkCore.DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Seed();
        }
    }
}
