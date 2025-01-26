using HotelUp.Employee.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Employee.Persistence.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _dbContext;

    public EmployeeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Entities.Employee?> GetByIdAsync(Guid id)
    {
        return _dbContext.Employees
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Entities.Employee?> GetByEmailAsync(string email)
    {
        return _dbContext.Employees
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public Task<List<Entities.Employee>> GetAllAsync()
    {
        return _dbContext.Employees.ToListAsync();
    }

    public Task AddAsync(Entities.Employee employee)
    {
        _dbContext.Employees.Add(employee);
        return _dbContext.SaveChangesAsync();
    }
}