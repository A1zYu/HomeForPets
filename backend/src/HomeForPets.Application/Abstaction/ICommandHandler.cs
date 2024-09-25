﻿using CSharpFunctionalExtensions;
using HomeForPets.Application.VolunteersManagement.Commands.AddPet;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Application.Abstaction;

public interface ICommandHandler<TResponse,in TCommand> where TCommand : ICommand
{
    public Task<Result<Guid,ErrorList>> Handle(TCommand command, CancellationToken ct);
}
public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task<UnitResult<ErrorList>> Handle(TCommand command, CancellationToken ct);
}