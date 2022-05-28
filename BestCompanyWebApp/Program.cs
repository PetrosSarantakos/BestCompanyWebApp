using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BestCompanyWebApp.Data;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BestCompanyWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BestCompanyWebAppContext") ?? throw new InvalidOperationException("Connection string 'BestCompanyWebAppContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

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
    pattern: "{controller=ActionModels}/{action=Index}/{id?}");

app.Run();
