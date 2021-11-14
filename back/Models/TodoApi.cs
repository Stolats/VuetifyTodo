
using Microsoft.EntityFrameworkCore;

// File must be given name of the namespace??
namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }

    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

    // The database context is the main class that coordinates Entity Framework functionality for a data model. 
    // This class is created by deriving from the Microsoft.EntityFrameworkCore.DbContext class.   
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {

        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}