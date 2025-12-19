namespace API.Entities;

// Entities：定義應用程式中的資料實體之數據結構和屬性。
public class AppUser
{
// * 定義應用程式使用者的實體類別 AppUser，包含 Id、DisplayName 和 Email 屬性。
// * required : 屬性在物件初始化時必須被賦值。
// * Guid.NewGuid().ToString() : 為 Id 屬性生成一個唯一的識別碼。

    public required string Id { get; set; } = Guid.NewGuid().ToString();
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
}
