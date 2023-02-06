using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoApp.Data
{

    public class ToDoDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        {

        }
    }
}

