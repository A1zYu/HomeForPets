using CSharpFunctionalExtensions;

namespace HomeForPets.Domain.Models.PetModel;

public class PetPhoto  : Shared.Entity<PetPhotoId>
{
    public string Path { get; private set; } 
    public bool IsMain { get; private set; } 
    
    private PetPhoto(PetPhotoId id) : base(id)
    {
    }

    public PetPhoto(PetPhotoId id, string path, bool isMain) : base(id)
    {
        Path = path;
        IsMain = isMain;
    }
    public static Result<PetPhoto> Create(string path, bool isMain )
    {
        if (string.IsNullOrWhiteSpace(path))
            return Result.Failure<PetPhoto>("path is invalid");
        var petPhoto = new PetPhoto(PetPhotoId.Create(Guid.NewGuid()),path,isMain);
        return Result.Success(petPhoto);
    }
}