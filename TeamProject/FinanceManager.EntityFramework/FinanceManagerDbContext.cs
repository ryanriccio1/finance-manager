using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.EntityFramework
{
    // this is the main interface between EntityFramework and our DB
    // we apply CRUD operations later
    public class FinanceManagerDbContext : DbContext
    {
        // use the defualt constructor
        public FinanceManagerDbContext(DbContextOptions options) : base(options) { }
        
        // setup the tables (or sets) in our DB
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AssetTransaction> AssetTransaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // this basically performs a SQL JOIN
            // every AssetTransaction in our DB includes the individual properties of an Asset
            // so Asset turns into Asset_Symbol and Asset_PricePerShare in the table
            modelBuilder.Entity<AssetTransaction>().OwnsOne(a => a.Asset);
            base.OnModelCreating(modelBuilder);
        }
    }
}
