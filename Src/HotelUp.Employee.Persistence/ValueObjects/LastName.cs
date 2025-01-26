using HotelUp.Employee.Persistence.ValueObjects.Abstractions;
using HotelUp.Employee.Persistence.ValueObjects.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Employee.Persistence.ValueObjects;

public record LastName : IValueObject
{
    public LastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new InvalidLastNameException(value);
        Value = value;
    }

    public string Value { get; }

    public static ValueConverter GetValueConverter()
    {
        return new ValueConverter<LastName, string>(
            vo => vo.Value,
            value => new LastName(value));
    }

    public static implicit operator string(LastName valueObject)
    {
        return valueObject.Value;
    }

    public static implicit operator LastName(string value)
    {
        return new LastName(value);
    }
}