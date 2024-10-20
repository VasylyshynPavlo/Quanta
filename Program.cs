using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Data.Entities;
//using Quanta.Hubs;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("LocalDb") ?? throw new InvalidOperationException("Connection string 'LocalDb' not found.");

builder.Services.AddDbContext<QuantaDbContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddSignalR();

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<QuantaDbContext>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// app.MapGet("/Account/Register", async context =>
// {
//     context.Response.Redirect("/Identity/Account/Register");
// });

//app.MapHub<ChatHub>("/chatHub");

app.Run();