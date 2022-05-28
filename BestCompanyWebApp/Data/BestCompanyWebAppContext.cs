using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BestCompanyWebApp.Models;

namespace BestCompanyWebApp.Data
{
    public class BestCompanyWebAppContext : DbContext
    {
        public BestCompanyWebAppContext (DbContextOptions<BestCompanyWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<BestCompanyWebApp.Models.ActionModel>? ActionModels { get; set; }

        public DbSet<BestCompanyWebApp.Models.Employee>? Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionModel>()
                .ToTable("Actions");
        }
    }
}
