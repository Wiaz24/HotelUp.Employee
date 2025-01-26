using HotelUp.Employee.Persistence.Const;
using HotelUp.Employee.Persistence.Repositories;
using HotelUp.Employee.Services.DTOs;
using HotelUp.Employee.Services.Events;
using HotelUp.Employee.Services.Services;
using HotelUp.Employee.Services.Services.Exceptions;
using MassTransit;
using NSubstitute;
using Shouldly;

namespace HotelUp.Employee.Tests.Unit.Services;

public class EmployeeServiceTests
{
    [Fact]
    public async Task RegisterNewEmployeeAsync_WhenEmployeeExist_ShouldThrowEmployeeAlreadyExistsException()
    {
        // Arrange
        var employeeRepository = Substitute.For<IEmployeeRepository>();
        var userRepository = Substitute.For<IUserRepository>();
        var bus = Substitute.For<IPublishEndpoint>();
        var employeeService = new EmployeeService(employeeRepository, userRepository, bus);
        var createEmployeeDto = new CreateEmployeeDto
        {
            FirstName = "John",
            LastName = "Cramer",
            Email = "example@email.com",
            PhoneNumber = "+48123456789",
            Role = EmployeeType.Admin
        };
        var existingEmployee = new Persistence.Entities.Employee
        {
            Id = Guid.NewGuid(),
            FirstName = createEmployeeDto.FirstName,
            LastName = createEmployeeDto.LastName,
            Email = createEmployeeDto.Email,
            PhoneNumber = createEmployeeDto.PhoneNumber,
            Role = createEmployeeDto.Role
        };
        
        employeeRepository.GetByEmailAsync(createEmployeeDto.Email)
            .Returns(existingEmployee);
        
        // Act
        var exception = await Record.ExceptionAsync(async () => 
            await employeeService.RegisterNewEmployeeAsync(createEmployeeDto));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmployeeAlreadyExistsException>();
    }

    [Fact]
    public async Task RegisterNewEmployeeAsync_WhenEmployeeNotExist_ShouldCreateEmployee()
    {
        // Arrange
        var employeeRepository = Substitute.For<IEmployeeRepository>();
        var userRepository = Substitute.For<IUserRepository>();
        var bus = Substitute.For<IPublishEndpoint>();
        var employeeService = new EmployeeService(employeeRepository, userRepository, bus);
        var createEmployeeDto = new CreateEmployeeDto
        {
            FirstName = "John",
            LastName = "Cramer",
            Email = "example@email.com",
            PhoneNumber = "+48123456789",
            Role = EmployeeType.Admin
        };

        employeeRepository.GetByEmailAsync(createEmployeeDto.Email)
            .Returns((Persistence.Entities.Employee?)null);
        userRepository.SignUpAsync(createEmployeeDto.Email, createEmployeeDto.Role)
            .Returns(Guid.NewGuid());

        // Act
        var result = await employeeService.RegisterNewEmployeeAsync(createEmployeeDto);

        // Assert
        result.ShouldNotBe(Guid.Empty);
        await bus.Received(1).Publish(Arg.Any<EmployeeCreatedEvent>());
    }
}