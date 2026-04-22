using MinhaApi.Domain.employee.entitie;

namespace MinhaApi.Application.UseCases.Employees.UpdateEmployee
{
    public record UpdateEmployeeCommand(Employee employee, int id)
    {
    }
}
