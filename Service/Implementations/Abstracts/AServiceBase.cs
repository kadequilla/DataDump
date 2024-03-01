using Dapper;
using DemoDataDump.Constants;
using Parquet.Serialization;

namespace DemoDataDump.Service.Implementations.Abstracts;

public abstract class AServiceBase<TModel> : AdbService where TModel : new()
{
    protected async Task WriteFile(IEnumerable<TModel> objectInstances, string fileName)
    {
        try
        {
            var filePath = Path.Combine(ConstantPath.RootPath, fileName);
            //generate here...
            await ParquetSerializer.SerializeAsync(objectInstances, filePath);
            Utils.Println(ConsoleColor.Cyan, $"Generated file '{fileName}'");
        }
        catch (Exception e)
        {
            Utils.Println(ConsoleColor.Red, e.Message);
        }
    }

    private async Task<IList<TModel>> ReadFile(string fileName)
    {
        var filePath = Path.Combine(ConstantPath.RootPath, fileName);
        return await ParquetSerializer.DeserializeAsync<TModel>(filePath);
    }

    protected async Task ExecuteRead(string qry, string fileName)
    {
        try
        {
            var instances = await ReadFile(fileName);
            var rowsAffected = await SqlConnection.ExecuteAsync(qry, instances);

            Utils.Println(ConsoleColor.Cyan,
                $"'{fileName}' was successfully inserted to database | ROWS AFFECTED: ({rowsAffected})");
        }
        catch (Exception e)
        {
            Utils.Println(ConsoleColor.Red, e.Message);
        }
    }
}