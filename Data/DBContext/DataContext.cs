using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cofidis.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.DBContext
{
    public class DataContext : DbContext
    {        //DB Set´s
        public DbSet<GrantingCredit> grantingCredits { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
            // Create database and apply migrations
            Database.EnsureCreated();
        
        }

    }
}
