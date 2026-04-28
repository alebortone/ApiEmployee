using MinhaAPI.Aplication.DTOs;
using MinhaAPI.Aplication.Interfaces;

namespace MinhaAPI.Aplication.UseCases.Employees.GetEmployeeById
{
    public class GetEmployeeByIdHandler
    {
        private readonly IEmployeeRepository _repo;

        public GetEmployeeByIdHandler(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<EmployeeResponse?> Handle(GetEmployeeByIdQuery query)
        {
            var employee = await _repo.GetById(query.Id);

            if (employee == null)
                throw new Exception("Usuario não encontrado");

            return EmployeeResponse.ToResponse(employee);
        }
    }
}
