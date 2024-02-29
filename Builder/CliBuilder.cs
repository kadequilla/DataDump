namespace DemoDataDump.Builder;

public class CliBuilder : ICliBuilder
{
    public static readonly CliBuilder Instance = new();
    private List<dynamic> ObjectInstances { get; } = [];

    public void AddService<T>() where T : new()
    {
        ObjectInstances.Add(new T());
    }

    public void Build()
    {
        foreach (var instance in ObjectInstances)
        {
            instance.OpenConnection();
            instance.Write();
            instance.CloseConnection();
        }
    }
}