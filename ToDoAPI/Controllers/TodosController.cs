using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.DataAccess;

namespace ToDoAPI.Controllers
{
    public record TodoRequest(string Text);

    [ApiController]
    [Route("api/todos")]
    public class TodosController(AppDbContext db) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetAll()
        {
            return await GetAllItems();
        }

        [HttpPost]
        public async Task<ActionResult<List<TodoItem>>> Create(TodoRequest request)
        {
            db.Todos.Add(new TodoItem { Text = request.Text });
            await db.SaveChangesAsync();

            return await GetAllItems();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<List<TodoItem>>> Update(int id, TodoRequest request)
        {
            var item = await db.Todos.FindAsync(id);
            if (item is null) return NotFound();

            item.Text = request.Text;
            await db.SaveChangesAsync();

            return await GetAllItems();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List<TodoItem>>> Delete(int id)
        {
            var item = await db.Todos.FindAsync(id);
            if (item is null) return NotFound();

            db.Todos.Remove(item);
            await db.SaveChangesAsync();

            return await GetAllItems();
        }

        private Task<List<TodoItem>> GetAllItems()
        {
            return db.Todos.AsNoTracking().OrderBy(t => t.Id).ToListAsync();
        }
    }
}