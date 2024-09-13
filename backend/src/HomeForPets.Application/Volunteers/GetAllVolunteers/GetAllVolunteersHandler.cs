using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.VolunteersManagement;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.Volunteers.GetAllVolunteers;

public class GetAllVolunteersHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<GetAllVolunteersHandler> _logger;

    public GetAllVolunteersHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<GetAllVolunteersHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<List<VolunteersDto>,ErrorList>> Handle(
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var volunteers =await _volunteersRepository.GetAll(page,pageSize,cancellationToken);

        var volunteersDto = volunteers.Select(v => new VolunteersDto
        {
            Id = v.Id,
            FullName = v.FullName.FirstName + " " + v.FullName.LastName,
            PhoneNumber = v.PhoneNumber.Number,
            YearsOfExperience = v.YearsOfExperience.Value,
        }).ToList();
        
        return volunteersDto;
    }
    
}

public class VolunteersDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public int? YearsOfExperience { get; set; }
}