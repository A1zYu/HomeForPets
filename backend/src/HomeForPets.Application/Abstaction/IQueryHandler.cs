namespace HomeForPets.Application.Abstaction;

public interface IQueryHandler<TResponse,in TQuery> where TQuery : IQuery
{
    public Task<TResponse> Handle(TQuery query, CancellationToken ct);
}