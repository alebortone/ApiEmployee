using MinhaApi.Application.DTOs;
using MinhaApi.Application.Interfaces;

namespace MinhaApi.Application.UseCases.Employees.GetEmployeeById
{
    public class GetEmployeeByIdHandler
    {
        private readonly IEmployeeRepository _repo;

        public GetEmployeeByIdHandler(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<EmployeeResponse?> GetById(GetEmployeeByIdQuery query)
        {
            var employee = await _repo.GetById(query.Id);

            if (employee == null)
                throw new Exception("Usuario não encontrado");

            return EmployeeResponse.ToResponse(employee);
        }
    }
}
