using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class INtiAndAddProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBM", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Charlie Green", "Learn the basics of machine learning.", "978-1-23-456789-0", 44.990000000000002, 39.990000000000002, 34.990000000000002, 37.990000000000002, "Introduction to Machine Learning" },
                    { 2, "Bob Brown", "A comprehensive guide to data structures and algorithms.", "978-0-98-765432-1", 59.990000000000002, 54.990000000000002, 49.990000000000002, 52.990000000000002, "Data Structures and Algorithms" },
                    { 3, "Alice Johnson", "Build modern web applications with ASP.NET Core.", "978-0-12-345678-9", 39.990000000000002, 34.990000000000002, 29.989999999999998, 32.990000000000002, "Mastering ASP.NET Core" },
                    { 4, "Jane Smith", "Deep dive into advanced C# concepts.", "978-1-23-456789-7", 49.990000000000002, 44.990000000000002, 39.990000000000002, 42.990000000000002, "Advanced C# Techniques" },
                    { 5, "John Doe", "An introduction to C# programming.", "978-3-16-148410-0", 29.989999999999998, 24.989999999999998, 19.989999999999998, 22.989999999999998, "C# Programming Basics" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
