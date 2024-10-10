using HomeForPets.Application.Authorization.DataModels;

namespace HomeForPets.Application.Authorization;

public interface ITokenProvider
{
    string GenerateAccessToken(User user, CancellationToken cancellationToken);
}