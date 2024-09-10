using HomeForPets.Application.Dtos;

namespace HomeForPets.Application.Volunteers.UploadFilesToPet;

public record UploadFilesToPetPhotoCommand(Guid VolunteerId,Guid PetId,IEnumerable<UploadFileDto> Files);