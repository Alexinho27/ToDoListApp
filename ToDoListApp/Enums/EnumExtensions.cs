using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ToDoListApp.Enums
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this ToDoItemState enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}
