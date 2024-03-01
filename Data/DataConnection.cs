using System.Data.SqlClient;
using DemoDataDump.Constants;
using Npgsql;

namespace DemoDataDump.Data;

public class DataConnection
{
    public static readonly DataConnection Instance = new();


    public readonly NpgsqlConnection NpgsqlConnection = new(ConnectionString.PgConnString);
    public readonly SqlConnection SqlConnection = new(ConnectionString.MssqlConnString);

    public void OpenConnection()
    {
        try
        {
            NpgsqlConnection.Open();
            SqlConnection.Open();
            Console.WriteLine("Connected to databases.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public async Task OpenConnectionAsync()
    {
        try
        {
            await NpgsqlConnection.OpenAsync();
            await SqlConnection.OpenAsync();

            Console.WriteLine("\nConnected to databases. \n");
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
            SqlConnection.Close();
            Console.ResetColor();
            Console.WriteLine("Disconnected to databases.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public async Task CloseConnectionAsync()
    {
        try
        {
            await NpgsqlConnection.CloseAsync();
            await SqlConnection.CloseAsync();
            Console.ResetColor();
            Console.WriteLine("Disconnected to databases.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}