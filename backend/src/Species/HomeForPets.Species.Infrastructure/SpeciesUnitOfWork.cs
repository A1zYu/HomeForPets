using System.Data;
using HomeForPets.Core.Abstactions;
using HomeForPets.Species.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace HomeForPets.Species.Infrastructure;

public class SpeciesUnitOfWork : IUnitOfWork
{
    private readonly SpeciesWriteDbContext _dbContext;

    public SpeciesUnitOfWork(SpeciesWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}