using System;
using API.Entities;

namespace API.Interfaces;

#region IMemberRepository_介面
/// <summary>
/// 會員資料存取的 Repository 介面。
/// </summary>
#endregion
public interface IMemberRepository
{
    #region 更新會員_Update
    /// <summary>
    /// 更新指定的會員資料。
    /// </summary>
    /// <param name="member">要更新的會員實體。</param>
    #endregion
    void Update(Member member);

    #region 儲存變更_SaveAllAsync
    /// <summary>
    /// 儲存所有變更至資料庫。
    /// </summary>
    /// <returns>回傳是否成功儲存（true/false）。</returns>
    #endregion
    Task<bool> SaveAllAsync();

    #region 取得所有會員_GetMembersAsync
    /// <summary>
    /// 取得所有會員的清單。
    /// </summary>
    /// <returns>會員清單（唯讀）。</returns>
    #endregion
    Task<IReadOnlyList<Member>> GetMembersAsync();

    #region 取得會員_ById_GetMemberByIdAsync
    /// <summary>
    /// 依會員 ID 取得單一會員資料。
    /// </summary>
    /// <param name="id">會員的唯一識別碼。</param>
    /// <returns>會員實體或 null。</returns>
    #endregion
    Task<Member?> GetMemberByIdAsync(string id);

    #region 取得會員照片_GetPhotosForMemberIdAsync
    /// <summary>
    /// 取得指定會員的所有照片。
    /// </summary>
    /// <param name="memberId">會員的唯一識別碼。</param>
    /// <returns>照片清單（唯讀）。</returns>
    #endregion
    Task<IReadOnlyList<Photo>> GetPhotosForMemberIdAsync(string memberId);
}
