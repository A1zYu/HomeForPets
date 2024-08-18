using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Domain.ValueObjects;

public record FullName 
{
    private FullName(string lastName, string firstName, string? middleName)
    {
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
    }
    public string LastName { get;}
    public string FirstName { get;}
    public string? MiddleName { get;}
    public override string ToString()
    {
        return $"{FirstName} {LastName} {MiddleName}";
    }

    public static Result<FullName,Error> Create(string firstName, string lastName, string? middleName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Errors.General.Validation("first Name");

        } 
        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Errors.General.Validation("Last Name");
        } 
        
        var fullName = new FullName(lastName, firstName, middleName);
        return fullName;
    }
}