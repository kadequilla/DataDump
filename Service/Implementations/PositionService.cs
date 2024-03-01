using Dapper;
using DemoDataDump.Model;
using DemoDataDump.Service.Contracts;

namespace DemoDataDump.Service.Implementations;

public class PositionService : AServiceBase<Position>, IService
{
    public async Task Write()
    {
        var instances
            = NPglSqlConn.Query<Position>("select * from position");
        await WriteFile(instances, "Position.parquet");
    }

    public async Task Read()
    {
        try
        {
            const string fileName = "Position.parquet";
            var instances = await ReadFile(fileName);

            var qry = "" +
                      "INSERT INTO position (Id, Description, DateCreated, DateUpdated, IsActive)\n" +
                      "SELECT @Id, @Description, @DateCreated, @DateUpdated, @IsActive\n " +
                      "WHERE NOT EXISTS (SELECT 1 FROM position WHERE Id = @Id);";

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