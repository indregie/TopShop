using Application.Services;
using Npgsql;
using System.Data;
using TopShop.Middleware;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Clients;
using Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<IJsonPlaceholderClient, JsonPlaceholderClient>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IShopRepository, ShopRepository>();
builder.Services.AddScoped<ShopService>();
var connectionString = builder.Configuration.GetConnectionString("PostgreConnection");
builder.Services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(connectionString));

builder.Services.AddHttpClient();

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
