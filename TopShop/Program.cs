using Npgsql;
using System.Data;
using TopShop.Interfaces;
using TopShop.Middleware;
using TopShop.Repositories;
using TopShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ItemService>();
var connectionString = builder.Configuration.GetConnectionString("PostgreConnection");
builder.Services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(connectionString));

var app = builder.Build();


// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
