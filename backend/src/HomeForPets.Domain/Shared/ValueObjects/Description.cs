using CSharpFunctionalExtensions;

namespace HomeForPets.Domain.Shared.ValueObjects;

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
        if (string.IsNullOrWhiteSpace(text) || text.Length > Constraints.Constraints.HIGH_VALUE_LENGTH)
        {
            return Errors.General.Validation("Description");
        }
        var description =new Description(text);
        return description;
    }
}