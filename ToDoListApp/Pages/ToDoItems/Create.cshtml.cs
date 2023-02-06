using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoApp.Data;
using ToDoListApp.Interfaces;
using ToDoListApp.ViewModels;

namespace ToDoListApp.Pages.ToDoItems
{
    public class CreateModel : PageModel
    {
        private readonly ToDoDbContext dbContext;
        private readonly ICreateService<ToDoItemViewModel> toDoItemService;

        [BindProperty]
        public ToDoItemViewModel ToDoItemModel { get; set; }

        public CreateModel(ToDoDbContext dbContext, ICreateService<ToDoItemViewModel> service)
        {
            this.dbContext = dbContext;
            this.toDoItemService = service;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostSave()
        {
            if (this.dbContext.ToDoItems.Any(x => x.Name == this.ToDoItemModel.Name))
            {
                ModelState.AddModelError("Error", "Duplicate name of To Do Item");
                ViewData["ErrorMessage"] = "Duplicate name of To Do Item";
            }
            if (ModelState.IsValid)
            {
                await this.toDoItemService.CreateAsync(this.ToDoItemModel);

                TempData["success"] = "To Do Item created successfully!";

                return Redirect("/index");
            }

            return Page();
        }
    }
}
