using System.Text;
using HouseFinances.Application;
using HouseFinances.Application.Interfaces;
using HouseFinances.Application.Users;
using HouseFinances.Domain.Entities;
using HouseFinances.Domain.Enums;
using HouseFinances.Domain.Repositories;
using HouseFinances.Infrastructure;
using HouseFinances.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<HouseFinances.Api.GlobalExceptionHandler>();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var maxRetries = 10;
    for (var attempt = 1; attempt <= maxRetries; attempt++)
    {
        try
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
            logger.LogInformation("Database migration applied successfully.");
            break;
        }
        catch (Exception ex)
        {
            if (attempt == maxRetries) throw;
            logger.LogWarning("Database not ready (attempt {Attempt}/{Max}): {Message}. Retrying in 3s...",
                attempt, maxRetries, ex.Message);
            Thread.Sleep(3000);
        }
    }

    // Seed default admin user if no users exist
    var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
    if (!await userRepo.AnyAsync())
    {
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        var admin = User.Create(
            "Admin",
            "admin@housefinances.com",
            passwordHasher.Hash("Admin@123"),
            UserRole.Admin);
        userRepo.Add(admin);
        await userRepo.SaveChangesAsync();
        logger.LogInformation("Default admin user created: admin@housefinances.com / Admin@123");
    }
}

app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
