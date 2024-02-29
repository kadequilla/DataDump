using DemoDataDump.Model;
using DemoDataDump.Service.Contracts;

namespace DemoDataDump.Service.Implementations;

public class PositionService : AbstractServiceBase<Position>, IService
{
    public void Write()
    {
        var employees = Query("SELECT * FROM position");
        GenerateFile(employees, "Position.parquet");
    }
}