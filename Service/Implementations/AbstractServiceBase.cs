using Dapper;
using DemoDataDump.Constants;
using DemoDataDump.Data;
using Npgsql;
using Parquet.Serialization;

namespace DemoDataDump.Service.Implementations;

public abstract class AbstractServiceBase<TModel>
{
    private readonly NpgsqlConnection _pgDbCon = DataConnection.Connect();

    public void OpenConnection()
    {
        try
        {
            _pgDbCon.Open();
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
            _pgDbCon.Close();
            Console.WriteLine("Disconnected to PostgreSQL database.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }


    protected IEnumerable<TModel> Query(string sql)
    {
        return _pgDbCon.Query<TModel>(sql);
    }

    protected async void GenerateFile(IEnumerable<TModel> objectInstances, string fileName)
    {
        var filePath = Path.Combine(ConstantPath.RootPath, fileName);

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Writing parquet file of 'Employee'. . .");

        //generate here...
        await ParquetSerializer.SerializeAsync(objectInstances, filePath);

        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Generated file 'Employee.parquet'");
        Console.ResetColor();
    }
}