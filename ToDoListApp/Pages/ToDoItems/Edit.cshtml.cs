using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using ToDoApp.Data;
using ToDoListApp.Interfaces;
using ToDoListApp.Models;
using ToDoListApp.ViewModels;

namespace ToDoListApp.Pages.ToDoItems
{
    public class EditModel : PageModel
    {
        private readonly ToDoDbContext dbContext;
        private readonly IEditService<EditToDoItemViewModel> service;

        [BindProperty]
        public EditToDoItemViewModel EditToDoItemRequest { get; set; }

        public EditModel(ToDoDbContext dbContext, IEditService<EditToDoItemViewModel> service)
        {
            this.dbContext = dbContext;
            this.service = service;
        }

        public async Task OnGet(Guid id)
        {
            try
            {
                this.EditToDoItemRequest = await this.service.LoadModelAsync(id);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
        }

        public async Task<IActionResult> OnPost(Guid id) 
        {
            if (this.dbContext.ToDoItems.Any(x => x.Name == this.EditToDoItemRequest.Name && x.Id != id))
            {
                ModelState.AddModelError(string.Empty, "Duplicate name of To Do Item");
                ViewData["ErrorMessage"] = "Duplicate name of To Do Item";
            }
            if (ModelState.IsValid)
            {
                if (this.EditToDoItemRequest != null)
                {
                    try
                    {
                        await this.service.EditAsync(this.EditToDoItemRequest);

                        TempData["success"] = "To Do Item updated successfully!";
                        return Redirect("/index");
                    }
                    catch (Exception ex) 
                    {
                        ViewData["ErrorMessage"] = ex.Message;
                    }
                }
            }

            return Page();
        }
    }
}
