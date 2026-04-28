using MinhaApi.Domain.employee.entitie;

namespace MinhaApi.Application.UseCases.Employees.UpdateEmployee
{
    public record UpdateEmployeeCommand(string name, int age, string email)
    {
    }
}
