using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RideRequestService.RabbitMQ;
using RideRequestService.Repository;
using RideRequestService.SignalRHub;
using MediatR;
using System.Reflection;
using RideRequestService.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddSingleton<IRabbitMQConProvider, RabbitMQConProvider>();
builder.Services.AddScoped<IPassengerRequestRepository, PassengerRequestRepository>();

//postgres sql connection
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<RideRequestService.RabbitMQ.RabbitMQPublisher>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<PassengerRequestHub>("/passengerRequestHub");
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
