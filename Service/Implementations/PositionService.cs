using Dapper;
using DemoDataDump.Model;
using DemoDataDump.Service.Contracts;
using DemoDataDump.Service.Implementations.Abstracts;

namespace DemoDataDump.Service.Implementations;

public class PositionService : AbstractServiceBase<Position>, IService
{
    public async Task Write()
    {
        var instances = NPglSqlConn.Query<Position>("select * from position");
        await WriteFile(instances, "Position.parquet");
    }

    public async Task Read()
    {
        await ExecuteRead(
            "" +
            "INSERT INTO position (Id, Description, DateCreated, DateUpdated, IsActive)\n" +
            "SELECT @Id, @Description, @DateCreated, @DateUpdated, @IsActive\n " +
            "WHERE NOT EXISTS (SELECT 1 FROM position WHERE Id = @Id);",
            "Position.parquet");
    }
}