using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoListApp.Enums;
using ToDoListApp.Interfaces;
using ToDoListApp.Models;
using ToDoListApp.ViewModels;

namespace ToDoListApp.Services
{
    public class ToDoItemService : ICreateService<ToDoItemViewModel>
    {
        private readonly ToDoDbContext dbContext;

        public ToDoItemService(ToDoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(ToDoItemViewModel model)
        {
            var toDoItem = new ToDoItem
            {
                Name = model.Name,
                Priority = model.Priority,
                Status = model.Status
            };

            await dbContext.ToDoItems.AddAsync(toDoItem);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync()
        {
            foreach (var item in dbContext.ToDoItems.Where(item => item.Status == ToDoItemState.Completed))
            {
                this.dbContext.ToDoItems.Remove(item);
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
}
