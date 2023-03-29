
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
using Office.Interfaces.Generic;
using Office.Controllers.Generic;
using Office.Services.Generic;
using Dtos.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var connectionString = builder.Configuration.GetConnectionString("SCADAConnectionString");
//builder.Services.AddDbContext<SCADA>(options => options.UseSqlite(connectionString));
builder.Services.AddDbContext<Scada>(a => a.UseSqlServer(builder.Configuration.GetConnectionString("ScadaConnectionString")));
builder.Services.AddScoped<AdminSeeder>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<IPasswordHasher<Employee>, PasswordHasher<Employee>>();
builder.Services.AddScoped<IPasswordHasher<EmployeeDto>, PasswordHasher<EmployeeDto>>();

//Authentication
var authenticationSettings = new AuthenticationSettings();
DependencyInjection(builder);
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

var app = builder.Build();

SeedDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseHttpsRedirection();

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
    builder.Services.AddScoped<IEmployeeService, EmployeeService>();
    builder.Services.AddScoped<IGenericService<EmployeeDto>, EmployeeService>();
    builder.Services.AddScoped<IRoleService, RoleService>();
    builder.Services.AddScoped<IGenericService<RoleDto>, RoleService>();
    //builder.Services.AddScoped<IGenericService<RoleDto>>(x => x.GetRequiredService<IRoleService>());
    //builder.Services.AddScoped<IGenericService<EmployeeDto>>(a => a.GetRequiredService<IEmployeeService>());
}
