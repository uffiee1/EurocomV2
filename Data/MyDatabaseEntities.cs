using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DbContext = System.Data.Entity.DbContext;

namespace EurocomV2.Models
{
    public partial class MyDatabaseEntities : DbContext
    {

        public MyDatabaseEntities() : base("name=MyDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
    }
}
