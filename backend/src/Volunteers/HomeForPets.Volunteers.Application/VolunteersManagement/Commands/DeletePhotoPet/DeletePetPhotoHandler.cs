using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Abstaction;
using HomeForPets.Core.Extensions;
using HomeForPets.Volunteers.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.DeletePhotoPet;

public class DeletePetPhotoHandler : ICommandHandler<DeletePhotoPetCommand>
{
    private const string BUCKET_NAME = "pet-photos";
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<DeletePetPhotoHandler> _logger;
    private readonly IValidator<DeletePhotoPetCommand> _validator;
    private readonly IFileProvider _fileProvider;

    public DeletePetPhotoHandler(IUnitOfWork unitOfWork, IVolunteersRepository volunteersRepository, ILogger<DeletePetPhotoHandler> logger, IValidator<DeletePhotoPetCommand> validator, IFileProvider fileProvider)
    {
        _unitOfWork = unitOfWork;
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _fileProvider = fileProvider;
    }

    public async Task<UnitResult<ErrorList>> Handle(DeletePhotoPetCommand command, CancellationToken ct)
    {
        using var transaction = await _unitOfWork.BeginTransaction(ct);
        try
        {
            var validationResult = await _validator.ValidateAsync(command,ct);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrorList();
            }
            var volunteer = await _volunteersRepository.GetById(command.VolunteerId,ct);
            if (volunteer.IsFailure)
            {
                return volunteer.Error.ToErrorList();
            }
            var pet = volunteer.Value.Pets.FirstOrDefault(x=>x.Id.Value == command.PetId);
            if (pet is null)
            {
                return Errors.General.NotFound(command.PetId).ToErrorList();
            }
            var photo = pet.PetPhotos.FirstOrDefault(x=>x.Id.Value == command.PhotoId);
            if (photo is null)
            {
                return Errors.General.NotFound(command.PhotoId).ToErrorList();
            }

            // var resultDelete = await _fileProvider.DeleteFile(new FileInfo(FilePath.Create(photo.Path).Value, BUCKET_NAME),ct);
            // if (resultDelete.IsFailure)
            // {
                // return resultDelete.Error.ToErrorList();
            // }
            var result = volunteer.Value.DeletePetPhoto(command.PetId, command.PhotoId);

            if (result.IsFailure)
            {
                return result.Error.ToErrorList();
            }

            await _unitOfWork.SaveChanges(ct);
            
            transaction.Commit();

            return UnitResult.Success<ErrorList>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            transaction.Rollback();
            return Errors.General.ValueIsInvalid().ToErrorList();
        }
        
    }
}