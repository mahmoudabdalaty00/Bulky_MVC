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
          

            modelBuilder.Entity<Product>().HasData(
           
               
              
                
 
        
                   new Product
                  {
                       Id = 7,
                        Title = "1984",
                        Description = "A dystopian novel by George Orwell about totalitarianism and surveillance.",
                        ISBM = "978-0451524935",
                        Author = "George Orwell",
                        ListPrice = 18.00,
                        Price = 14.00,
                        Price50 = 11.00,
                        Price100 = 9.00,
                       ImageURL = "",
                       CategoryId = 1,
                   },
                   new Product
                   {
                       Id = 8,
                        Title = "To Kill a Mockingbird",
                        Description = "A novel by Harper Lee that addresses serious issues like racial injustice.",
                        ISBM = "978-0061120084",
                        Author = "Harper Lee",
                        ListPrice = 22.00,
                        Price = 17.00,
                        Price50 = 13.00,
                        Price100 = 11.00,
                       ImageURL = "",
                       CategoryId = 1,
                   },
                    new Product
                    {
                        Id = 9,
                        Title = "Pride and Prejudice",
                        Description = "A romantic novel by Jane Austen that critiques the British landed gentry.",
                        ISBM = "978-1503290563",
                        Author = "Jane Austen",
                        ListPrice = 15.00,
                        Price = 12.00,
                        Price50 = 9.00,
                        Price100 = 7.00,
                        ImageURL = "",
                        CategoryId = 1,
                    },
                      new Product
                    {
                        Id = 10,
                          Title = "Moby Dick",
                          Description = "A novel by Herman Melville about the obsessive quest of Captain Ahab for revenge on Moby Dick, the giant white whale.",
                          ISBM = "978-1503280786",
                          Author = "Herman Melville",
                          ListPrice = 18.00,
                          Price = 14.00,
                          Price50 = 11.00,
                          Price100 = 9.00,
                          ImageURL = "",
                          CategoryId = 1,
                      },
                      new Product
                       {
                        Id = 11,
                          Title = "War and Peace",
                          Description = "A historical novel by Leo Tolstoy that chronicles the French invasion of Russia and its impact on Tsarist society.",
                          ISBM = "978-1420954308",
                          Author = "Leo Tolstoy",
                          ListPrice = 25.00,
                          Price = 20.00,
                          Price50 = 15.00,
                          Price100 = 12.00,
                          ImageURL = "",
                          CategoryId = 1,
                      },
                      new Product
                        {
                        Id = 12,
                          Title = "The Catcher in the Rye",
                          Description = "A novel by J.D. Salinger that explores themes of teenage angst and alienation.",
                          ISBM = "978-0316769488",
                          Author = "J.D. Salinger",
                          ListPrice = 17.00,
                          Price = 13.00,
                          Price50 = 10.00,
                          Price100 = 8.00,
                          ImageURL = "",
                          CategoryId = 1,
                      },
                      new Product
                       {
                        Id = 13,
                          Title = "Brave New World",
                          Description = "A dystopian novel by Aldous Huxley that explores a future society driven by technological advancements and consumerism.",
                          ISBM = "978-0060850524",
                          Author = "Aldous Huxley",
                          ListPrice = 19.00,
                          Price = 15.00,
                          Price50 = 12.00,
                          Price100 = 10.00,
                          ImageURL = "",
                          CategoryId = 1,
                      },
                      new Product
                       {
                        Id = 14,
                          Title = "The Alchemist",
                          Description = "A novel by Paulo Coelho about a shepherd's journey to discover his personal legend.",
                          ISBM = "978-0062315007",
                          Author = "Paulo Coelho",
                          ListPrice = 16.00,
                          Price = 12.00,
                          Price50 = 9.00,
                          Price100 = 7.00,
                          ImageURL = "",
                          CategoryId = 1,
                      },
                      new Product
                       {
                        Id = 15,
                          Title = "The Hobbit",
                          Description = "A fantasy novel by J.R.R. Tolkien about the adventures of Bilbo Baggins.",
                          ISBM = "978-0547928227",
                          Author = "J.R.R. Tolkien",
                          ListPrice = 15.00,
                          Price = 12.00,
                          Price50 = 9.00,
                          Price100 = 7.00,
                          ImageURL = "",
                          CategoryId = 1,
                      },
                      new Product
                       {
                        Id = 16,
                          Title = "The Picture of Dorian Gray",
                          Description = "A novel by Oscar Wilde about a young man whose portrait ages while he remains young.",
                          ISBM = "978-0141439570",
                          Author = "Oscar Wilde",
                          ListPrice = 14.00,
                          Price = 11.00,
                          Price50 = 8.00,
                          Price100 = 6.00,
                          ImageURL = "",
                          CategoryId = 1,

                      },
                      new Product
                       {
                        Id = 17,
                          Title = "The Chronicles of Narnia",
                          Description = "A series of seven fantasy novels by C.S. Lewis that take place in the magical land of Narnia.",
                          ISBM = "978-0066238500",
                          Author = "C.S. Lewis",
                          ListPrice = 30.00,
                          Price = 25.00,
                          Price50 = 20.00,
                          Price100 = 15.00,
                          ImageURL = "",
                          CategoryId = 1,
                      },
                      new Product
                       {
                        Id = 18,
                          Title = "The Catcher in the Rye",
                          Description = "A novel by J.D. Salinger that explores themes of teenage angst and alienation.",
                          ISBM = "978-0316769488",
                          Author = "J.D. Salinger",
                          ListPrice = 17.00,
                          Price = 13.00,
                          Price50 = 10.00,
                          Price100 = 8.00,
                          ImageURL = "",
                          CategoryId = 1,
                      },
                      new Product
                       {
                        Id = 19,
                          Title = "Fahrenheit 451",
                          Description = "A dystopian novel by Ray Bradbury about a future where books are banned.",
                          ISBM = "978-1451673319",
                          Author = "Ray Bradbury",
                          ListPrice = 16.00,
                          Price = 12.00,
                          Price50 = 9.00,
                          Price100 = 7.00,
                          ImageURL = "",
                          CategoryId = 1,
                      } 


                );


        }
    }
}
