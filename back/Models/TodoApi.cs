
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public string SessionId { get; set; }
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
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }

    // public class InMemoryListManager
    // {
    //     private Dictionary<string, List<TodoItem>> allLists = new Dictionary<string, List<TodoItem>>();
        
    //     public List<TodoItem> GetList(string sessionId)
    //     {
    //         if (allLists.TryGetValue(sessionId, out var list)){
    //             return list;
    //         }
    //         list = new List<TodoItem>();
    //         allLists.Add(sessionId, list);
    //         return list;
    //     }
    // }
}