using FluentValidation;
using HotelUp.Employee.Persistence.ValueObjects.Abstractions;
using HotelUp.Employee.Persistence.ValueObjects.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Employee.Persistence.ValueObjects;

public record Email : IValueObject
{
    private Email()
    {
    }

    public Email(string value)
    {
        var email = new Email
        {
            Value = value
        };
        var validator = new EmailValidator();
        var result = validator.Validate(email);
        if (!result.IsValid)
        {
            var failure = result.Errors.FirstOrDefault();
            throw new InvalidEmailException(failure?.ErrorMessage);
        }

        Value = value;
    }

    public string Value { get; init; } = null!;

    public static ValueConverter GetValueConverter()
    {
        return new ValueConverter<Email, string>(
            vo => vo.Value,
            value => new Email(value));
    }

    public static implicit operator string(Email email)
    {
        return email.Value;
    }

    public static implicit operator Email(string value)
    {
        return new Email(value);
    }

    private class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .EmailAddress();
        }
    }
}