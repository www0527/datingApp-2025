using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

// Data：負責與資料庫進行互動，管理資料實體的存取和操作。
// * 定義資料庫上下文類別 AddDbContext，繼承自 Entity Framework Core 的 DbContext，並包含一個 DbSet 屬性 Users，用於操作 AppUser 實體。
public class AddDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
}
