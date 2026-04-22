using MinhaApi.Domain.employee.entitie;
using MinhaApi.Repository.RepositorioAbstrato;

namespace MinhaApi.Domain.employee.Interface
{
    public interface IEmployeeRepository : IRepositoryGeneric<Employee>
    {
        Task<byte[]> GetPhotoBytesAsync(int id);
    }
}