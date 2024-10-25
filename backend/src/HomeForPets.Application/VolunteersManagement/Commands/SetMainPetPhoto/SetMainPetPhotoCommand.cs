using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.VolunteersManagement.Commands.SetMainPetPhoto;

public record SetMainPetPhotoCommand(Guid VolunteerId,Guid PetId, Guid PetPhotoId) : ICommand;