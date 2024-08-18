using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Domain.ValueObjects;

public record PhoneNumber
{
    private static readonly Regex ValidationRegex = new Regex(
        @"^\+7\s?\(?(\d{3})\)?[\s.-]?(\d{3})[\s.-]?(\d{2})[\s.-]?(\d{2})$",
        RegexOptions.Singleline | RegexOptions.Compiled);
    public PhoneNumber(string number)
    {
        Number = number;
    }
    public string Number { get; }

    public static Result<PhoneNumber,Error> Create(string number)
    {
        if (string.IsNullOrWhiteSpace(number) || !ValidationRegex.IsMatch(number))
        {
            return Errors.General.Validation("Phone number");
        }
        var phoneNumber = new PhoneNumber(number);
        return phoneNumber;
    }
}