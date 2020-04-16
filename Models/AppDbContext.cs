using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EurocomV2.Models.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EurocomV2.Models;


namespace EurocomV2.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public Microsoft.EntityFrameworkCore.DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            model.Seed();
        }

        public DbSet<User> User { get; set; }

        public DbSet<EurocomV2.Models.AdminCRUD> AdminCRUD { get; set; }
    }
}
