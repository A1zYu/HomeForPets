﻿using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.SharedKernel;

namespace HomeForPets.Volunteers.Domain.ValueObjects;

public record Address 
{
    private Address(string city, string street, int houseNumber, int flatNumber)
    {
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        FlatNumber = flatNumber;
    }
    public string City { get; }
    public string Street { get; }
    public int HouseNumber { get; }
    public int FlatNumber { get; }

    public static Result<Address,Error> Create(string city, string street, int houseNumber, int flatNumber)
    {
        if (string.IsNullOrWhiteSpace(city) )
        {
            return Errors.General.ValueIsInvalid("City");
        }
        if (string.IsNullOrWhiteSpace(city))
        {
            return Errors.General.ValueIsInvalid("street");

        }
        if (houseNumber < 0)
        {
            return Errors.General.ValueIsInvalid("houseNumber");
        }
        if (flatNumber < 0)
        {
            return Errors.General.ValueIsInvalid("flatNumber");
        }
        var address = new Address(city, street, houseNumber, flatNumber);
        return address;
    }
}