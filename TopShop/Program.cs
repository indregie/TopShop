using Npgsql;
using System.Data;
using TopShop.Interfaces;
using TopShop.Middleware;
using TopShop.Repositories;
using TopShop.Services;
using TopShop.WebApi.Clients;
using TopShop.WebApi.Interfaces;
using TopShop.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<IJsonPlaceholderClient, JsonPlaceholderClient>();
builder.Services.AddScoped<UserService>();
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
