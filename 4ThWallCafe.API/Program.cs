using System.Reflection;
using System.Text;
using _4ThWallCafe.API;
using _4ThWallCafe.API.JWT.Implementations;
using _4ThWallCafe.API.JWT.Interfaces;
using _4ThWallCafe.Application;
using _4ThWallCafe.Application.Services;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.Data;
using _4ThWallCafe.Data.Repositories;
using _4ThWallCafe.MVC.Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
var config = new AppConfiguration();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetAPIKey())),
            ValidateIssuer = true,
            ValidIssuer = config.GetAPIIssuer(),
            ValidateAudience = true,
            ValidAudience = config.GetAPIAudience(),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero

        };
        
        
    });
builder.Services.AddDbContext<IdentitySetupDBContext>(options =>
    options.UseSqlServer(config.GetConnectionString()));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentitySetupDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

});

builder.Services.AddSingleton(config); 
builder.Services.AddScoped<IJwtService, JwtTokenService>();
builder.Services.AddScoped<IServiceFactory, ServiceFactory>();

builder.Services.AddDbContext<FourthWallCafeContext>(options =>
    options.UseSqlServer(config.GetConnectionString()));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

//    await SeedAPIUser.SeedApiUserAsync(userManager);
//}
app.Run();
