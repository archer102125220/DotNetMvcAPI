using Microsoft.AspNetCore.Mvc;
using DotNetMvcAPI.Models;

namespace DotNetMvcAPI.Controllers;

/// <summary>
/// 使用者控制器 (Users Controller) - 示範基本的 CRUD 操作
/// RESTful API 設計標準：
/// GET    /api/users       - 取得所有使用者
/// GET    /api/users/{id}  - 取得特定使用者
/// POST   /api/users       - 新增使用者
/// PUT    /api/users/{id}  - 更新特定使用者的完整資料
/// PATCH  /api/users/{id}  - 更新特定使用者的部分資料
/// DELETE /api/users/{id}  - 刪除特定使用者
/// </summary>
[ApiController] // 標記這是一個 API 控制器，會自動處理模型驗證 (Model Validation) 與推斷綁定來源 (Binding Source)
[Route("api/[controller]")] // 路由設定，[controller] 會自動替換為控制器名稱 (這裡是 users)
public class UsersController : ControllerBase
{
    // 模擬的資料庫 (靜態變數，讓資料在記憶體中保留)
    private static readonly List<User> _users = new()
    {
        new User { Id = 1, Name = "Alice", Email = "alice@example.com", Age = 25 },
        new User { Id = 2, Name = "Bob", Email = "bob@example.com", Age = 30 }
    };

    /// <summary>
    /// 取得所有使用者列表 (GET: api/users)
    /// </summary>
    /// <returns>使用者列表</returns>
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        // 回傳 200 OK 與所有資料
        return Ok(_users);
    }

    /// <summary>
    /// 取得特定使用者資料 (GET: api/users/{id})
    /// </summary>
    /// <param name="id">使用者 ID</param>
    /// <returns>找到的使用者資料，或回傳找不到 (404)</returns>
    [HttpGet("{id}")]
    public ActionResult<User> GetById(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            // 如果找不到，回傳 404 Not Found
            return NotFound(new { message = $"找不到 ID 為 {id} 的使用者" });
        }
        
        // 找到的話回傳 200 OK 與資料
        return Ok(user);
    }

    /// <summary>
    /// 新增一位使用者 (POST: api/users)
    /// </summary>
    /// <param name="newUser">要新增的使用者資料</param>
    /// <returns>新增成功的使用者與建立位置 (201)</returns>
    [HttpPost]
    public ActionResult<User> Create([FromBody] User newUser)
    {
        // 模擬產生新的 ID
        newUser.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
        
        _users.Add(newUser);

        // 回傳 201 Created，並且在 Response Header 中附上取得該資源的 Location
        // nameof(GetById) 會對應到上方的 GetById 方法
        return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
    }

    /// <summary>
    /// 更新特定使用者的完整資料 (PUT: api/users/{id})
    /// PUT 通常用於「完整替換」資源
    /// </summary>
    /// <param name="id">要更新的使用者 ID</param>
    /// <param name="updatedUser">更新後的使用者完整資料</param>
    /// <returns>更新成功 (204 No Content)</returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] User updatedUser)
    {
        var index = _users.FindIndex(u => u.Id == id);
        if (index == -1)
        {
            return NotFound(new { message = $"找不到 ID 為 {id} 的使用者" });
        }

        // 強制確保 ID 不被竄改
        updatedUser.Id = id;
        
        // 完整替換資料
        _users[index] = updatedUser;

        // 回傳 204 No Content 代表處理成功且沒有要回傳特別的資料
        return NoContent(); 
    }

    /// <summary>
    /// 更新特定使用者的部分資料 (PATCH: api/users/{id})
    /// PATCH 通常用於「部分更新」資源，這裡示範簡單的部分屬性更新
    /// </summary>
    /// <param name="id">要更新的使用者 ID</param>
    /// <param name="updates">要更新的部分資料 (這裡簡化使用字典來接收)</param>
    /// <returns>更新成功 (204 No Content)</returns>
    [HttpPatch("{id}")]
    public IActionResult PartialUpdate(int id, [FromBody] Dictionary<string, object> updates)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(new { message = $"找不到 ID 為 {id} 的使用者" });
        }

        // 簡單示範：走訪要更新的欄位並進行修改
        foreach (var (key, value) in updates)
        {
            // 不允許更新 ID
            if (key.Equals("Id", StringComparison.OrdinalIgnoreCase)) continue;

            if (key.Equals("Name", StringComparison.OrdinalIgnoreCase))
                user.Name = value?.ToString() ?? user.Name;
            
            if (key.Equals("Email", StringComparison.OrdinalIgnoreCase))
                user.Email = value?.ToString() ?? user.Email;
            
            if (key.Equals("Age", StringComparison.OrdinalIgnoreCase) && int.TryParse(value?.ToString(), out int age))
                user.Age = age;
        }

        return NoContent();
    }

    /// <summary>
    /// 刪除特定使用者 (DELETE: api/users/{id})
    /// </summary>
    /// <param name="id">要刪除的使用者 ID</param>
    /// <returns>刪除成功 (204 No Content)</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(new { message = $"找不到 ID 為 {id} 的使用者" });
        }

        _users.Remove(user);

        return NoContent();
    }
}
