
using Microsoft.AspNetCore.Mvc;
using MinhaAPI.Aplication.UseCases.Employees.CreateEmployee;
using MinhaAPI.Aplication.UseCases.Employees.DeleteEmployee;
using MinhaAPI.Aplication.UseCases.Employees.GetEmployeeById;
using MinhaAPI.Aplication.UseCases.Employees.GetEmployees;
using MinhaAPI.Aplication.UseCases.Employees.GetPhotoEmployee;
using MinhaAPI.Aplication.UseCases.Employees.UpdateEmployee;
using MinhaAPI.Aplication.UseCases.Employees.GetPdfEmployee;

//[Authorize]
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
    private readonly GetPdfEmployeeHandler _getPdf;

    public EmployeeController(
        CreateEmployeeHandler create,
        GetEmployeesHandler getAll,
        GetEmployeeByIdHandler getById,
        DeleteEmployeeHandler delete,
        UpdateEmployeeHandle update,
        GetEmployeePhotoHandler getPhoto,
        GetPdfEmployeeHandler getPdf
          
    )
    {
        _create = create;
        _getAll = getAll;
        _getById = getById;
        _delete = delete;
        _update = update;
        _getPhoto = getPhoto;
        _getPdf = getPdf;
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

    [HttpGet("{id}/pdf")]
    public async Task<IActionResult> GetPdf(Guid id)
    {
        var pdf = await _getPdf.Handler(new GetPdfEmployeeQuery(id));

        return File(pdf, "application/pdf", $"funcionario.pdf");

    }
}