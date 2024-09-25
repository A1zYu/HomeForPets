using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Dtos;
using HomeForPets.Application.Dtos.FilesDto;

namespace HomeForPets.Application.VolunteersManagement.Commands.UploadFilesToPet;

public record UploadFilesToPetPhotoCommand(Guid VolunteerId,Guid PetId,IEnumerable<UploadFileDto> Files) : ICommand;