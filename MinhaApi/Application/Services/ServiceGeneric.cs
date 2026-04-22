using MinhaApi.Repository.RepositorioAbstrato;

namespace MinhaApi.Application.Services
{

    public class ServiceGeneric<T, VM> where T : class
    {
        protected readonly IRepositoryGeneric<T> _repository;

        public ServiceGeneric(IRepositoryGeneric<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<List<T>> GetAll() => await _repository.GetAll();

        public virtual async Task<T?> GetById(int id) => await _repository.GetById(id);

        public virtual async Task Delete(int id) => await _repository.Delete(id);

    }
}