using DemoDataDump.Builder;
using DemoDataDump.Service.Implementations;

namespace DemoDataDump
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = CliBuilder.Instance;

            builder.AddService<EmployeeService>();
            builder.AddService<PositionService>();

            builder.Build(args);
        }
    }
}