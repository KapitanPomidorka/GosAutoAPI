using GosAutoAPI.IServices;
using GosAutoAPI.Services;
using Infrastructure.Data;
using Infrastructure.IRepositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Domain.Models;
using GosAutoAPI.IServices.Auth;
using GosAutoAPI.Services.Auth;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDataProtection();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAuthentication, Authentication>();
builder.Services.AddScoped<IVehiclesRepository, VehiclesRepository>();
builder.Services.AddScoped<IDriversRepository, DriversRepository>();
builder.Services.AddScoped<IFinesRepository, FinesRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IFineService, FineService>();
builder.Services.AddScoped<IEncrypt, PBKDF2Encrypt>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddDbContext<GosAutoDbContext>(
    options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"), 
        b => b.MigrationsAssembly("GosAutoAPI")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}",
    defaults: new { controller = "Login", action = "Index" });


app.Run();
