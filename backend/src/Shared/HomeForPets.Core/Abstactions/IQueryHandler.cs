using CSharpFunctionalExtensions;
using HomeForPets.SharedKernel;

namespace HomeForPets.Core.Abstactions;

public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
{
    public Task<Result<TResponse,ErrorList>> Handle(TQuery query, CancellationToken ct);
}