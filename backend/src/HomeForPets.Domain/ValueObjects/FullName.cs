using CSharpFunctionalExtensions;

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

    public static Result<FullName> Create(string firstName, string lastName, string? middleName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Result.Failure<FullName>("firstName can not be empty");
        } 
        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Result.Failure<FullName>("lastName can not be empty");
        } 

        var fullName = new FullName(lastName, firstName, middleName);
        return Result.Success(fullName);
    }
}