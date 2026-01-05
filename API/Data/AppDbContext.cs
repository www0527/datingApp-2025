using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

#region 資料庫上下文_AddDbContext
/// <summary>
/// Entity Framework Core 的資料庫上下文，負責管理 AppUser 實體的存取與變更追蹤。
/// <para>透過建構式注入 <see cref="DbContextOptions"/> 以支援在 Startup/Program 中設定資料庫供應者與連線字串。</para>
/// </summary>
/// <param name="options">DbContext 配置選項，由 DI 提供，包含供應者與連線字串等設定。</param>
#endregion
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    #region 使用者集合_Users
    /// <summary>
    /// AppUser 實體集合的 DbSet，代表資料庫中的使用者表格。
    /// <para>透過此屬性執行 CRUD 操作與 LINQ 查詢。</para>
    /// </summary>
    #endregion
    public DbSet<AppUser> Users { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Photo> Photos { get; set; }
}
