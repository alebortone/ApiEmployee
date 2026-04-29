using MinhaApi.Domain.employee.entitie;
using MinhaAPI.Aplication.DTOs;
using MinhaAPI.Aplication.Interfaces;

namespace MinhaAPI.Aplication.UseCases.Employees.UpdateEmployee
{
    public class UpdateEmployeeHandle
    {
        private readonly IEmployeeRepository _repoEmployee;
        private readonly IFileStorage _fileStorage;

        public UpdateEmployeeHandle(IEmployeeRepository repoEmployee, IFileStorage fileStorage)
        {
            _repoEmployee = repoEmployee;
            _fileStorage = fileStorage;
        }

        public async Task<EmployeeResponse> Handle(UpdateEmployeeCommand command, Guid id)
        {
            if (await _repoEmployee.IsEmailAlreadyUse(command.email, id))
                throw new Exception("Email ja foi cadastrado no sistema!");
            
            var employee = await _repoEmployee.GetById(id);
            
            if (employee == null)
            {
                throw new Exception("Funcionario nao encontrado");
            }

            string? filePath = null;

            if (command.photo != null)
            {
                filePath = await _fileStorage.SaveFileAsync(command.photo);
            }

            employee.ToUpdate(command.name, command.email, command.age, filePath);

             await _repoEmployee.Update(employee);
             return EmployeeResponse.ToResponse(employee);
            
            
        }

    }
}