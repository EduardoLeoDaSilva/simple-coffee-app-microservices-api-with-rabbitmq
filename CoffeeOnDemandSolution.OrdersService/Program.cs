using CoffeeOnDemandSolution.Common.Data.Repositories;
using CoffeeOnDemandSolution.Common.RabbitMQ;
using CoffeeOnDemandSolution.OrdersService.Background;
using CoffeeOnDemandSolution.OrdersService.Data;
using CoffeeOnDemandSolution.OrdersService.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ApplicationContext>(x => x.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddSingleton<Publisher>();
builder.Services.AddSingleton<Subscriber<Order>>();
builder.Services.AddScoped<BaseRepository<ApplicationContext, Order>>();
builder.Services.AddHostedService<MessageConsumer>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
