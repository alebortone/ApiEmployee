using MinhaApi.Application.DTOs;
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

        public async Task<List<EmployeeResponse>> Handle(GetEmployeesQuery query)
        {
            var employees =  await _repo.GetAll();

            return EmployeeResponse.FromList(employees);
        
        }
    }
}
