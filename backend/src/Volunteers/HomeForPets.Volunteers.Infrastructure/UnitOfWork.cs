using System.Data;
using HomeForPets.Core;
using HomeForPets.Core.Abstactions;
using HomeForPets.Volunteers.Infrastucture.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace HomeForPets.Volunteers.Infrastucture;

public class UnitOfWork : IUnitOfWork
{
    private readonly VolunteerWriteDbContext _dbContext;

    public UnitOfWork(VolunteerWriteDbContext dbContext)
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