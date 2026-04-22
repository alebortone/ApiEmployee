using Microsoft.AspNetCore.Mvc;
using MinhaApi.Application.UseCases.Employees.CreateEmployee;
using MinhaApi.Application.UseCases.Employees.DeleteEmployee;
using MinhaApi.Application.UseCases.Employees.GetEmployeeById;
using MinhaApi.Application.UseCases.Employees.GetEmployees;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly CreateEmployeeHandler _create;
    private readonly GetEmployeesHandler _getAll;
    private readonly GetEmployeeByIdHandler _getById;
    private readonly DeleteEmployeeHandler _delete;

    public EmployeeController(
        CreateEmployeeHandler create,
        GetEmployeesHandler getAll,
        GetEmployeeByIdHandler getById,
        DeleteEmployeeHandler delete
    )
    {
        _create = create;
        _getAll = getAll;
        _getById = getById;
        _delete = delete;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateEmployeeCommand command)
    {
        var result = await _create.Create(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAll.GetAll(new GetEmployeesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _getById.GetById(new GetEmployeeByIdQuery(id));

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _delete.Delete(new DeleteEmployeeCommand(id));
        return NoContent();
    }
}