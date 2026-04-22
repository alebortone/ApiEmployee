using MinhaApi.Application.Interfaces;
using MinhaApi.Domain.employee.entitie;
using MinhaApi.Infrastructure.Data;

namespace MinhaApi.Infrastructure.Repositories
{
    public class EmployeeRepository : RepositoryGeneric<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ConnectionContext context) : base(context)
        {
        }

        public async Task<byte[]> GetPhotoBytesAsync(Guid id)
        {
            var employee = await GetById(id);
            if (employee == null || string.IsNullOrEmpty(employee.Photo))
                return null!;

            return await File.ReadAllBytesAsync(employee.Photo);
        }

    }
}

