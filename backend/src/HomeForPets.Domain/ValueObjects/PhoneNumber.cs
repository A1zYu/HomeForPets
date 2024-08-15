using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace HomeForPets.Domain.ValueObjects;

public record PhoneNumber
{
    private static readonly Regex ValidationRegex = new Regex(
        @"(^\+\d{1,3}\d{10}$|^$)",
        RegexOptions.Singleline | RegexOptions.Compiled);
    public PhoneNumber(string number)
    {
        Number = number;
    }
    public string Number { get; }

    public static Result<PhoneNumber> Create(string number)
    {
        if (string.IsNullOrWhiteSpace(number) && !ValidationRegex.IsMatch(number))
        {
            return Result.Failure<PhoneNumber>("Number is not correct");
        }
        var phoneNumber = new PhoneNumber(number);
        return Result.Success(phoneNumber);
    }
}