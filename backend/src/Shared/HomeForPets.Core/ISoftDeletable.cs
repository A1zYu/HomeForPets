namespace HomeForPets.Core;

public interface ISoftDeletable
{
    void Delete();
    void Restore();
}