namespace HomeForPets.Application.VolunteersManagement.Commands.DeletePhotoFromPet;

public record DeleteFileFromPetCommand(Guid VolunteerId, Guid PetId,Guid PhotoId);