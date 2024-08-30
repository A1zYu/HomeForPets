using HomeForPets.Application.Dtos;

namespace HomeForPets.Application.Volunteers.Update;

public record UpdateMainInfoRequest(Guid VolunteerId, UpdateMainInfoDto Dto);
public record UpdateMainInfoDto(FullNameDto FullNameDto,string Description, int WorkExperience, string PhoneNumber);