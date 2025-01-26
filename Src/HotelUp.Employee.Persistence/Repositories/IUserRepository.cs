using HotelUp.Employee.Persistence.Const;
using HotelUp.Employee.Persistence.ValueObjects;

namespace HotelUp.Employee.Persistence.Repositories;

public interface IUserRepository
{
    Task<Guid> SignUpAsync(Email email, EmployeeType employeeType);
}