using MinhaApi.Domain.employee.entitie;
using MinhaApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Aplication.Interfaces;

namespace MinhaApi.Infrastructure.Repositories
{
    public class EmployeeRepository : RepositoryGeneric<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ConnectionContext context) : base(context)
        {
        }

        public async Task<Employee?> GetByEmail(string email)
        {
            return await context.Set<Employee>().FirstOrDefaultAsync(x => x.Email == email);

        }

        public async Task<bool> IsEmailAlreadyUse(string email, Guid? id)
        {
          
           return await context.Set<Employee>().AnyAsync(x => x.Email == email && x.Id != id);

        }
    }
}

