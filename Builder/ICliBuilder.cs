namespace DemoDataDump.Builder;

public interface ICliBuilder
{
    public void AddService<T>() where T : new();

    public void Build(string[] args);
}