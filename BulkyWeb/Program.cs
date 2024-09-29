using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAccess.Repository.RepositoriesClasses;
using Bulky.Utility;
using BulkyWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder
	.Configuration
	.GetConnectionString("ApplicationDbContextConnection")
	?? throw new InvalidOperationException(
		"Connection string 'ApplicationDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();





builder.Services.AddDbContext<ApplicationDbContext>(
	op => op.UseSqlServer(
		builder.Configuration
		.GetConnectionString("Connection")));

builder.Services.AddIdentity<IdentityUser , IdentityRole>
	(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ApplicationDbContext>();




//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped< IEmailSender ,EmailSender >();
builder.Services.AddRazorPages();
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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
	name: "defult",
	pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
