using HotelUp.Employee.Persistence.EFCore.CustomExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelUp.Employee.Persistence.EFCore.Config;

internal sealed class DbContextConfiguration
    : IEntityTypeConfiguration<Entities.Employee>
{
    public void Configure(EntityTypeBuilder<Entities.Employee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasValueObject(x => x.FirstName);
        builder.HasValueObject(x => x.LastName);
        builder.HasValueObject(x => x.Email);
        builder.HasValueObject(x => x.PhoneNumber);
        builder.Property(x => x.Role);
    }
}