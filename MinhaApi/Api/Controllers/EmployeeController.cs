using Microsoft.AspNetCore.Mvc;
using MinhaApi.Application.UseCases.Employees.CreateEmployee;
using MinhaApi.Application.UseCases.Employees.DeleteEmployee;
using MinhaApi.Application.UseCases.Employees.GetEmployeeById;
using MinhaApi.Application.UseCases.Employees.GetEmployees;
using MinhaApi.Application.UseCases.Employees.UpdateEmployee;
using MinhaApi.Domain.employee.entitie;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly CreateEmployeeHandler _create;
    private readonly GetEmployeesHandler _getAll;
    private readonly GetEmployeeByIdHandler _getById;
    private readonly DeleteEmployeeHandler _delete;
    private readonly UpdateEmployeeHandle _update;

    public EmployeeController(
        CreateEmployeeHandler create,
        GetEmployeesHandler getAll,
        GetEmployeeByIdHandler getById,
        DeleteEmployeeHandler delete,
        UpdateEmployeeHandle update
    )
    {
        _create = create;
        _getAll = getAll;
        _getById = getById;
        _delete = delete;
        _update = update;
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
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _getById.GetById(new GetEmployeeByIdQuery(id));

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _delete.Delete(new DeleteEmployeeCommand(id));
        return NoContent();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommand command)
    {
        var result = await _update.Update(command);
        return Ok(result);
    }
}