using ToDoApp.Data;
using ToDoListApp.Interfaces;
using ToDoListApp.ViewModels;

namespace ToDoListApp.Services
{
    public class ToDoItemEditService : IEditService<EditToDoItemViewModel>
    {
        private readonly ToDoDbContext dbContext;

        public ToDoItemEditService(ToDoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task EditAsync(EditToDoItemViewModel model)
        {
            var currentToDoItem = this.dbContext.ToDoItems.Find(model.Id);
            if (currentToDoItem != null)
            {
                currentToDoItem.Name = model.Name;
                currentToDoItem.Priority = model.Priority;
                currentToDoItem.Status = model.Status;

                await this.dbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("To Do Item was not found!");
            }
        }

        public async Task<EditToDoItemViewModel> LoadModelAsync(Guid id)
        {
            EditToDoItemViewModel viewModel = null;
            var toDoItem = await this.dbContext.ToDoItems.FindAsync(id);
            if (toDoItem != null)
            {
                viewModel = new EditToDoItemViewModel()
                {
                    Id = toDoItem.Id,
                    Name = toDoItem.Name,
                    Priority = toDoItem.Priority,
                    Status = toDoItem.Status
                };
            }
            else
            {
                throw new KeyNotFoundException("To Do Item was not found!");
            }

            return viewModel;
        }
    }
}
