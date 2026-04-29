using Microsoft.EntityFrameworkCore;
using MinhaApi.Domain.employee.entitie;
using MinhaApi.Infrastructure.Data;
using MinhaAPI.Aplication.Abstracoes.Filter;
using MinhaAPI.Aplication.Interfaces;

namespace MinhaApi.Infrastructure.Repositories
{
    public class EmployeeRepository : RepositoryGeneric<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ConnectionContext context) : base(context)
        {
        }

        protected override List<string> OrderColumns => new()
    {
        "Id",
        "Name",
        "Email"
    };

        protected override List<FilterColumn> FilterColumns => new()
    {
        new FilterColumn { Column = "Name", Type = "string" },
        new FilterColumn { Column = "Email", Type = "string" }
    };

        public async Task<Employee?> GetByEmail(string email)
        {
            return await context.Set<Employee>().FirstOrDefaultAsync(x => x.Email == email);

        }

        public async Task<bool> IsEmailAlreadyUse(string email, Guid? id)
        {
          
           return await context.Set<Employee>().AnyAsync(x => x.Email == email && x.Id != id);

        }

        public async Task ConfirmedEmail(Guid id)
        {
            var employee = await GetById(id);

            employee.MarkEmailAsSent();
            await Update(employee);
        }
    }
}

