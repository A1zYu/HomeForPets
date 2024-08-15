using CSharpFunctionalExtensions;

namespace HomeForPets.Domain.ValueObjects;

public record Contact 
{
    private Contact(PhoneNumber phoneNumber, List<SocialNetwork> socialNetworks)
    {
        PhoneNumber = phoneNumber;
        SocialNetworks = socialNetworks;
    }
    public PhoneNumber PhoneNumber { get; }
    public  List<SocialNetwork> SocialNetworks  { get; }

    public static Result<Contact> Create(string number, List<SocialNetwork> socialNetworks)
    {
        var phoneNumber = PhoneNumber.Create(number);
        if (!phoneNumber.IsSuccess)
            return Result.Failure<Contact>("Contact is invalid");
        var contact = new Contact(phoneNumber.Value, socialNetworks);
        return Result.Success(contact);
    }
}