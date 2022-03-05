using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;
        // private readonly InMemoryListManager _inMemLists;

        public TodoItemsController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            // _inMemLists = inMemLists;
        }

        // GET: api/TodoItems
        // [HttpGet]InMemoryListManager
        //     HttpContext.Session.SetString("_test", "test");
        //     return await _dbContext.TodoItems
        //         .Where(x => x.SessionId == HttpContext.Session.Id)
        //         .Select(x => ItemToDTO(x))
        //         .ToListAsync();
        // }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            SetPlaceholderCookie();
            return await _dbContext.TodoItems
                .Where(x => x.SessionId == HttpContext.Session.Id)
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            SetPlaceholderCookie();
            var todoItem = await _dbContext.TodoItems
                .Where(x => x.SessionId == HttpContext.Session.Id)
                .FirstAsync(x => x.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            SetPlaceholderCookie();
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            // _context.Entry(todoItem).State = EntityState.Modified;
            var todoItem = await _dbContext.TodoItems
                .Where(x => x.SessionId == HttpContext.Session.Id)
                .FirstAsync(x => x.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }
        
            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoItemDTO)
        {
            SetPlaceholderCookie();
            var todoItem = new TodoItem
            {
                SessionId = HttpContext.Session.Id,
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _dbContext.TodoItems.Add(todoItem);
            await _dbContext.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, ItemToDTO(todoItem));
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            SetPlaceholderCookie();
            var todoItem = await _dbContext.TodoItems
                .Where(x => x.SessionId == HttpContext.Session.Id)
                .FirstAsync(x => x.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _dbContext.TodoItems.Remove(todoItem);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _dbContext.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };

        private void SetPlaceholderCookie()
        {
            // Sessions require a value to be set in order to be persisted across requests. 
            // Identifying user data via session id's in this way is not a typical use case but for testing purposes, we will set a placeholder cookie to do so.
            // Docs on persisting sessions: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-6.0#session-state
            HttpContext.Session.SetString("_placeholder", "placeholder");
            return;
        }
    }
}
