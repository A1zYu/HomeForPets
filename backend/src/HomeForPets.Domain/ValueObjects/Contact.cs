using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;

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
    
    public static Result<Contact,Error> Create(PhoneNumber number)
    {
        var contact = new Contact(number);
        return contact;
    }
}