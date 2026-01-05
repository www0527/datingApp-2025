using System;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

/// <summary>
/// 會員資料存取的 Repository 實作，負責與資料庫互動。
/// </summary>
public class MemberRepository(AppDbContext context) : IMemberRepository
{
    #region 取得_單一會員_ById_GetMemberByIdAsync
    /// <summary>
    /// 依會員 ID 非同步取得單一會員資料。
    /// </summary>
    /// <param name="id">會員的唯一識別碼。</param>
    /// <returns>會員實體或 null。</returns>
    #endregion
    public async Task<Member?> GetMemberByIdAsync(string id)
    {
        // FindAsync: 根據主鍵值查找實體
        return await context.Members.FindAsync(id);
    }

    #region 取得_所有會員_GetMembersAsync
    /// <summary>
    /// 非同步取得所有會員的清單。
    /// </summary>
    /// <returns>會員清單（唯讀）。</returns>
    #endregion
    public async Task<IReadOnlyList<Member>> GetMembersAsync()
    {
        // ToListAsync: 非同步將查詢結果轉換為清單
        return await context.Members.ToListAsync();
    }

    #region 取得_會員照片列表_GetPhotosForMemberIdAsync
    /// <summary>
    /// 非同步取得指定會員的所有照片。
    /// </summary>
    /// <param name="memberId">會員的唯一識別碼。</param>
    /// <returns>照片清單（唯讀）。</returns>
    #endregion
    public async Task<IReadOnlyList<Photo>> GetPhotosForMemberIdAsync(string memberId)
    {
        return await context.Members
            .Where(x => x.Id == memberId)   // 篩選指定會員
            .SelectMany(x => x.Photos)      // 展開會員的照片集合
            .ToListAsync();                 // 非同步轉換為清單
    }

    #region 儲存_所有變更_SaveAllAsync
    /// <summary>
    /// 非同步儲存所有變更至資料庫。
    /// </summary>
    /// <returns>回傳是否成功儲存（true/false）。</returns>
    #endregion
    public async Task<bool> SaveAllAsync()
    {
        // SaveChangesAsync: 非同步儲存變更，回傳受影響的列數
        return await context.SaveChangesAsync() > 0;
    }

    #region 更新_會員資料_Update
    /// <summary>
    /// 標記會員資料為已修改，待儲存至資料庫。
    /// </summary>
    /// <param name="member">要更新的會員實體。</param>
    #endregion
    public void Update(Member member)
    {
        context.Entry(member).State = EntityState.Modified;
    }
}
