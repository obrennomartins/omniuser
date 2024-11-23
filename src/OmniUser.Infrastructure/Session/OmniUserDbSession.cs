using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace OmniUser.Infrastructure.Session;

public sealed class OmniUserDbSession : IDisposable
{
    public OmniUserDbSession(IConfiguration configuration)
    {
        Connection = new MySqlConnection(configuration.GetConnectionString("MYSQLCONNSTR_localdb"));
        Connection.Open();
    }

    public IDbConnection Connection { get; }
    public IDbTransaction? Transaction { get; set; }

    public void Dispose() {
        Connection.Dispose();
        Transaction?.Dispose();
    }
}
