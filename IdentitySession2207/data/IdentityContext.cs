using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentitySession2207.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IdentitySession2207.data
{
    public class IdentityContext:IdentityDbContext<ApplicationUser> /*DbContext*/
    {
        private readonly IConfiguration config;

        public IdentityContext(IConfiguration _config)
        {
            config = _config;
        }
        public DbSet<Employee> employees { get; set; }

        //public DbSet<IdentityUser> Users { get; set; }
        //public DbSet<identityRole> roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("IdentityConnection"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
