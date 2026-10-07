using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ToDoAPI.DataAccess
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TodoItem> Todos => Set<TodoItem>();
    }
}
