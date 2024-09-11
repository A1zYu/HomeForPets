using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Domain.VolunteersManagement.ValueObjects;

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
        if (string.IsNullOrWhiteSpace(text) || text.Length > Constraints.Constants.HIGH_VALUE_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Description");
        }
        var description =new Description(text);
        return description;
    }
}