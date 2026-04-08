using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Application.Services;
using QuanLy.Domain.Interface;
using QuanLy.Infrastructure.Context;
using QuanLy.Infrastructure.Repositories;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DASContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("QuanLyConnection"));
});

#region Đăng ký Repository
builder.Services.AddScoped<IQuanLyRepositoryWrapper, QuanLyRepositoryWrapper>();
#endregion


#region Đăng ký Service
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IAuth_UsersService, Auth_UsersService>();
#endregion

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddLogging();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuanLy API", Version = "v1" });
});

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// JWT
var secretKey = builder.Configuration["AppSettings:SecretKey"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey ?? "");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "QuanLy API v1");
});

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();