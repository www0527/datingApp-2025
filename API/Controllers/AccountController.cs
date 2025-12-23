using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


#region 帳戶控制器
/// <summary>
/// 處理與帳戶相關的 API 請求，例如註冊和登入。
/// </summary>
#endregion
public class AccountController(AddDbContext context, ITokenService tokenService) : BaseAPIController
{

    #region 註冊新使用者
    /// <summary>
    /// 註冊新使用者。
    /// </summary>
    /// <param name="registerDto">註冊所需的使用者資料。</param>
    /// <returns>註冊成功時回傳 UserDto，否則回傳錯誤訊息。</returns>
    #endregion
    [HttpPost("register")] // POST: api/account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await EmailExists(registerDto.Email)) return BadRequest("該電子郵件已被使用");

        // 雜湊加密
        using var hmac = new HMACSHA512();   // using  語句確保 HMACSHA512 實例在使用後被正確釋放

        // 創建新的使用者 
        var user = new AppUser
        {
            Email = registerDto.Email,
            DisplayName = registerDto.DisplayName,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user.ToDto(tokenService);
    }

    #region 使用者登入
    /// <summary>
    /// 使用者登入。
    /// </summary>
    /// <param name="loginDto">登入所需的使用者資料。</param>
    /// <returns>登入成功時回傳 UserDto，否則回傳錯誤訊息。</returns>
    #endregion
    [HttpPost("login")] // POST: api/account/login
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await context.Users.SingleOrDefaultAsync(uer => uer.Email == loginDto.Email);

        // 不存 Email 在返回 401 未授權
        if (user == null) return Unauthorized("無效的電子郵件");

        // 驗證密碼
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("無效的密碼");
        }

        return user.ToDto(tokenService);
    }


    #region 檢查電子郵件是否存在
    /// <summary>
    /// 檢查指定的電子郵件是否已存在於資料庫中。
    /// </summary>
    /// <param name="email">要檢查的電子郵件。</param>
    /// <returns>如果已存在則回傳 true，否則回傳 false。</returns>
    #endregion
    private async Task<bool> EmailExists(string email)
    {
        return await context.Users.AnyAsync(
            user => user.Email.ToLower() == email.ToLower()
        );
    }
}
