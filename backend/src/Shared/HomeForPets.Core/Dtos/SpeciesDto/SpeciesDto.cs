namespace HomeForPets.Core.Dtos.SpeciesDto;

public class SpeciesDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IReadOnlyList<BreedDto> BreedDtos { get; set; }
}