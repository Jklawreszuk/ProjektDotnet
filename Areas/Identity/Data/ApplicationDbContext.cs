using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektDotnet.Models;

namespace ProjektDotnet.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<RecipeCategory> RecipeCategory { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Ratings> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RecipeCategory>()
                .HasKey(rc => new { rc.RecipeId, rc.CategoryId });
            builder.Entity<RecipeCategory>()
                .HasOne(rc => rc.Recipe)
                .WithMany(r => r.RecipeCategories)
                .HasForeignKey(rc => rc.RecipeId);
            builder.Entity<RecipeCategory>()
                .HasOne(rc => rc.Category)
                .WithMany(c => c.RecipeCategories)
                .HasForeignKey(rc => rc.CategoryId);

            builder.Entity<Favourites>()
                .HasKey(f => new { f.UserId, f.RecipeId });
            builder.Entity<Favourites>()
               .HasOne(f => f.Recipe)
               .WithMany(r => r.Favourites)
               .HasForeignKey(f => f.RecipeId);
            builder.Entity<Favourites>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favourites)
                .HasForeignKey(f => f.UserId);

            builder.Entity<Ratings>()
                .HasKey(r => new { r.UserId, r.RecipeId });
            builder.Entity<Ratings>()
               .HasOne(r => r.Recipe)
               .WithMany(re => re.Ratings)
               .HasForeignKey(r => r.RecipeId);
            builder.Entity<Ratings>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
