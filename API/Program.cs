using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// * 配置 DbContext，決定系統使用的資料庫
builder.Services.AddDbContext<AddDbContext>(
    options =>
    {
        options.UseSqlite(
            builder.Configuration.GetConnectionString("DefaultConnection")
        );
    }
);
// * 配置 CORS 服務
builder.Services.AddCors();      

// JWT 服務註冊，使用 AddScoped<{Interface}, {Service}> 方法註冊服務, 
builder.Services.AddScoped<ITokenService, TokenService>();  // 生命週期為單個 HTTP 請求

// 建立驗證服務
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer( option =>
{
    var tokenKey = builder.Configuration["TokenKey"] 
        ?? throw new Exception("Token key not found");
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(tokenKey)
        ),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x =>        // * 使用 CORS 中介軟體，並設定允許的來源、標頭和方法
    x.AllowAnyHeader()
     .AllowAnyMethod()
     .AllowAnyOrigin()
     .WithOrigins("http://localhost:4200", "https://localhost:4200")
);

 // * 使用驗證中介軟體，順序不可顛倒
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
