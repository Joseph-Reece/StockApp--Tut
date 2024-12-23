using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Portfolio>().HasKey(p => new { p.AppUserId, p.StockId }); 
            builder.Entity<Portfolio>().HasOne(p => p.AppUser).WithMany(u => u.Portfolios).HasForeignKey(p => p.AppUserId);
            builder.Entity<Portfolio>().HasOne(p => p.Stock).WithMany(u => u.Portfolios).HasForeignKey(p => p.StockId);
            base.OnModelCreating(builder);
            List<IdentityRole> roles = [
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" }
            ];
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}