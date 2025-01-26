namespace HotelUp.Employee.Persistence.Repositories;

public interface IEmployeeRepository
{
    Task<Entities.Employee?> GetByIdAsync(Guid id);
    Task<Entities.Employee?> GetByEmailAsync(string email);
    Task<List<Entities.Employee>> GetAllAsync();
    Task AddAsync(Entities.Employee employee);
}