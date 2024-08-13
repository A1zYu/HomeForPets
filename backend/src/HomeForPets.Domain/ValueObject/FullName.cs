using CSharpFunctionalExtensions;

namespace HomeForPets.ValueObject;

public class FullName
{
    //ef core
    private FullName(){}

    private FullName(string lastName, string firstName, string middleName)
    {
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
    }
    public string LastName { get;} = default!;
    public string FirstName { get;  } = default!;
    public string MiddleName { get;  } = default!;
    public override string ToString()
    {
        return $"{FirstName} {LastName} {MiddleName}";
    }

    public static Result<FullName> Create(string firstName, string lastName, string middleName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Result.Failure<FullName>("firstName can not be empty");
        } 
        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Result.Failure<FullName>("lastName can not be empty");
        } 
        if (string.IsNullOrWhiteSpace(middleName))
        {
            return Result.Failure<FullName>("middleName can not be empty");
        }

        var fullName = new FullName(lastName, firstName, middleName);
        return Result.Success(fullName);
    }
}