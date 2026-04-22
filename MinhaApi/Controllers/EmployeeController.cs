using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaApi.Application.Services;
using MinhaApi.Application.ViewModel;


namespace MinhaApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {

        private readonly EmployeeService employeeService;


        public EmployeeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] EmployeeViewModel employeeViewModel)
        {
            var employee = await employeeService.Create(employeeViewModel);
            return Ok(employee);

        }

        [HttpGet]
        [Route("{id}/download")]
        public async Task<IActionResult> DownloadPhoto(int id)
        {
            var photoBytes = await employeeService.GetPhotoBytes(id);

            if (photoBytes == null) return NotFound();

            return File(photoBytes, "image/png");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var employees = await employeeService.GetAll();
            return Ok(employees);

        }

    }
}
