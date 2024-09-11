using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;

namespace HomeForPets.Domain.Volunteers;

public class PetPhoto  : Shared.Entity<PetPhotoId>
{
    public string Path { get; private set; } 
    public bool IsMain { get; private set; }

    public void SetMain() => IsMain = !IsMain;
    
    private PetPhoto(PetPhotoId id) : base(id)
    {
    }

    private PetPhoto(PetPhotoId id, string path, bool isMain) : base(id)
    {
        Path = path;
        IsMain = isMain;
    }
    public static Result<PetPhoto,Error> Create(PetPhotoId petPhotoId,string path, bool isMain )
    {
        if (string.IsNullOrWhiteSpace(path))
            return Errors.General.ValueIsInvalid("path");

        var petPhoto = new PetPhoto(petPhotoId,path,isMain);
        return petPhoto;
    }
}