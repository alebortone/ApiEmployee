using MinhaApi.Domain.employee.entitie;

namespace MinhaApi.Application.Interfaces
{
    public interface IEmployeeRepository : IRepositoryGeneric<Employee>
    {
        Task<byte[]> GetPhotoBytesAsync(int id);
    }
}