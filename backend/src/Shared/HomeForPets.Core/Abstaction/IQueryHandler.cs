using CSharpFunctionalExtensions;

namespace HomeForPets.Core.Abstaction;

public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
{
    public Task<Result<TResponse,ErrorList>> Handle(TQuery query, CancellationToken ct);
}