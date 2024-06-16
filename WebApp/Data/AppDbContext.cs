using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using System;

namespace WebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<ArchiveCategory> ArchiveCategories { get; set; }
        public DbSet<ArchiveProduct> ArchiveProducts { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>().HasData(
            //    new Category
            //    {
            //        Id = 1,
            //        CreatedDate = DateTime.UtcNow, 
            //        Name = "UI UX"
            //    },
            //    new Category
            //    {
            //        Id = 2,
            //        CreatedDate = DateTime.UtcNow, 
            //        Name = "BackEnd"
            //    },
            //    new Category
            //    {
            //        Id = 3,
            //        CreatedDate = DateTime.UtcNow,
            //        Name = "FrontEnd"
            //    }
            //);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Slider>().ToTable("Sliders");
        }
    }
}
