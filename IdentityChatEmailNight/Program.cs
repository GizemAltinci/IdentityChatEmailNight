using IdentityChatEmailNight.Context;
using IdentityChatEmailNight.Entities;
using IdentityChatEmailNight.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<EmailContext>();
builder.Services.AddIdentity<AppUser,IdentityRole>(options =>
{
    // Default olan en kýsa þifre uzunluðunu deðiþtirmek istersek
    options.Password.RequiredLength = 10;
})
	.AddEntityFrameworkStores<EmailContext>().AddErrorDescriber<CustomIdentityValidator>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
