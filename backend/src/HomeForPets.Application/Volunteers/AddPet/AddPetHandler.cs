using CSharpFunctionalExtensions;
using HomeForPets.Application.Providers;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Application.Volunteers.AddPet;

public class AddPetHandler
{
    private readonly IFileProvider _fileProvider;

    public AddPetHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> Handle(
        
        CancellationToken cancellationToken = default)
    {
        return "";
    }
}