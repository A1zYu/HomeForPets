using System.Data;
using CSharpFunctionalExtensions;
using Dapper;
using HomeForPets.Application.Volunteers;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.VolunteersManagement;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace HomeForPets.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteersRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDbConnection _dbConnection;

    public VolunteersRepository(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _dbConnection = dbContext.Database.GetDbConnection();
        _dbConnection.ConnectionString = configuration.GetConnectionString("localDb");
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken ct = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, ct);
        return volunteer.Id;
    }

    public Guid Delete(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.Remove(volunteer);
        return volunteer.Id;
    }

    public Guid Save(Volunteer volunteer, CancellationToken cancellationToken)
    {
        _dbContext.Volunteers.Attach(volunteer);
        return volunteer.Id.Value;
    }

    public async Task<List<Volunteer>> GetAll(int page, int pageSize, CancellationToken cancellationToken)
    {
        var offset = (page - 1) * pageSize;
        var query = @"
        SELECT 
            id AS Id, 
            is_deleted AS IsDeleted, 
            description AS Description,
            first_name AS FirstName, 
            last_name AS LastName,
            middle_name AS MiddleName,
            phone_number AS PhoneNumber,
            years_of_experience AS YearsOfExperience,
            payment_details AS PaymentDetails,
            social_network AS SocialNetwork
        FROM volunteers
        ORDER BY Id
        OFFSET @Offset LIMIT @PageSize;";

        var volunteers = await _dbConnection.QueryAsync<Volunteer>(
            new CommandDefinition(query,
                new { Offset = offset, PageSize = pageSize },
                cancellationToken: cancellationToken));

        return volunteers.AsList();
    }

    public async Task<Result<Volunteer, Error>> GetById(
        VolunteerId id,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound();

        return volunteer;
    }

    public async Task<Volunteer?> GetByPhoneNumber(PhoneNumber phoneNumber)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        return volunteer;
    }
}