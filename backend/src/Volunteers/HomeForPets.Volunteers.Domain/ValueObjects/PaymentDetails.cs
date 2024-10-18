using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.SharedKernel;

namespace HomeForPets.Volunteers.Domain.ValueObjects;
public record PaymentDetails
{
    private PaymentDetails() { }
    private PaymentDetails(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public string Name { get; } 
    public string Description { get; }

    public static Result<PaymentDetails,Error> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.LOW_VALUE_LENGTH)
            return Errors.General.ValueIsInvalid("Payment detail name");

        if (string.IsNullOrWhiteSpace(description) || description.Length > Constants.HIGH_VALUE_LENGTH)
            return Errors.General.ValueIsInvalid("Payment detail description");
        var paymentDetails = new PaymentDetails(name, description);
        return paymentDetails;
    }
}