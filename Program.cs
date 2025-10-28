//using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tienda.src.Domain.Models;
using Tienda.src.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la base de datos SQLite
//builder.Services.AddDbContext<DataContext>(options =>
    //options.UseSqlite(builder.Configuration.GetConnectionString("SqliteDatabase")));

// Configuración de Identity
//builder.Services.AddIdentityCore<User>(options =>
//{
//    options.Password.RequireDigit = true;
//    options.Password.RequiredLength = 8;
//    options.Password.RequireNonAlphanumeric = false;
//    options.User.RequireUniqueEmail = true;
//})
//.AddRoles<Role>()
//.AddEntityFrameworkStores<DataContext>()
//.AddDefaultTokenProviders();

// Configuración de autenticación JWT
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWTSecret"])),
//            ValidateLifetime = true,
//            ValidateIssuer = false,
//            ValidateAudience = false
//        };
//    });

// Configuración de Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Configuración de Hangfire
//builder.Services.AddHangfire(config => config.UseSQLiteStorage(builder.Configuration.GetConnectionString("SqliteDatabase")));
//builder.Services.AddHangfireServer();

var app = builder.Build();

// Uso de Hangfire Dashboard
//app.UseHangfireDashboard();

// Middleware
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAllOrigins");
app.MapControllers();

app.Run();
