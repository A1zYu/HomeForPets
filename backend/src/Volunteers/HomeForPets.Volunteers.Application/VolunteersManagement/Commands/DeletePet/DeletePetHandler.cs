using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Abstactions;
using HomeForPets.SharedKernel;
using HomeForPets.SharedKernel.Ids;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.DeletePhotoPet;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.DeletePet;

public class DeletePetHandler : ICommandHandler<DeletePetCommand>
{
    private const string BUCKET_NAME = "pet-photos";
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<DeletePetPhotoHandler> _logger;
    private readonly IValidator<DeletePhotoPetCommand> _validator;
    private readonly IFileProvider _fileProvider;

    public DeletePetHandler(IUnitOfWork unitOfWork, IVolunteersRepository volunteersRepository,
        ILogger<DeletePetPhotoHandler> logger, IValidator<DeletePhotoPetCommand> validator, IFileProvider fileProvider)
    {
        _unitOfWork = unitOfWork;
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _fileProvider = fileProvider;
    }

    public async Task<UnitResult<ErrorList>> Handle(DeletePetCommand command, CancellationToken ct)
    {
        var volunteer = await _volunteersRepository.GetById(command.VolunteerId, ct);
        if (volunteer.IsFailure)
        {
            return volunteer.Error.ToErrorList();
        }

        var petId = PetId.Create(command.PetId);

        var pet = volunteer.Value.Pets.FirstOrDefault(p => p.Id == petId);
        if (pet is null)
        {
            return volunteer.Error.ToErrorList();
        }

        var petPhotos = pet.PetPhotos.ToList();

        // foreach (var photo in petPhotos)
        // {
        // var fileInfoCommnad = new FileInfoCommnad(FilePath.Create(photo.Path).Value,BUCKET_NAME);

        // var deleteFileinMinioResult =await _fileProvider.DeleteFile(fileInfoCommnad,ct);
        // if (deleteFileinMinioResult.IsFailure)
        // {
        // return deleteFileinMinioResult.Error.ToErrorList();
        // }
        // var resultDelete =  volunteer.Value.DeletePetPhoto(petId.Value, photo.Id);

        // if (resultDelete.IsFailure)
        // {
        // return resultDelete.Error.ToErrorList();
        // }
        // }

        var result = volunteer.Value.DeletePet(petId);

        if (result.IsFailure)
        {
            return result.Error.ToErrorList();
        }

        await _unitOfWork.SaveChanges(ct);

        return UnitResult.Success<ErrorList>();
    }
}