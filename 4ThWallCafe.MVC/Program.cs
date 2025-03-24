using _4ThWallCafe.Application;
using _4ThWallCafe.Core.Interfaces.Application;
using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.Data;
using _4ThWallCafe.MVC;
using _4ThWallCafe.MVC.API.Implementations;
using _4ThWallCafe.MVC.API.Interfaces;
using _4ThWallCafe.MVC.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var config = new AppConfiguration();

builder.Services.AddDbContext<IdentitySetupDBContext>(options =>
options.UseSqlServer(config.GetConnectionString()));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentitySetupDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
var httpClient = new HttpClient
{
    BaseAddress = new Uri(config.GetBaseAddress())
};
builder.Services.AddDbContext<FourthWallCafeContext>(options =>
    options.UseSqlServer(config.GetConnectionString()));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(httpClient);
builder.Services.AddSingleton<IApplicationConfiguration, AppConfiguration>();
builder.Services.AddScoped<IServiceFactory, ServiceFactory>();
builder.Services.AddSingleton<IApiClientFactory, APIClientFactory>();
var app = builder.Build();

app.Use(async (context, next) =>
{
    if (!context.Request.Cookies.ContainsKey("CartId"))
    {
        var newGuid = Guid.NewGuid().ToString();
        context.Response.Cookies.Append("CartId", newGuid, new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });
    }
    await next.Invoke();
});

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Unhandled exception occurred.");
        throw; 
    }
});

app.UseExceptionHandler("/Home/Error");
   
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
