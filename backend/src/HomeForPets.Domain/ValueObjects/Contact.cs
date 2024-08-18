using CSharpFunctionalExtensions;

namespace HomeForPets.Domain.ValueObjects;

public record Contact 
{
    private readonly List<SocialNetwork> _socialNetworks = [];
    private Contact() { }
    private Contact(PhoneNumber phoneNumber)
    {
        PhoneNumber = phoneNumber;
        SocialNetworks = _socialNetworks;
    }
    public PhoneNumber PhoneNumber { get; }
    public  List<SocialNetwork> SocialNetworks  { get; }
    public void AddSocialNetwork(SocialNetwork socialNetwork) => _socialNetworks.Add(socialNetwork);
    
    public static Result<Contact> Create(string number)
    {
        var phoneNumber = PhoneNumber.Create(number);
        if (!phoneNumber.IsSuccess)
            return Result.Failure<Contact>("Contact is invalid");
        var contact = new Contact(phoneNumber.Value);
        return Result.Success(contact);
    }
}