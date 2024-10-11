using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.Core.Abstaction;
using HomeForPets.Core.Dtos.Volunteers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetVolunteerById;

public class GetVolunteerByIdHandler : IQueryHandler<VolunteerDto, GetVolunteerByIdQuery>
{
    private readonly ILogger<GetVolunteerByIdHandler> _logger;
    private readonly IReadDbContext _readDbContext;

    public GetVolunteerByIdHandler(ILogger<GetVolunteerByIdHandler> logger, IReadDbContext readDbContext)
    {
        _logger = logger;
        _readDbContext = readDbContext;
    }
    public async Task<Result<VolunteerDto, ErrorList>> Handle(
        GetVolunteerByIdQuery query,
        CancellationToken token = default)
    {
        var volunteerDto = await _readDbContext.Volunteers
            .FirstOrDefaultAsync(v => v.Id == query.Id, token);

        if (volunteerDto is null)
            return Errors.General.NotFound(query.Id).ToErrorList();

        _logger.Log(LogLevel.Information, "Get volunteer with Id {volunteerId}", query.Id);
        return volunteerDto;
    }
}