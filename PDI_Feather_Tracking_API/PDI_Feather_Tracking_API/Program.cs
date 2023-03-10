using Microsoft.EntityFrameworkCore;
using PDI_Feather_Tracking_API;
using PDI_Feather_Tracking_API.Models;
using PDI_Feather_Tracking_API.Services.ServicesImpl;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connection_string = builder.Configuration.GetConnectionString("PDIFeatherTracking");

//builder.Services.AddDbContext<PDIFeatherTrackingDbContext>(options => options.UseMySQL(connection_string));
builder.Services.AddDbContext<PDIFeatherTrackingDbContext>(options => options.UseMySQL(connection_string));

// Add services to the container.
#region Service
//builder.Services.AddSingleton<UserServiceImpl>();
//builder.Services.AddSingleton<OutboundServiceImpl>();
#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
