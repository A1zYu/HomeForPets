using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.Volunteers;

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
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;
        var fullname = FullName.Create(request.FirstName, request.LastName, request.MiddleName).Value;
        var description = Description.Create(request.Description).Value;
        var existVolunteerByPhone =await _volunteersRepository.GetByPhoneNumber(phoneNumber);
        if (existVolunteerByPhone is not null)
            return Errors.Volunteer.AlreadyExist();
        var volunteer = Volunteer.Create(
            volunteerId,
            fullname,
            phoneNumber,
            description,
            request.WorkExperience
        );
        if (volunteer.IsFailure)
        {
            return volunteer.Error;
        }
        return await _volunteersRepository.Add(volunteer.Value,cancellationToken);;
    }
}