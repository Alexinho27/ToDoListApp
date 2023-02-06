using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using ToDoApp.Data;
using ToDoListApp.Enums;
using ToDoListApp.Interfaces;
using ToDoListApp.Models;
using ToDoListApp.ViewModels;

namespace ToDoListApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ToDoDbContext dbContext;
        private readonly ICreateService<ToDoItemViewModel> service;

        public List<ToDoItem> ToDoList { get { return this.dbContext.ToDoItems.ToList(); } }

        public IndexModel(ToDoDbContext dbContext, ICreateService<ToDoItemViewModel> service)
        {
            this.dbContext = dbContext;
            this.service = service;
        }

        public async Task OnPostDelete()
        {
            await service.DeleteAsync();

            TempData["success"] = "Completed To Do Items were successfully deleted!";
        }
    }
}