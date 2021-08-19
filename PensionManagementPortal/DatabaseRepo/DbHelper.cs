using Microsoft.EntityFrameworkCore;
using PensionManagementPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementPortal.DatabaseRepo
{
    public class DbHelper:DbContext
    {
        public DbHelper(DbContextOptions<DbHelper> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PensionInput>().HasKey(x => new { x.Id, x.AadharNumber });
            modelBuilder.Entity<PensionData>().HasKey(x => new { x.Id, x.AadharNumber });

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<PensionData> pension { get; set; }
        public DbSet<PensionInput> userDetails { get; set; }
    }
}
