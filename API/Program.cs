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


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

app.Run();
