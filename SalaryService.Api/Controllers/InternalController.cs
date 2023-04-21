using System.Net;
using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Services;

namespace SalaryService.Api.Controllers;

[Route("api/internal")]
[ApiController]
public class InternalController : ControllerBase
{
    private const int CreatedStatusCode = (int)HttpStatusCode.Created;
    private const int InternalServerErrorCode = (int)HttpStatusCode.InternalServerError;

    private readonly EmployeesService _employeeService;

    public InternalController(EmployeesService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost("create-employee")]
    public async Task<ActionResult> CreateEmployeeAsync([FromBody] EmployeeCreationParameters employeeCreationParameters)
    {
        try
        {
            await _employeeService.CreateAsync(employeeCreationParameters);
            return StatusCode(CreatedStatusCode);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException != null 
                ? ex.InnerException.Message 
                : ex.Message;

            return Problem(message, null, InternalServerErrorCode);
        }
    }
}
