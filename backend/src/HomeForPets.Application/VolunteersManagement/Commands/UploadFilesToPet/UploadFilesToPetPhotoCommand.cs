using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Dtos;

namespace HomeForPets.Application.VolunteersManagement.Commands.UploadFilesToPet;

public record UploadFilesToPetPhotoCommand(Guid VolunteerId,Guid PetId,IEnumerable<UploadFileDto> Files) : ICommand;