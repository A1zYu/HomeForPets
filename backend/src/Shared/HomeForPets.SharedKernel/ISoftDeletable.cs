namespace HomeForPets.SharedKernel;

public interface ISoftDeletable
{
    void Delete();
    void Restore();
}