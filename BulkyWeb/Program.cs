using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAccess.Repository.RepositoriesClasses;
using BulkyWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();





builder.Services.AddDbContext<ApplicationDbContext>(
	op => op.UseSqlServer(
		builder.Configuration
		.GetConnectionString("Connection")));




//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped< IUnitOfWork  ,    UnitOfWork >();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "defult",
	pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
