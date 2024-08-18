using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Domain.Models.PetModel;

public class PetPhoto  : Shared.Entity<PetPhotoId>
{
    public string Path { get; private set; } 
    public bool IsMain { get; private set; }

    public void SetMain() => IsMain = !IsMain;
    
    private PetPhoto(PetPhotoId id) : base(id)
    {
    }

    public PetPhoto(PetPhotoId id, string path, bool isMain) : base(id)
    {
        Path = path;
        IsMain = isMain;
    }
    public static Result<PetPhoto,Error> Create(string path, bool isMain )
    {
        if (string.IsNullOrWhiteSpace(path))
            return Errors.General.Validation("path");

        var petPhoto = new PetPhoto(PetPhotoId.Create(Guid.NewGuid()),path,isMain);
        return petPhoto;
    }
}