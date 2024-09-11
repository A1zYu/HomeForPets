using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Domain.VolunteersManagement.ValueObjects;

public record PaymentDetailsList
{
    private PaymentDetailsList() { }
    public PaymentDetailsList(IEnumerable<PaymentDetails> paymentDetails) =>
        PaymentDetails = paymentDetails.ToList();
    public IReadOnlyList<PaymentDetails> PaymentDetails { get; }
    public static PaymentDetailsList Create(IEnumerable<PaymentDetails> list) => new(list);
}
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
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constraints.Constants.LOW_VALUE_LENGTH)
            return Errors.General.ValueIsInvalid("Payment detail name");

        if (string.IsNullOrWhiteSpace(description) || description.Length > Constraints.Constants.HIGH_VALUE_LENGTH)
            return Errors.General.ValueIsInvalid("Payment detail description");
        var paymentDetails = new PaymentDetails(name, description);
        return paymentDetails;
    }
}