using System.ComponentModel.DataAnnotations;
using ToDoListApp.Enums;

namespace ToDoListApp.Models
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Priority { get; set; }
        public ToDoItemState Status { get; set; }
    }
}
