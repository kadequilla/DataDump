using Dapper;
using DemoDataDump.Model;
using DemoDataDump.Service.Contracts;
using DemoDataDump.Service.Implementations.Abstracts;

namespace DemoDataDump.Service.Implementations;

public class EmployeeService : AbstractServiceBase<Employee>, IService
{
    public async Task Write()
    {
        var instances =
            NPglSqlConn.Query<Employee>("select * from employees");

        await WriteFile(instances, "Employee.parquet");
    }

    public async Task Read()
    {
        await ExecuteRead(
            "" +
            "INSERT INTO EMPLOYEES (id, first_name, last_name, email, mobile_no, date_of_birth)\n" +
            "SELECT @Id, @First_Name, @Last_Name, @Email, @Mobile_No, @Date_of_birth\n" +
            "WHERE NOT EXISTS (SELECT 1 FROM EMPLOYEES WHERE id = @Id);",
            "Employee.parquet");
    }
}