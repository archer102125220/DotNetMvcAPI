namespace DotNetMvcAPI.Models;

/// <summary>
/// 使用者模型 (User Model) - 用於示範 API 資料結構
/// </summary>
public class User
{
    /// <summary>
    /// 使用者唯一識別碼 (ID)
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 使用者名稱 (Name)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 使用者電子郵件 (Email)
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 使用者年齡 (Age)
    /// </summary>
    public int Age { get; set; }
}
