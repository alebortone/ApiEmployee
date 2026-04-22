namespace MinhaApi.Application.Interfaces
{
    public interface IRepositoryGeneric<T> where T : class
    {
        Task Add(T entity);
        Task<List<T>> GetAll();
        Task Delete(int id);
        Task<T?> GetById(int id);
        Task Update(T entity, int id);
    }
}
