namespace ToDoListApp.Interfaces
{
    public interface ICreateService<T> where T : IViewModel
    {
        Task CreateAsync(T model);

        Task DeleteAsync();

    }
}
