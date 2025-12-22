using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]  // 必填欄位
    public string DisplayName { get; set; } = "";
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [MinLength(4)]  // 密碼最小長度為 4 個字元
    public string Password { get; set; } = "";
}
