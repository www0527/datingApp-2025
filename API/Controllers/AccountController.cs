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


// 用途：處理與帳戶相關的 API 請求，例如註冊和登入。
public class AccountController(AddDbContext context, ITokenService tokenService) : BaseAPIController
{

    // 註冊
    [HttpPost("register")] // POST: api/account/register
    public async Task<ActionResult<UserDto>> Register( RegisterDto registerDto)
    {
        // 檢查 Email 是否存在
        if (await EmailExists(registerDto.Email)) return BadRequest("該電子郵件已被使用");

        // 創建新的使用者 
        using var hmac = new HMACSHA512();   // using  語句確保 HMACSHA512 實例在使用後被正確釋放
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

    // 登入
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


    // 檢查 Email 邏輯
    private async Task<bool> EmailExists(string email)
    {
        return await context.Users.AnyAsync(
            user => user.Email.ToLower() == email.ToLower()
        );
    }
}
