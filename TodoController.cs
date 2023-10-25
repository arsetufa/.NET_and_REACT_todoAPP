using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Models;

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
    public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
    {
        return _context.TodoItems.ToList();
    }


    [HttpGet("{id}")]
    public ActionResult<TodoItem> GetTodoItem(long id)
    {
        var todoItem = _context.TodoItems.Find(id);
        if(todoItem == null)
        {
            return NotFound();
        }
        return todoItem;
    }


    [HttpPost]
    public ActionResult<TodoItem> PostTodoItem(TodoItem item)
    {
        _context.TodoItems.Add(item);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public IActionResult PutTodoItem(long id, TodoItem item)
    {
        if(id != item.Id)
        {
            return BadRequest();
        }

        try
        {
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            // Log the exception message and return a bad request or internal server error
            return BadRequest(ex.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTodo(long id)
    {
        var todoItem = _context.TodoItems.Find(id);
        if(todoItem == null)
        {
            return NotFound();
        }

        _context.TodoItems.Remove(todoItem);
        _context.SaveChanges();

        return NoContent();
    }
}

