using MinhaApi.Application.Interfaces;
using MinhaApi.Domain.employee.entitie;

namespace MinhaApi.Application.UseCases.Employees.GetEmployeeById
{
    public class GetEmployeeByIdHandler
    {
        private readonly IEmployeeRepository _repo;

        public GetEmployeeByIdHandler(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<Employee?> GetById(GetEmployeeByIdQuery query)
        {
            return await _repo.GetById(query.Id);
        }
    }
}
