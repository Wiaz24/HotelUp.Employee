using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Employee.Persistence.ValueObjects.Abstractions;

public interface IValueObject
{
    public static abstract ValueConverter GetValueConverter();
}