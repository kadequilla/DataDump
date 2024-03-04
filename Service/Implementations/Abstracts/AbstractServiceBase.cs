using Dapper;
using DemoDataDump.Constants;
using Parquet.Serialization;
using Timer = System.Threading.Timer;

namespace DemoDataDump.Service.Implementations.Abstracts;

public abstract class AbstractServiceBase<TModel> : AbstractDbService where TModel : new()
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
        var transaction = SqlConnection.BeginTransaction();
        try
        {
            Utils.Println(ConsoleColor.Blue, $"Reading {fileName} . . .");
            var modelInstances = await ReadFile(fileName);

            Utils.Println(ConsoleColor.Cyan, $"Inserting {fileName} . . .");
            var timer = new Timer(Utils.TimerCallback!, null, 0, 1000);
            var rowsAffected = await SqlConnection.ExecuteAsync(qry, modelInstances, transaction);

            await timer.DisposeAsync(); //dispose timer if success

            Utils.Println(ConsoleColor.Cyan,
                $"'{fileName}' was successfully inserted to database | ROWS AFFECTED: ({rowsAffected}) | Elapsed Time: {Utils.ElapsedSeconds} seconds.\n");
            Utils.ElapsedSeconds = 0;

            transaction.Commit(); //commit rows to database
        }
        catch (Exception e)
        {
            Utils.Println(ConsoleColor.Red, e.Message);
            transaction.Rollback();
        }
    }
}