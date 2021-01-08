using Microsoft.EntityFrameworkCore;
using MobileAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI.Data
{
    public class BoodschapContext : DbContext
    {
        public BoodschapContext(DbContextOptions<BoodschapContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Boodschap> Boodschappen { get; set; }
        public DbSet<BoodschapRow> BoodschapRows { get; set; }
        public DbSet<Product> Producten { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Boodschap>().ToTable("Boodschap");
            modelBuilder.Entity<BoodschapRow>().ToTable("BoodschapRow");
            modelBuilder.Entity<Product>().ToTable("Product");
        }

    }
}
