using DemoDataDump.Constants;
using Npgsql;

namespace DemoDataDump.Data;

public class DataConnection
{
    public static readonly DataConnection Instance = new();

    public static NpgsqlConnection Connect()
    {
        return new NpgsqlConnection(ConnectionString.PgConnString);
    }
}