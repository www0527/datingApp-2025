using API.Data;
using API.Interfaces;
using API.Middleware;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// // 啟用 Swagger XML 註解支援
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(options =>
// {
//     options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//     {
//         Title = "Dating App API",
//         Version = "v1",
//         Description = "Dating App 後端 API 文件"
//     });

//     // 讀取 XML 註解檔案
//     var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
//     var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//     if (File.Exists(xmlPath))
//     {
//         options.IncludeXmlComments(xmlPath);
//     }
// });

/// <summary>
/// 配置 DbContext，決定系統使用的資料庫。
/// </summary>
builder.Services.AddDbContext<AddDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

/// <summary>
/// 配置 CORS 服務。
/// </summary>
builder.Services.AddCors();

/// <summary>
/// JWT 服務註冊，使用 AddScoped 方法註冊服務，生命週期為單個 HTTP 請求。
/// </summary>
builder.Services.AddScoped<ITokenService, TokenService>();

/// <summary>
/// 建立 JWT 驗證服務。
/// </summary>
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
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

// // 啟用 Swagger（開發環境）
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(options =>
//     {
//         options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dating App API v1");
//     });
// }

app.UseMiddleware<ExceptionMiddleware>();

/// <summary>
/// 使用 CORS 中介軟體，並設定允許的來源、標頭和方法。
/// </summary>
app.UseCors(x =>
    x.AllowAnyHeader()
     .AllowAnyMethod()
     .WithOrigins("http://localhost:4200", "https://localhost:4200")
);

/// <summary>
/// 使用驗證中介軟體，順序不可顛倒。
/// </summary>
app.UseAuthentication();  // 驗證使用者身份
app.UseAuthorization();   // 授權使用者存取資源

app.MapControllers();

app.Run();