using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure;
using UserService.Services;

var builder = WebApplication.CreateBuilder(args);

var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "UniSpace Corp.";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "Clients";
var jwtKey = builder.Configuration["Jwt:Key"] ??
             throw new InvalidOperationException("JWT Key не задан в appsettings.json!");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)), 
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserDb>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUserRegister, UserRegisterService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();



app.MapGet("/data", [Authorize] () => new { message = "Hello World!" });

app.Run();