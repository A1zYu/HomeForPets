using System.Data;

namespace HomeForPets.Core;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}