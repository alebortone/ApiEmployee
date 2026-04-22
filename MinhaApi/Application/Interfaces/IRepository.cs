namespace MinhaApi.Application.Interfaces
{
    public interface IRepositoryGeneric<T> where T : class
    {
        Task Add(T entity);
        Task<List<T>> GetAll();
        Task Delete(Guid id);
        Task<T?> GetById(Guid id);
        Task Update(T entity, Guid id);
    }
}
