using HotelUp.Employee.Persistence.Repositories;
using HotelUp.Employee.Services.DTOs;
using HotelUp.Employee.Services.Events;
using HotelUp.Employee.Services.Services.Exceptions;
using MassTransit;

namespace HotelUp.Employee.Services.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPublishEndpoint _bus;

    public EmployeeService(IEmployeeRepository employeeRepository, IUserRepository userRepository, IPublishEndpoint bus)
    {
        _employeeRepository = employeeRepository;
        _userRepository = userRepository;
        _bus = bus;
    }

    public async Task<Guid> RegisterNewEmployeeAsync(CreateEmployeeDto dto)
    {
        var existingUser = await _employeeRepository.GetByEmailAsync(dto.Email);
        if (existingUser is not null)
        {
            throw new EmployeeAlreadyExistsException(dto.Email);
        }
        var employee = new Persistence.Entities.Employee
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Role = dto.Role
        };
        
        var employeeId = await _userRepository.SignUpAsync(employee.Email, employee.Role);
        
        employee.Id = employeeId;
        
        await _employeeRepository.AddAsync(employee);
        var employeeCreatedEvent = new EmployeeCreatedEvent
        {
            Id = employee.Id,
            Role = employee.Role
        };
        await _bus.Publish(employeeCreatedEvent);
        return employee.Id;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees.Select(EmployeeDto.FromEntity);
    }

    public async Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        return employee is null ? null : EmployeeDto.FromEntity(employee);
    }
}