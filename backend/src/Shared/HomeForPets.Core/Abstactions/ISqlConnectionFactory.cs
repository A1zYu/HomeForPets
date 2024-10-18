using System.Data;

namespace HomeForPets.Core.Abstactions;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}