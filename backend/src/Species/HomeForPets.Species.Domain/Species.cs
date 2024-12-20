﻿using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.SharedKernel;
using HomeForPets.SharedKernel.Ids;

namespace HomeForPets.Species.Domain;

public class Species : SharedKernel.Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];

    public Species(SpeciesId id) : base(id) { }

    private Species(SpeciesId speciesId, string name) : base(speciesId)
    {
        Name = name;
    }

    public string Name { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;

    public UnitResult<ErrorList> AddBreed(Breed breed)
    {
        var breedExits = _breeds.Any(x => x.Name == breed.Name);
        if (breedExits)
        {
            return UnitResult.Failure(Errors.General.AlreadyExist().ToErrorList());
        }

        _breeds.Add(breed);
        return UnitResult.Success<ErrorList>();
    }

    public UnitResult<Error> RemoveBreed(BreedId breedId)
    {
        var breed = Breeds.FirstOrDefault(x => x.Id == breedId);
        if (breed is null)
        {
            return Errors.General.NotFound(breedId);
        }

        _breeds.Remove(breed);
        
        return UnitResult.Success<Error>();
    }

    public static Result<Species, Error> Create(SpeciesId speciesId, string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.LOW_VALUE_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Species name");
        }

        return new Species(speciesId, name);
    }
}