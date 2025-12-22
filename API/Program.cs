using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
builder.Services.AddCors();    // * 配置 CORS 服務


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x =>        // * 使用 CORS 中介軟體，並設定允許的來源、標頭和方法
    x.AllowAnyHeader()
     .AllowAnyMethod()
     .AllowAnyOrigin()
     .WithOrigins("http://localhost:4200", "https://localhost:4200")
);
app.MapControllers();

app.Run();
