using CSharpFunctionalExtensions;

namespace HomeForPets.Domain.ValueObjects;

public record Contact 
{
    private readonly List<SocialNetwork> _socialNetworks = [];
    private Contact() { }
    private Contact(PhoneNumber phoneNumber, List<SocialNetwork> socialNetworks)
    {
        PhoneNumber = phoneNumber;
        SocialNetworks = _socialNetworks;
    }
    public PhoneNumber PhoneNumber { get; }
    public  List<SocialNetwork> SocialNetworks  { get; }
    public void AddSocialNetwork(SocialNetwork socialNetwork) => _socialNetworks.Add(socialNetwork);
    
    public static Result<Contact> Create(string number, List<SocialNetwork> socialNetworks)
    {
        var phoneNumber = PhoneNumber.Create(number);
        if (!phoneNumber.IsSuccess)
            return Result.Failure<Contact>("Contact is invalid");
        var contact = new Contact(phoneNumber.Value, socialNetworks);
        return Result.Success(contact);
    }
}