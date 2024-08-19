﻿using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;

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

    public static Result<PaymentDetails,Error> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) && name.Length > Constraints.Constraints.LOW_VALUE_LENGTH)
            return Errors.General.Validation("Payment detail name");

        if (string.IsNullOrWhiteSpace(description) && description.Length > Constraints.Constraints.HIGH_VALUE_LENGTH)
            return Errors.General.Validation("Payment detail description");
        var paymentDetails = new PaymentDetails(name, description);
        return paymentDetails;
    }
}