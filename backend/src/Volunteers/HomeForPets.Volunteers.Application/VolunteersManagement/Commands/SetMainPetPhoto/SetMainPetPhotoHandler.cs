using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.Core.Abstactions;
using HomeForPets.SharedKernel;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.SetMainPetPhoto;

public class SetMainPetPhotoHandler : ICommandHandler<SetMainPetPhotoCommand>
{
    private readonly ILogger<SetMainPetPhotoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVolunteersRepository _volunteersRepository;

    public SetMainPetPhotoHandler(ILogger<SetMainPetPhotoHandler> logger, IUnitOfWork unitOfWork,
        IVolunteersRepository volunteersRepository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<UnitResult<ErrorList>> Handle(SetMainPetPhotoCommand command, CancellationToken ct)
    {
        var volunteer = await _volunteersRepository.GetById(command.VolunteerId, ct);
        if (volunteer.IsFailure)
        {
            return volunteer.Error.ToErrorList();
        }

        var pet =volunteer.Value.Pets.FirstOrDefault(x => x.Id.Value == command.PetId);
        if (pet is null)
        {
            return Errors.General.NotFound(command.PetId).ToErrorList();
        }

        var result = pet.SetMainPhoto(command.PetPhotoId);
        if (result.IsFailure)
        {
            return result.Error.ToErrorList();
        }

        await _unitOfWork.SaveChanges(ct);
        
        _logger.LogInformation("set main in photo:{photoId}", command.PetPhotoId);
        
        return UnitResult.Success<ErrorList>();
    }
}