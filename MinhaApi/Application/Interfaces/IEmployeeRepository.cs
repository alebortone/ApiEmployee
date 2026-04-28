using MinhaApi.Domain.employee.entitie;

namespace MinhaApi.Application.Interfaces
{
    public interface IEmployeeRepository : IRepositoryGeneric<Employee>
    {
        Task<Employee?> GetByEmail(string email);
        Task<bool> IsEmailAlreadyUse(string email, Guid? id = null);
    }
}