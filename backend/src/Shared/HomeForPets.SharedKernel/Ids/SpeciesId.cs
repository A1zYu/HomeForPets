﻿namespace HomeForPets.SharedKernel.Ids;

public record SpeciesId
{
    private SpeciesId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static SpeciesId NewId => new(Guid.NewGuid());

    public static SpeciesId NewEmptyId => new(Guid.Empty);

    public static SpeciesId Create(Guid id) => new(id);
    
    public static implicit operator SpeciesId(Guid id) => new(id);
    public static implicit operator Guid(SpeciesId speciesId)
    {
        ArgumentNullException.ThrowIfNull(speciesId);
        return speciesId.Value;
    }
}