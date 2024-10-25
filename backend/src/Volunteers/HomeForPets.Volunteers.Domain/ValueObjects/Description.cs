using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.SharedKernel;

namespace HomeForPets.Volunteers.Domain.ValueObjects;

public record Description
{
    private Description()
    {
        
    }
    private Description(string text)
    {
        Text = text;
    }
    public string Text { get; }

    public static Result<Description,Error> Create(string text)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Length > Constants.HIGH_VALUE_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Description");
        }
        var description =new Description(text);
        return description;
    }
}