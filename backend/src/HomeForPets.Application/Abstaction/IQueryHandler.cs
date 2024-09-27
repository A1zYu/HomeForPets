using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Application.Abstaction;

public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
{
    public Task<Result<TResponse,ErrorList>> Handle(TQuery query, CancellationToken ct);
}