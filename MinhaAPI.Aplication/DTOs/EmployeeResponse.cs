using MinhaApi.Domain.employee.entitie;

namespace MinhaAPI.Aplication.DTOs;

public record EmployeeResponse(Guid Id, string Name, int Age, string? Photo, string Email)
{

    public static List<EmployeeResponse> FromList(IEnumerable<Employee> employees)
    {
        return employees.Select(e => ToResponse(e)).ToList();
    }

    private static string? FormatPhoto(string? photo)
    {
        if (string.IsNullOrWhiteSpace(photo))
            return null;

        return "/" + photo.Replace("\\", "/");
    }
    public static EmployeeResponse ToResponse(Employee employee)
    {
        return new EmployeeResponse(
            employee.Id,
            employee.Name,
            employee.Age,
            FormatPhoto(employee.Photo),
            employee.Email
        );
    }
}