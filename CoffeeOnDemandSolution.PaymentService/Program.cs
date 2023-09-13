using CoffeeOnDemandSolution.Common.RabbitMQ;
using CoffeeOnDemandSolution.PaymentService.Background;
using CoffeeOnDemandSolution.PaymentService.Models;
using CoffeeOnDemandSolution.PaymentService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<Publisher>();

var connectionString = builder.Configuration["RabbitMQ:Hostname"];
Console.WriteLine(connectionString);

builder.Services.AddSingleton<Subscriber<OrderPayment>>();
builder.Services.AddHostedService<MessageConsumer>();
builder.Services.AddScoped<IPaymentService,PaymentService>();

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
