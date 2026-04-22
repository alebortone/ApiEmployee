namespace MinhaApi.Application.UseCases.Employees.CreateEmployee
{
    public record CreateEmployeeCommand(
     string Name,
     int Age,
     IFormFile? Photo
 );
}
