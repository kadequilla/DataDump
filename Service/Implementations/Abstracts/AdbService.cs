using System.Data.SqlClient;
using Npgsql;

namespace DemoDataDump.Service.Implementations.Abstracts;

public abstract class AdbService
{
    protected NpgsqlConnection NPglSqlConn = null!;
    protected SqlConnection SqlConnection = null!;

    public void SetDbConnInstance(NpgsqlConnection npgsqlConnection)
    {
        NPglSqlConn = npgsqlConnection;
    }

    public void SetDbConnInstance(SqlConnection sqlConnection)
    {
        SqlConnection = sqlConnection;
    }
}