using System;

namespace API.DTOs;

#region 使用者資料傳輸物件_DTO
/// <summary>
/// 回傳給前端的使用者資料傳輸物件，包含使用者基本資訊與身份驗證令牌。
/// <para>參數：</para>
/// <list type="bullet">
///   <item><term>Id</term><description>（string）使用者唯一識別碼</description></item>
///   <item><term>DisplayName</term><description>（string）使用者顯示名稱</description></item>
///   <item><term>Email</term><description>（string）使用者電子郵件</description></item>
///   <item><term>ImageUrl</term><description>（string?）使用者頭像網址（可為 null）</description></item>
///   <item><term>Token</term><description>（string）JWT 驗證權杖</description></item>
/// </list>
/// </summary>
#endregion
public class UserDto
{
    #region 使用者_唯一識別碼
    /// <summary>
    /// 使用者唯一識別碼。
    /// </summary>
    #endregion
    public required string Id { get; set; }

    #region 使用者_顯示名稱
    /// <summary>
    /// 使用者顯示名稱。
    /// </summary>
    #endregion
    public required string DisplayName { get; set; }

    #region 使用者_電子郵件
    /// <summary>
    /// 使用者電子郵件地址。
    /// </summary>
    #endregion
    public required string Email { get; set; }

    #region 使用者_頭像網址
    /// <summary>
    /// 使用者頭像圖片網址（選填）。
    /// </summary>
    #endregion
    public string? ImageUrl { get; set; }

    #region JWT_驗證令牌
    /// <summary>
    /// JWT 身份驗證令牌，用於 API 請求授權。
    /// </summary>
    #endregion
    public required string Token { get; set; }
}