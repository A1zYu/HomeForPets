using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Abstactions;
using HomeForPets.Core.Extensions;
using HomeForPets.Core.Messaging;
using HomeForPets.SharedKernel;
using HomeForPets.SharedKernel.Ids;
using HomeForPets.Volunteers.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.UploadFilesToPet;

public class UploadFilesToPetPhotoHandler : ICommandHandler<Guid, UploadFilesToPetPhotoCommand>
{
    private readonly IFileProvider _fileProvider;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UploadFilesToPetPhotoHandler> _logger;
    private const string BUCKET_NAME = "pet-photos";
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UploadFilesToPetPhotoCommand> _validator;
    // private readonly IMessageQueue<IEnumerable<FileInfoCommnad>> _messageQueue;

    public UploadFilesToPetPhotoHandler(
        IFileProvider fileProvider,
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IValidator<UploadFilesToPetPhotoCommand> validator,
        // IMessageQueue<IEnumerable<FileInfoCommnad>> messageQueue,
        ILogger<UploadFilesToPetPhotoHandler> logger)
    {
        _fileProvider = fileProvider;
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        // _messageQueue = messageQueue;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UploadFilesToPetPhotoCommand command,
        CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validatorResult.IsValid == false)
        {
            validatorResult.ToErrorList();
        }

        var volunteerResult = await _volunteersRepository
            .GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);
        if (volunteerResult.IsFailure)
        {
            return volunteerResult.Error.ToErrorList();
        }

        var petId = PetId.Create(command.PetId).Value;

        var pet = volunteerResult.Value.GetPetById(petId);
        if (pet.IsFailure)
        {
            pet.Error.ToErrorList();
        }

        List<FileData> filesData = [];
        // foreach (var file in command.Files)
        // {
        //     var extension = Path.GetExtension(file.FileName);
        //
        //     var filePath = FilePath.Create(Guid.NewGuid(), extension);
        //     
        //     if (filePath.IsFailure)
        //         return filePath.Error.ToErrorList();
        //
        //     var fileData = new FileData(file.Content, new FileInfoCommnad(filePath.Value, BUCKET_NAME));
        //
        //     filesData.Add(fileData);
        // }

        var uploadResult = await _fileProvider.UploadFiles(filesData, cancellationToken);
        if (uploadResult.IsFailure)
        {
            // await _messageQueue.WriteAsync(filesData.Select(f=>f.InfoCommnad), cancellationToken);
            return uploadResult.Error.ToErrorList();
        }

        var petPhotos = filesData
            .Select(x => PetPhoto.Create(PetPhotoId.NewId(), x.InfoCommnad.FilePath.Path, false).Value)
            .ToList();
        if (petPhotos.Count != 0)
        {
            pet.Value.AddPetPhotos(petPhotos);
        }

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Success uploaded files to pet - {id}", pet.Value.Id.Value);

        return command.VolunteerId;
    }
}