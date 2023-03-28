
using Microsoft.EntityFrameworkCore;
using Database;
using Database.Seeders;
using Database.Models;
using Microsoft.AspNetCore.Identity;
using Office.Interfaces;
using Office.Services;
using Office.Authentication;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//var connectionString = builder.Configuration.GetConnectionString("SCADAConnectionString");
//builder.Services.AddDbContext<SCADA>(options => options.UseSqlite(connectionString));
builder.Services.AddDbContext<SCADADbContext>(a => a.UseSqlServer(builder.Configuration.GetConnectionString("SCADAConnectionString")));
builder.Services.AddScoped<AdminSeeder>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddCors(ops =>
{
    ops.AddDefaultPolicy(build =>
    {
        build.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//Authentication
var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});

///Dependency injection
DependencyInjection(builder);

var app = builder.Build();

SeedDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<AdminSeeder>();
        seeder.AdminSeed();
    }
}

///Dependency injection
void DependencyInjection(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddSingleton(authenticationSettings);
}
