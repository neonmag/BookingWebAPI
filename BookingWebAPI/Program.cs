using BookingWebAPI.Data;
using BookingWebAPI.Repositories.Hall;
using BookingWebAPI.Repositories.IRepositories;
using BookingWebAPI.Repositories.Orders;
using BookingWebAPI.Services.CalculatingService;
using BookingWebAPI.Services.ValidationOrderService;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DB") // Connection string to database
                       ?? throw new InvalidOperationException("Connection String 'DB' not found.");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICalculatingService, CalculatingService>(); // Add services in builder
builder.Services.AddScoped<IValidateOrder, ValidateOrder>();

builder.Services.AddScoped<IHallRepository, HallRepository>(); // Add repositories in builder
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();
app.MapControllers();
app.Run();
