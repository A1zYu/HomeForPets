using CSharpFunctionalExtensions;
using HomeForPets.Domain.Models.PetModel;
using HomeForPets.Domain.Models.Volunteer;
using HomeForPets.Domain.ValueObjects;

namespace HomeForPets.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }
    public async Task<Result<Guid>> Handle(CreateVolunteerRequest request,CancellationToken cancellationToken=default)
    {
        var volunteerId = VolunteerId.NewId();
        var fullname = FullName.Create(request.FirstName, request.LastName, request.MiddleName);
        if (fullname.IsFailure)
        {
            return Result.Failure<Guid>(fullname.Error);
        }
        var socialNetworks = request.SocialNetworks.Select(x => SocialNetwork.Create(x.Name, x.Path).Value).ToList();
        
        var contact = Contact.Create(request.PhoneNumber, socialNetworks);
        
        
        var volunteer = Volunteer.Create(
            volunteerId,
            fullname.Value,
            request.Description,
            contact.Value,
            request.WorkExperience,
            request.Pets,
            request.PaymentDetailsList
        );
        if (volunteer.IsFailure)
        {
            return Result.Failure<Guid>(volunteer.Error);
        }
        await _volunteersRepository.Add(volunteer.Value,cancellationToken);
        
        return volunteerId.Value;
    }
}