namespace HomeForPets.Application.Dtos.SpeciesDto;

public class SpeciesDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IReadOnlyList<BreedDto> BreedDtos { get; set; }
}

public class BreedDto
{
    public Guid Id { get; set; }
    public Guid SpeciesId { get; set; }
    public string Name { get; set; }
}