using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;


// JWT 令牌服務實現
public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        // 獲取密鑰
        // 1. 從 appsettings.json or appsettings.Development.json 配置中讀取 TokenKey
        // 2. 如果無法獲取，則拋出異常
        var token = config["TokenKey"] ?? throw new Exception("無法獲取密鑰");

        // 確保密鑰長度足夠
        if (token.Length < 64) 
            throw new Exception("TokenKey 必須至少包含 64 個字元");
        
        // 建立對稱安全密鑰
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token));

        // 取得用戶聲明並傳回資料
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id)
        };

        // 設定加密方式
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //  * 設定令牌內容
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };

        // 產生令牌
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
}
