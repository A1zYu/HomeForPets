using CSharpFunctionalExtensions;
using HomeForPets.SharedKernel;

namespace HomeForPets.Core.Abstactions;

public interface ICommandHandler<TResponse,in TCommand>
{
    public Task<Result<TResponse,ErrorList>> Handle(TCommand command, CancellationToken ct);
}
public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task<UnitResult<ErrorList>> Handle(TCommand command, CancellationToken ct);
}