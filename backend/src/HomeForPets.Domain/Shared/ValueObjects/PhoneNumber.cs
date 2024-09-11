using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace HomeForPets.Domain.Shared.ValueObjects;

public record PhoneNumber
{
    private static readonly Regex ValidationRegex = new Regex(
        @"^\+?[78][-\(]?\d{3}\)?-?\d{3}-?\d{2}-?\d{2}$",
        RegexOptions.Singleline | RegexOptions.Compiled);
    private PhoneNumber(string number)
    {
        Number = number;
    }
    public string Number { get; }

    public static Result<PhoneNumber,Error> Create(string number)
    {
        if (string.IsNullOrWhiteSpace(number) || !ValidationRegex.IsMatch(number))
        {
            return Errors.General.ValueIsInvalid("Phone number");
        }

        if (!ValidationRegex.IsMatch(number))
        {
            return Errors.PhoneNumber.Validation(number);
        }
        var phoneNumber = new PhoneNumber(number);
        return phoneNumber;
    }
}