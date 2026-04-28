using MinhaApi.Domain.employee.entitie;
using MinhaAPI.Aplication.DTOs;
using MinhaAPI.Aplication.Interfaces;

namespace MinhaAPI.Aplication.UseCases.Employees.GetEmployees
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
