namespace ToDoListApp.Interfaces
{
    public interface IEditService<T> where T : IEditViewModel
    {
        Task EditAsync(T model);

        Task<T> LoadModelAsync(Guid id);
    }
}
