using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PDI_Feather_Tracking_API;
using PDI_Feather_Tracking_API.Models;
using PDI_Feather_Tracking_API.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddMvc(options =>
{
    options.ReturnHttpNotAcceptable = true;
    options.EnableEndpointRouting = false;
}).AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "PDI Feather Tracking API", Version = "v1.0.0" }));

//Entity Framework Connection
var connectionStr = builder.Configuration.GetConnectionString("PDIFeatherTracking");
builder.Services.AddDbContext<PDIFeatherTrackingDbContext>(options => options.UseMySQL(connectionStr));

#region Service
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OutboundService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("./swagger/v1/swagger.json", "PDI Feather Tracking API V1");
    c.DocumentTitle = "PDI Feather Tracking API";
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
