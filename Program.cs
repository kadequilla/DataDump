using DemoDataDump.Builder;
using DemoDataDump.Service.Implementations;

var builder = CliBuilder.Instance;

builder.AddService<EmployeeService>();

builder.Build();