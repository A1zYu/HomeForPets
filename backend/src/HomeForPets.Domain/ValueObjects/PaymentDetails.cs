using CSharpFunctionalExtensions;

namespace HomeForPets.Domain.ValueObjects;

public record PaymentDetails
{
    private PaymentDetails(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public string Name { get; } 
    public string Description { get; }

    public static Result<PaymentDetails> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) && name.Length > Constraints.Constraints.LOW_VALUE_LENGTH)
            return Result.Failure<PaymentDetails>("name is invalid");
        if (string.IsNullOrWhiteSpace(description) && description.Length > Constraints.Constraints.HIGH_VALUE_LENGTH)
            return Result.Failure<PaymentDetails>("name is invalid");
        var paymentDetails = new PaymentDetails(name, description);
        return Result.Success(paymentDetails);
    }
}