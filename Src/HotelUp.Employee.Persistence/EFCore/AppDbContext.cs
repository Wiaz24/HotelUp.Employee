using HotelUp.Employee.Persistence.EFCore.Config;
using HotelUp.Employee.Persistence.EFCore.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HotelUp.Employee.Persistence.EFCore;

public class AppDbContext : DbContext
{
    public DbSet<Entities.Employee> Employees { get; set; }
    private readonly PostgresOptions _postgresOptions;

    public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<PostgresOptions> postgresOptions)
        : base(options)
    {
        _postgresOptions = postgresOptions.Value;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(_postgresOptions.SchemaName);

        var configuration = new DbContextConfiguration();
        modelBuilder.ApplyConfiguration<Entities.Employee>(configuration);

        base.OnModelCreating(modelBuilder);
    }
}