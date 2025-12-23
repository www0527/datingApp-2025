using System;

namespace API.Entities;

#region 應用程式使用者實體_AppUser
/// <summary>
/// 表示應用程式使用者的資料實體（Entity），供資料庫儲存與身份驗證使用。
/// <para>參數：</para>
/// <list type="bullet">
///   <item><term>Id</term><description>（string）使用者唯一識別碼，預設以 Guid 生成</description></item>
///   <item><term>DisplayName</term><description>（string）使用者顯示名稱</description></item>
///   <item><term>Email</term><description>（string）使用者電子郵件，用於登入與識別</description></item>
///   <item><term>PasswordHash</term><description>（byte[]）密碼雜湊值，用於驗證密碼</description></item>
///   <item><term>PasswordSalt</term><description>（byte[]）雜湊鹽值，用以加強密碼雜湊的安全性</description></item>
/// </list>
/// </summary>
#endregion
public class AppUser
{
    #region 使用者_唯一識別碼
    /// <summary>
    /// 使用者唯一識別碼（預設使用 <see cref="Guid.NewGuid"/> 生成的字串）。
    /// </summary>
    #endregion
    public string Id { get; set; } = Guid.NewGuid().ToString();

    #region 使用者_顯示名稱
    /// <summary>
    /// 使用者顯示名稱，建立物件時應提供。
    /// </summary>
    #endregion
    public required string DisplayName { get; set; }

    #region 使用者_電子郵件
    /// <summary>
    /// 使用者電子郵件，建立物件時應提供，常用於登入與識別。
    /// </summary>
    #endregion
    public required string Email { get; set; }

    #region 密碼_雜湊值
    /// <summary>
    /// 儲存密碼的雜湊值（byte[]），與 <see cref="PasswordSalt"/> 一起用於驗證密碼。
    /// </summary>
    #endregion
    public required byte[] PasswordHash { get; set; }

    #region 密碼_雜湊鹽值
    /// <summary>
    /// 密碼雜湊使用的鹽值（byte[]），用以加強雜湊安全性。
    /// </summary>
    #endregion
    public required byte[] PasswordSalt { get; set; }
}