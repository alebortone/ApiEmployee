using MinhaAPI.Aplication.Abstracoes.Filter;
using MinhaAPI.Aplication.DTOs;

namespace MinhaAPI.Aplication.Interfaces
{
    public interface IRepositoryGeneric<T> where T : class
    {
        Task Add(T entity);
        Task<List<T>> GetAll();

        Task<PagedResponse<T>> GetPagined(
            int page,
            int limit,
            List<FilterObject>? filters,
            string? orderBy,
            string? orderDirection
        );
        Task Delete(Guid id);
        Task<T?> GetById(Guid id);
        Task Update(T entity);
    }
}
