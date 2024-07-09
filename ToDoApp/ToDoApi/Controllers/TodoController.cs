using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoContext _context;

    public TodoController(TodoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Todo>> GetTodos()
    {
        var todos = _context.MK_Todos.ToList();
        return Ok(todos);
    }

    [HttpPost]
    public ActionResult<Todo> PostTodo(Todo todo)
    {
        _context.MK_Todos.Add(todo);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetTodos), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public IActionResult PutTodo(int id, Todo todo)
    {
        if (id != todo.Id)
            return BadRequest();

        _context.Entry(todo).State = EntityState.Modified;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTodo(int id)
    {
        var todo = _context.MK_Todos.Find(id);
        if (todo == null)
            return NotFound();

        _context.MK_Todos.Remove(todo);
        _context.SaveChanges();
        return NoContent();
    }
}
