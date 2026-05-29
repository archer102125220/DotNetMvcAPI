using Microsoft.AspNetCore.Mvc;
using DotNetMvcAPI.Models;

namespace DotNetMvcAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    // 建立一個在記憶體中的 List 當作暫時的資料庫（靜態變數以便在不同請求間保留資料）
    private static readonly List<Todo> _sampleTodos = new()
    {
        new Todo { Id = 1, Title = "Walk the dog", IsComplete = false },
        new Todo { Id = 2, Title = "Do the dishes", DueBy = DateOnly.FromDateTime(DateTime.Now), IsComplete = false },
        new Todo { Id = 3, Title = "Do the laundry", DueBy = DateOnly.FromDateTime(DateTime.Now.AddDays(1)), IsComplete = false },
        new Todo { Id = 4, Title = "Clean the bathroom", IsComplete = false },
        new Todo { Id = 5, Title = "Clean the car", DueBy = DateOnly.FromDateTime(DateTime.Now.AddDays(2)), IsComplete = false }
    };

    /// <summary>
    /// 1. GET: 取得所有 Todo
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Todo>> GetTodos()
    {
        return Ok(_sampleTodos);
    }

    /// <summary>
    /// 2. GET: 根據 ID 取得單一 Todo
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<Todo> GetTodoById(int id)
    {
        var todo = _sampleTodos.FirstOrDefault(t => t.Id == id);
        
        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    /// <summary>
    /// 3. POST: 建立新的 Todo
    /// </summary>
    [HttpPost]
    public ActionResult<Todo> CreateTodo(Todo todo)
    {
        // 自動產生新的 ID
        var newId = _sampleTodos.Count > 0 ? _sampleTodos.Max(t => t.Id) + 1 : 1;
        todo.Id = newId;

        _sampleTodos.Add(todo);

        // 回傳 201 Created，並附上新資源的位置與內容
        return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
    }

    /// <summary>
    /// 4. PUT: 完整更新特定 ID 的 Todo
    /// </summary>
    [HttpPut("{id}")]
    public IActionResult UpdateTodo(int id, Todo updatedTodo)
    {
        var index = _sampleTodos.FindIndex(t => t.Id == id);
        
        if (index == -1)
        {
            return NotFound();
        }

        // 強制把 ID 設為原本的 ID，避免被覆蓋
        updatedTodo.Id = id;
        _sampleTodos[index] = updatedTodo;

        return NoContent();
    }

    /// <summary>
    /// 5. PATCH: 部分更新特定 ID 的 Todo
    /// </summary>
    [HttpPatch("{id}")]
    public IActionResult PatchTodo(int id, TodoPatch patchTodo)
    {
        var index = _sampleTodos.FindIndex(t => t.Id == id);
        
        if (index == -1)
        {
            return NotFound();
        }

        var existing = _sampleTodos[index];

        // 如果 patchTodo 中有值，就更新；否則保留舊值
        if (patchTodo.Title != null) existing.Title = patchTodo.Title;
        if (patchTodo.DueBy != null) existing.DueBy = patchTodo.DueBy;
        if (patchTodo.IsComplete.HasValue) existing.IsComplete = patchTodo.IsComplete.Value;

        return NoContent();
    }

    /// <summary>
    /// 6. DELETE: 刪除特定 ID 的 Todo
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult DeleteTodo(int id)
    {
        var removedCount = _sampleTodos.RemoveAll(t => t.Id == id);
        
        if (removedCount == 0)
        {
            return NotFound();
        }

        return NoContent();
    }
}
