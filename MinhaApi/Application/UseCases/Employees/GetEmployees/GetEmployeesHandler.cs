using MinhaApi.Application.Interfaces;
using MinhaApi.Domain.employee.entitie;

namespace MinhaApi.Application.UseCases.Employees.GetEmployees
{
    public class GetEmployeesHandler
    {
        private readonly IEmployeeRepository _repo;

        public GetEmployeesHandler(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Employee>> GetAll(GetEmployeesQuery query)
        {
            return await _repo.GetAll();
        }
    }
}
