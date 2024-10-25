using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.SharedKernel;

namespace HomeForPets.Volunteers.Domain.ValueObjects;

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

    public static Result<FullName,Error> Create(string firstName, string lastName, string? middleName = null)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > Constants.LOW_VALUE_LENGTH)
        {
            return Errors.General.ValueIsInvalid("First name");

        } 
        if (string.IsNullOrWhiteSpace(lastName)|| lastName.Length > Constants.LOW_VALUE_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Last name");
        } 
        
        var fullName = new FullName(lastName, firstName, middleName);
        return fullName;
    }
}