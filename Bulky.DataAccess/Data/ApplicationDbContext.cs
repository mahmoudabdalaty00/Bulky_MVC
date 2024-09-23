using Bulky.Models.Models;
using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 }
              , new Category { Id = 2, Name = "sci", DisplayOrder = 2 },
                new Category { Id = 3, Name = "his", DisplayOrder = 3 },
                new Category { Id = 4, Name = "ad", DisplayOrder = 4 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Introduction to Machine Learning",
                    Description = "Learn the basics of machine learning.",
                    ISBM = "978-1-23-456789-0",
                    Author = "Charlie Green",
                    ListPrice = 44.99,
                    Price = 39.99,
                    Price50 = 37.99,
                    Price100 = 34.99
                }
                , new Product
                {
                    Id = 2,
                    Title = "Data Structures and Algorithms",
                    Description = "A comprehensive guide to data structures and algorithms.",
                    ISBM = "978-0-98-765432-1",
                    Author = "Bob Brown",
                    ListPrice = 59.99,
                    Price = 54.99,
                    Price50 = 52.99,
                    Price100 = 49.99
                },
                new Product
                {
                    Id = 3,
                    Title = "Mastering ASP.NET Core",
                    Description = "Build modern web applications with ASP.NET Core.",
                    ISBM = "978-0-12-345678-9",
                    Author = "Alice Johnson",
                    ListPrice = 39.99,
                    Price = 34.99,
                    Price50 = 32.99,
                    Price100 = 29.99
                },
                new Product
                {
                    Id = 4,
                    Title = "Advanced C# Techniques",
                    Description = "Deep dive into advanced C# concepts.",
                    ISBM = "978-1-23-456789-7",
                    Author = "Jane Smith",
                    ListPrice = 49.99,
                    Price = 44.99,
                    Price50 = 42.99,
                    Price100 = 39.99
                },
                new Product
                {
                    Id = 5,
                    Title = "C# Programming Basics",
                    Description = "An introduction to C# programming.",
                    ISBM = "978-3-16-148410-0",
                    Author = "John Doe",
                    ListPrice = 29.99,
                    Price = 24.99,
                    Price50 = 22.99,
                    Price100 = 19.99
                }



                );


        }
    }
}
