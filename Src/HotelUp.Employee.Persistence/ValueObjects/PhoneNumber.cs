using FluentValidation;
using HotelUp.Employee.Persistence.ValueObjects.Abstractions;
using HotelUp.Employee.Persistence.ValueObjects.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Employee.Persistence.ValueObjects;

public record PhoneNumber : IValueObject
{
    private PhoneNumber()
    {
    }

    private PhoneNumber(string value)
    {
        var phoneNumber = new PhoneNumber
        {
            Value = value
        };
        var validator = new PhoneNumberValidator();
        var result = validator.Validate(phoneNumber);
        if (!result.IsValid)
        {
            var failure = result.Errors.FirstOrDefault();
            throw new InvalidPhoneNumberException(failure?.ErrorMessage);
        }

        Value = value;
    }

    public string Value { get; private init; } = null!;

    public static ValueConverter GetValueConverter()
    {
        return new ValueConverter<PhoneNumber, string>(
            vo => vo.Value,
            value => new PhoneNumber(value));
    }

    public static implicit operator string(PhoneNumber phoneNumber)
    {
        return phoneNumber.Value;
    }

    public static implicit operator PhoneNumber(string value)
    {
        return new PhoneNumber(value);
    }

    private class PhoneNumberValidator : AbstractValidator<PhoneNumber>
    {
        public PhoneNumberValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .Matches(@"^(\+)?(\s*\d+\s*){9,}$");
        }
    }
}