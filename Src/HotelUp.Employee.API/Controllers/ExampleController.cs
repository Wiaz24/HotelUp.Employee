using System.Security.Claims;
using HotelUp.Employee.Services.DTOs;
using HotelUp.Employee.Services.Services;
using HotelUp.Employee.Shared.Auth;
using HotelUp.Employee.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HotelUp.Employee.API.Controllers;

[ApiController]
[Route("api/employee/employee")]
[ProducesErrorResponseType(typeof(ErrorResponse))]
public class ExampleController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public ExampleController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    private Guid LoggedInUserId => User.FindFirstValue(ClaimTypes.NameIdentifier)
        is { } id
        ? new Guid(id)
        : throw new TokenException("No user id found in access token.");


    [HttpGet]
    [Authorize(Policy = PoliciesNames.IsAdmin)]
    [SwaggerOperation("Get all employees")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAllEmployees()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        return Ok(employees);
    }
    
    [HttpGet("{id}")]
    [Authorize(Policy = PoliciesNames.IsAdmin)]
    [SwaggerOperation("Get employee by id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployeeById(Guid id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee is null)
        {
            return NotFound();
        }
        return Ok(employee);
    }
    
    [HttpPost]
    [Authorize(Policy = PoliciesNames.IsAdmin)]
    [SwaggerOperation("Register a new employee")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> RegisterNewEmployee([FromForm] CreateEmployeeDto dto)
    {
        var employeeId = await _employeeService.RegisterNewEmployeeAsync(dto);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeId }, null);
    }
}