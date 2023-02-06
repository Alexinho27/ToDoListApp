using System.ComponentModel.DataAnnotations;
using ToDoListApp.Enums;
using ToDoListApp.Interfaces;

namespace ToDoListApp.ViewModels
{
    public class ToDoItemViewModel : IViewModel
    {
        [Required]
        public string? Name { get; set; }
        public int Priority { get; set; }
        public ToDoItemState Status { get; set; }
    }
}
