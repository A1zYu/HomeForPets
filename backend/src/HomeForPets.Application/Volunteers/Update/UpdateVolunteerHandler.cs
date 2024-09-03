using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.Volunteers;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.Volunteers.Update;

public class UpdateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateVolunteerHandler> _logger;

    public UpdateVolunteerHandler(IVolunteersRepository volunteersRepository, ILogger<UpdateVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateMainInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult =await _volunteersRepository.GetById(request.VolunteerId,cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var newFullName = FullName.Create(
            request.Dto.FullNameDto.FirstName,
            request.Dto.FullNameDto.LastName,
            request.Dto.FullNameDto.MiddleName).Value;
        
        var newDescription = Description.Create(request.Dto.Description).Value;
        
        var newPhoneNumber = PhoneNumber.Create(request.Dto.PhoneNumber).Value;
        
        var newWorkExperience = YearsOfExperience.Create(request.Dto.WorkExperience).Value;
        
        volunteerResult.Value
            .UpdateMainInfo(newFullName,newDescription,newWorkExperience,newPhoneNumber);
        
        var result = await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);

        _logger.LogInformation(
            "Updated volunteer with id {volunteerId}",
            request.VolunteerId);
        
        return result;
    }
}
