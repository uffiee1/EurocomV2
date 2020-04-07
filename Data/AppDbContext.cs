using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EurocomV2.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EurocomV2.Models
{
    /// <summary>
    /// The Database representational model for our application
    /// </summary>
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Default constructor, expecting database options in 
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// The User for the application
        /// </summary>
        public DbSet<User> User { get; set; } 
    }
}
