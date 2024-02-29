namespace DemoDataDump.Model;

public class Position
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public bool IsActive { get; set; }
}