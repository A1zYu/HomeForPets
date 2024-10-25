using System.Data;

namespace HomeForPets.Application.Database;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}