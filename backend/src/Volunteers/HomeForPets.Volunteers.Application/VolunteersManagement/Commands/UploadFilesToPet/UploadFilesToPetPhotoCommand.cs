using HomeForPets.Core.Abstaction;
using HomeForPets.Core.FilesDto;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.UploadFilesToPet;

public record UploadFilesToPetPhotoCommand(Guid VolunteerId,Guid PetId,IEnumerable<UploadFileDto> Files) : ICommand;