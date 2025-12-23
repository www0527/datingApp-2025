using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extensions;

#region 使用者擴充方法
/// <summary>
/// 提供將 <see cref="AppUser"/> 轉換為 <see cref="UserDto"/> 的擴充方法。
/// </summary>
#endregion
public static class AddUserExtensions
{
    #region AppUser 轉換為 UserDto
    /// <summary>
    /// 將 AppUser 實體轉換為 UserDto，並產生 JWT Token。
    /// </summary>
    /// <param name="user">要轉換的 AppUser 實體。</param>
    /// <param name="tokenService">用於產生 JWT Token 的服務。</param>
    /// <returns>轉換後的 UserDto 物件。</returns>
    #endregion
    public static UserDto ToDto(
        this AppUser user,
        ITokenService tokenService
    )
    {
        return new UserDto
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = tokenService.CreateToken(user)
        };
    }
}