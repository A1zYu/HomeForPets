using CSharpFunctionalExtensions;

namespace HomeForPets.ValueObject;

public class Address
{
    private Address()
    {
        
    }
    private Address(string city, string district, int houseNumber, int flatNumber)
    {
        City = city;
        District = district;
        HouseNumber = houseNumber;
        FlatNumber = flatNumber;
    }
    public string City { get; }
    public string District { get; }
    public int HouseNumber { get; }
    public int FlatNumber { get; }

    public static Result<Address> Create(string city, string district, int houseNumber, int flatNumber)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            return Result.Failure<Address>("City can not be empty");
        }
        if (string.IsNullOrWhiteSpace(city))
        {
            return Result.Failure<Address>("District can not be empty");
        }

        if (houseNumber <= 0)
        {
            return Result.Failure<Address>("House number can not be empty");
        }
        if (flatNumber <= 0)
        {
            return Result.Failure<Address>("Flat number must be greater than zero");
        }
        var address = new Address(city, district, houseNumber, flatNumber);
        return Result.Success(address);
    }
}