using System.Runtime.InteropServices.JavaScript;

namespace DemoDataDump.Model;

public class Employee
{
    public int Id { get; set; }
    public string? First_Name { get; set; }
    public string? Last_Name { get; set; }
    public string? Email { get; set; }
    public long MobileNo { get; set; }
    public DateTime DateOfBirth { get; set; }
}