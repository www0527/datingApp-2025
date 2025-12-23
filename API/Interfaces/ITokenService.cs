using API.Entities;

namespace API.Interfaces;

#region JWT_令牌服務_介面
/// <summary>
/// 定義 JWT 令牌服務的介面規範，用於產生和管理使用者的身份驗證令牌。
/// </summary>
#endregion
public interface ITokenService
{
    #region 產生_JWT_令牌
    /// <summary>
    /// 根據指定的使用者資訊產生 JWT 身份驗證令牌。
    /// </summary>
    /// <param name="user">要產生令牌的應用程式使用者實體。</param>
    /// <returns>已簽署的 JWT 令牌字串，用於使用者身份驗證。</returns>
    #endregion
    string CreateToken(AppUser user);
}