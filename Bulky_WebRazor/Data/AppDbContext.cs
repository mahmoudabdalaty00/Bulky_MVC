using Bulky_WebRazor.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky_WebRazor.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }



        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 }
              , new Category { Id = 2, Name = "sci", DisplayOrder = 2 },
                new Category { Id = 3, Name = "his", DisplayOrder = 3 },
                new Category { Id = 4, Name = "ad", DisplayOrder = 4 }
                );

        }

    }
}
