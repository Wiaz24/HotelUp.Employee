using HotelUp.Employee.Services.DTOs;

namespace HotelUp.Employee.Services.Services;

public interface IEmployeeService
{
    Task<Guid> RegisterNewEmployeeAsync(CreateEmployeeDto dto);
    Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
    Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id);
}