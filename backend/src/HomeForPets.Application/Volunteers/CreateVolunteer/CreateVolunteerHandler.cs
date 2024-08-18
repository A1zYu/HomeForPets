using CSharpFunctionalExtensions;
using HomeForPets.Domain.Models.Volunteer;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.ValueObjects;

namespace HomeForPets.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }
    public async Task<Result<Guid,Error>> Handle(CreateVolunteerRequest request,CancellationToken cancellationToken=default)
    {
        var volunteerId = VolunteerId.NewId();
        var fullname = FullName.Create(request.FirstName, request.LastName, request.MiddleName);
        if (fullname.IsFailure)
        {
            return fullname.Error;
        }
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);
        if (phoneNumber.IsFailure)
        {
            return phoneNumber.Error;
        }
        var isPhoneNumber =await _volunteersRepository.GetByPhoneNumber(phoneNumber.Value);
        if (isPhoneNumber.IsFailure)
        {
            return isPhoneNumber.Error;
        }
        var contact = Contact.Create(phoneNumber.Value);
        if (contact.IsFailure)
        {
            return contact.Error;
        }
        
        
        var volunteer = Volunteer.Create(
            volunteerId,
            fullname.Value,
            request.Description,
            contact.Value,
            request.WorkExperience
        );
        if (volunteer.IsFailure)
        {
            return volunteer.Error;
        }
        
        return await _volunteersRepository.Add(volunteer.Value,cancellationToken);;
    }
}