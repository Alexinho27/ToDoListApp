using System.ComponentModel.DataAnnotations;
using ToDoListApp.Enums;
using ToDoListApp.Interfaces;

namespace ToDoListApp.ViewModels
{
    public class EditToDoItemViewModel : IEditViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Priority { get; set; }
        public ToDoItemState Status { get; set; }
    }
}
