using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

#region 註冊資料傳輸物件_DTO
/// <summary>
/// 用於接收前端註冊請求的資料傳輸物件（DTO）。
/// <para>參數：</para>
/// <list type="bullet">
///   <item><term>DisplayName</term><description>（string）使用者顯示名稱，必填</description></item>
///   <item><term>Email</term><description>（string）使用者電子郵件，必填且須符合 Email 格式</description></item>
///   <item><term>Password</term><description>（string）使用者密碼，必填且最少 4 個字元</description></item>
/// </list>
/// </summary>
#endregion
public class RegisterDto
{
    #region 使用者_顯示名稱
    /// <summary>
    /// 使用者顯示名稱（必填）。
    /// </summary>
    #endregion
    [Required]
    public string DisplayName { get; set; } = "";
    
    #region 使用者_電子郵件
    /// <summary>
    /// 使用者電子郵件（必填），必須符合 Email 格式。
    /// </summary>
    #endregion
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    #region 使用者_密碼
    /// <summary>
    /// 使用者密碼（必填），最少 4 個字元。
    /// </summary>
    #endregion
    [Required]
    [MinLength(4)]
    public string Password { get; set; } = "";
}