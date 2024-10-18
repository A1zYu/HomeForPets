using HomeForPets.Accounts.Domain;

namespace HomeForPets.Accounts.Application;

public interface ITokenProvider
{
    string GenerateAccessToken(User user, CancellationToken cancellationToken);
}