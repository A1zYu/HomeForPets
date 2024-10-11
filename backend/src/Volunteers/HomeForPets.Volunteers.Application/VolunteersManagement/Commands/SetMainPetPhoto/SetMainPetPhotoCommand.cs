using HomeForPets.Core.Abstaction;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.SetMainPetPhoto;

public record SetMainPetPhotoCommand(Guid VolunteerId,Guid PetId, Guid PetPhotoId) : ICommand;