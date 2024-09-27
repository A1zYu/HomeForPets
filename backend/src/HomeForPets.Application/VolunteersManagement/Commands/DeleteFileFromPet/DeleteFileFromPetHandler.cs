using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Database;
using HomeForPets.Application.Extensions;
using HomeForPets.Application.Files;
using HomeForPets.Application.VolunteersManagement.Commands.DeletePhotoFromPet;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;
using Microsoft.Extensions.Logging;
using FileInfo = HomeForPets.Application.Files.FileInfo;

namespace HomeForPets.Application.VolunteersManagement.Commands.DeleteFileFromPet;

public class DeleteFileFromPetHandler : ICommandHandler<bool, DeleteFileFromPetCommand>
{
    private const string BUCKET_NAME = "pet-photos";
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<DeleteFileFromPetHandler> _logger;
    private readonly IValidator<DeleteFileFromPetCommand> _validator;
    private readonly IFileProvider _fileProvider;

    public DeleteFileFromPetHandler(IUnitOfWork unitOfWork, IVolunteersRepository volunteersRepository, ILogger<DeleteFileFromPetHandler> logger, IValidator<DeleteFileFromPetCommand> validator, IFileProvider fileProvider)
    {
        _unitOfWork = unitOfWork;
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _fileProvider = fileProvider;
    }

    public async Task<Result<bool, ErrorList>> Handle(DeleteFileFromPetCommand command, CancellationToken ct)
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
        var photo = pet?.PetPhotos?.FirstOrDefault(x=>x.Id.Value == command.PhotoId);
        
        await _fileProvider.DeleteFile(new FileInfo(FilePath.Create(photo.Path).Value, BUCKET_NAME),ct);
        var result = volunteer.Value.DeletePhotoToPet(command.PetId, command.PhotoId);

        if (result.IsFailure)
        {
            return result.Error.ToErrorList();
        }

        await _unitOfWork.SaveChanges(ct);

        return result.IsSuccess;
    }
}