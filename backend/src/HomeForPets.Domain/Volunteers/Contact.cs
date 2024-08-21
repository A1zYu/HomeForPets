using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.ValueObjects;

namespace HomeForPets.Domain.Volunteers;

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
    public void AddSocialNetwork(IEnumerable<SocialNetwork> socialNetworks) => _socialNetworks.AddRange(socialNetworks);
    
    public static Result<Contact,Error> Create(PhoneNumber number)
    {
        var contact = new Contact(number);
        return contact;
    }
}