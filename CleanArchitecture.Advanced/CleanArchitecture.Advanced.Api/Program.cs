using FluentValidation;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Advanced.Api.Application.Interfaces.Caching;
using CleanArchitecture.Advanced.Api.Application.Interfaces.Repositories;
using CleanArchitecture.Advanced.Api.Application.Interfaces.Services;
using CleanArchitecture.Advanced.Api.Application.Mappings;
using CleanArchitecture.Advanced.Api.Application.Services;
using CleanArchitecture.Advanced.Api.Application.Validation;
using CleanArchitecture.Advanced.Api.Domain.Entities;
using CleanArchitecture.Advanced.Api.Infrastructure.Caching;
using CleanArchitecture.Advanced.Api.Infrastructure.Data;
using CleanArchitecture.Advanced.Api.Infrastructure.Middleware;
using CleanArchitecture.Advanced.Api.Infrastructure.Repositories;
using CleanArchitecture.Advanced.Api.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Register repositories
builder.Services.AddScoped<ILibraryRepository, LibraryRepository>();
builder.Services.AddScoped<IEventLogRepository, EventLogRepository>();

// Register validators
builder.Services.AddScoped<IValidator<Library>, LibraryValidator>();

// Add Cache
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICustomMemoryCache, CustomMemoryCache>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(LibraryProfile));

// Add Entity Framework Core and configure the SQL Server connection string
// Use Add-Migration InitialCreate and then Update-Database to let EF generate the DB
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Ensure the database is seeded
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<LibraryContext>();

    try
    {
        DbInitializer.Seed(context); // Fill the database with sample data
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();

// Register middleware
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
