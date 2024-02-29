using Dapper;
using DemoDataDump.Constants;
using DemoDataDump.Data;
using Npgsql;
using Parquet.Serialization;

namespace DemoDataDump.Service.Implementations;

public abstract class AbstractServiceBase<TModel>
{
    private NpgsqlConnection _nPglSqlConn = null!;

    public void SetNpgsqlConn(NpgsqlConnection npgsqlConnection)
    {
        _nPglSqlConn = npgsqlConnection;
    }


    protected IEnumerable<TModel> Query(string sql)
    {
        return _nPglSqlConn.Query<TModel>(sql);
    }

    protected async void GenerateFile(IEnumerable<TModel> objectInstances, string fileName)
    {
        var filePath = Path.Combine(ConstantPath.RootPath, fileName);
        //generate here...
        await ParquetSerializer.SerializeAsync(objectInstances, filePath);

        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"Generated file '{fileName}'");
        Console.ResetColor();
    }
}