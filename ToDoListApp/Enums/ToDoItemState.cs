using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Enums
{
    public enum ToDoItemState
    {
        [Display(Name = "Not Started")]
        NotStarted,
        [Display(Name = "In Progress")]
        InProgress,
        [Display(Name = "Completed")]
        Completed
    }
}
