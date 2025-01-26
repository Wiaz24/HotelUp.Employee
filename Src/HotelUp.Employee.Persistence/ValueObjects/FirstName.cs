using HotelUp.Employee.Persistence.ValueObjects.Abstractions;
using HotelUp.Employee.Persistence.ValueObjects.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Employee.Persistence.ValueObjects;

public record FirstName : IValueObject
{
    public FirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new InvalidFirstNameException(value);

        Value = value;
    }

    public string Value { get; init; }

    public static ValueConverter GetValueConverter()
    {
        return new ValueConverter<FirstName, string>(
            vo => vo.Value,
            value => new FirstName(value));
    }

    public static implicit operator string(FirstName valueObject)
    {
        return valueObject.Value;
    }

    public static implicit operator FirstName(string value)
    {
        return new FirstName(value);
    }
}