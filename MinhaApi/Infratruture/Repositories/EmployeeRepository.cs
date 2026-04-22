using MinhaApi.Domain.employee.entitie;
using MinhaApi.Domain.employee.Interface;
using MinhaApi.Infratruture.Data;
using MinhaApi.Repository.RepositorioAbstrato;

namespace MinhaApi.Infratruture.Repositories
{
    public class EmployeeRepository : RepositoryGeneric<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ConnectionContext context) : base(context)
        {
        }

        public async Task<byte[]> GetPhotoBytesAsync(int id)
        {
            var employee = await GetById(id);
            if (employee == null || string.IsNullOrEmpty(employee.photo))
                return null!;

            return await File.ReadAllBytesAsync(employee.photo);
        }

    }
}

