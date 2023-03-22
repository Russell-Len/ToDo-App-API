using Microsoft.EntityFrameworkCore;
using ToDo_App_API.Entity;
using Task = ToDo_App_API.Entity.Task;

namespace ToDo_App_API.DataContext
{
    public class ToDoDBContext : DbContext
    {
        public ToDoDBContext(DbContextOptions<ToDoDBContext> options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Author> Authors { get; set; }

    }
}
