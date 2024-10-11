using System.Data;
using HomeForPets.Core;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace HomeForPets.Infrastructure;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()=>
    new NpgsqlConnection(_configuration.GetConnectionString(Constants.DATABASE));
}