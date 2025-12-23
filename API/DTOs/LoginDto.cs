using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

#region 登入資料傳輸物件_DTO
/// <summary>
/// 用於接收前端登入請求的資料傳輸物件（DTO）。
/// <para>參數：</para>
/// <list type="bullet">
///   <item><term>Email</term><description>（string）使用者的電子郵件，用於識別帳號</description></item>
///   <item><term>Password</term><description>（string）使用者的密碼，用於驗證身份</description></item>
/// </list>
/// </summary>
#endregion
public class LoginDto
{
    #region 使用者_電子郵件
    /// <summary>
    /// 使用者的電子郵件（必填，用於登入識別）。
    /// </summary>
    #endregion
    public string Email { get; set; } = "";

    #region 使用者_密碼
    /// <summary>
    /// 使用者的密碼（必填，用於身份驗證）。
    /// </summary>
    #endregion
    public string Password { get; set; } = "";
}