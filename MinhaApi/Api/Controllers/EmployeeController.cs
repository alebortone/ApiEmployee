using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaApi.Application.UseCases.Employees.CreateEmployee;
using MinhaApi.Application.UseCases.Employees.DeleteEmployee;
using MinhaApi.Application.UseCases.Employees.GetEmployeeById;
using MinhaApi.Application.UseCases.Employees.GetEmployees;
using MinhaApi.Application.UseCases.Employees.GetPhotoEmployee;
using MinhaApi.Application.UseCases.Employees.UpdateEmployee;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly CreateEmployeeHandler _create;
    private readonly GetEmployeesHandler _getAll;
    private readonly GetEmployeeByIdHandler _getById;
    private readonly DeleteEmployeeHandler _delete;
    private readonly UpdateEmployeeHandle _update;
    private readonly GetEmployeePhotoHandler _getPhoto;

    public EmployeeController(
        CreateEmployeeHandler create,
        GetEmployeesHandler getAll,
        GetEmployeeByIdHandler getById,
        DeleteEmployeeHandler delete,
        UpdateEmployeeHandle update,
        GetEmployeePhotoHandler getPhoto
    )
    {
        _create = create;
        _getAll = getAll;
        _getById = getById;
        _delete = delete;
        _update = update;
        _getPhoto = getPhoto;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateEmployeeCommand command)
    {
        var result = await _create.Handle(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAll.Handle(new GetEmployeesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _getById.Handle(new GetEmployeeByIdQuery(id));

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _delete.Handle(new DeleteEmployeeCommand(id));
        return NoContent();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommand command, Guid id)
    {
        var result = await _update.Handle(command, id);
        return Ok(result);
    }

    [HttpGet("{id}/photo")]
    public async Task<IActionResult> GetPhoto(Guid id)
    {
        var result = await _getPhoto.Handle(new GetEmployeePhotoQuery(id));
        if (result == null)
            return NoContent();

        return File(result, "image/jpeg");
    }
}