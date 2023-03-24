using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PDI_Feather_Tracking_API;
using PDI_Feather_Tracking_API.Models;
using PDI_Feather_Tracking_API.Services;
using System.Configuration;
using System.Text;

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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "PDI Feather Tracking API", Version = "v1.0.0" }));

//Entity Framework Connection
var connectionStr = builder.Configuration.GetConnectionString("PDIFeatherTracking");
builder.Services.AddDbContext<PDIFeatherTrackingDbContext>(options => options.UseMySQL(connectionStr));
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = General.EncryptionKey,
        ValidAudience = General.EncryptionKey,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(General.TokenKey)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        //ValidateLifetime = true,
        //RequireExpirationTime = true,
    };
    o.SaveToken = true;
});
#region Service
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OutboundService>();
#endregion
builder.Services.AddControllers();
builder.Services.AddAuthorization();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
