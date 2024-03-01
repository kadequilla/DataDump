using Dapper;
using DemoDataDump.Model;
using DemoDataDump.Service.Contracts;

namespace DemoDataDump.Service.Implementations;

public class EmployeeService : AServiceBase<Employee>, IService
{
    public async Task Write()
    {
        var instances =
            NPglSqlConn.Query<Employee>("select * from employees");

        await WriteFile(instances, "Employee.parquet");
    }

    public async Task Read()
    {
        try
        {
            const string fileName = "Employee.parquet";
            var instances = await ReadFile(fileName);

            var qry = "" +
                      "INSERT INTO EMPLOYEES (id, first_name, last_name, email, mobile_no, date_of_birth)\n" +
                      "SELECT @Id, @First_Name, @Last_Name, @Email, @Mobile_No, @Date_of_birth\n" +
                      "WHERE NOT EXISTS (SELECT 1 FROM EMPLOYEES WHERE id = @Id);";

            var rowsAffected = await SqlConnection.ExecuteAsync(qry, instances);

            Utils.Println(ConsoleColor.Cyan,
                $"Successfully inserted {fileName} to database | ROWS AFFECTED: ({rowsAffected})");
        }
        catch (Exception e)
        {
            Utils.Println(ConsoleColor.Red, e.Message);
        }
    }
}