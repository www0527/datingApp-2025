using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extensions;

// 用途：將 AppUser 實體擴展為 UserDto
public static class AddUserExtensions
{
    public static UserDto ToDto(
        this AppUser user,          // * this 關鍵字表示這是一個擴展方法，擴展 AppUser 類型
        ITokenService tokenService
    )
    {
        return new UserDto
        {
            Id= user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = tokenService.CreateToken(user) // You should generate and assign a token here
        };
    }
}
