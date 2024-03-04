using DemoDataDump.Builder;
using DemoDataDump.Service.Implementations;

var builder = AppBuilder.Instance;

builder.AddService<EmployeeService>();
builder.AddService<PositionService>();

await builder.BuildAsync(args);