using DemoDataDump.Constants;
using Npgsql;

namespace DemoDataDump.Data;

public class DataConnection
{
    public static readonly DataConnection Instance = new();

    private static NpgsqlConnection Connect()
    {
        return new NpgsqlConnection(ConnectionString.PgConnString);
    }

    public readonly NpgsqlConnection NpgsqlConnection = Connect();

    public void OpenConnection()
    {
        try
        {
            NpgsqlConnection.Open();
            Console.WriteLine("Connected to PostgreSQL database.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public void CloseConnection()
    {
        try
        {
            NpgsqlConnection.Close();
            Console.WriteLine("Disconnected to PostgreSQL database.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}