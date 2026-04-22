using MinhaApi.Domain.employee.entitie;

namespace MinhaApi.Application.DTOs;

public record EmployeeResponse(Guid Id, string Name, int Age, string? Photo, string Email)
{

    public static List<EmployeeResponse> FromList(IEnumerable<Employee> employees)
    {
        return employees.Select(e => ToResponse(e)).ToList();
    }
    public static EmployeeResponse ToResponse(Employee employee)
    {
        return new EmployeeResponse(
            employee.Id,
            employee.Name,
            employee.Age,
            employee.Photo,
            employee.Email
        );
    }
}