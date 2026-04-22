using Microsoft.EntityFrameworkCore;
using MinhaApi.Domain.employee.entitie;

namespace MinhaApi.Infratruture.Data
{
    public class ConnectionContext: DbContext{

        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }

       
    }
}
