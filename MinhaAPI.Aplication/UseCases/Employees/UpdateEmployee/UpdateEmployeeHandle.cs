using MinhaApi.Domain.employee.entitie;
using MinhaAPI.Aplication.DTOs;
using MinhaAPI.Aplication.Interfaces;

namespace MinhaAPI.Aplication.UseCases.Employees.UpdateEmployee
{
    public class UpdateEmployeeHandle
    {
        private readonly IEmployeeRepository _repo;

        public UpdateEmployeeHandle(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<EmployeeResponse> Handle(UpdateEmployeeCommand command, Guid id)
        {
            if (await _repo.IsEmailAlreadyUse(command.email, id))
                throw new Exception("Email ja foi cadastrado no sistema!");
            
            var employee = await _repo.GetById(id);
            
            if (employee == null)
            {
                throw new Exception("Funcionario nao encontrado");
            }

             employee.ToUpdate(command.name, command.email, command.age);

             await _repo.Update(employee);
             return EmployeeResponse.ToResponse(employee);
            
            
        }

    }
}