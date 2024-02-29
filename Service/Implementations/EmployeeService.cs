using DemoDataDump.Model;
using DemoDataDump.Service.Contracts;

namespace DemoDataDump.Service.Implementations;

public class EmployeeService : AbstractServiceBase<Employee>, IService
{
    public void Write()
    {
        var employees = Query("SELECT * FROM employees");
        GenerateFile(employees, "Employee.parquet");
    }
}