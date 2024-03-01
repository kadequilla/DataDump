namespace DemoDataDump.Builder;

public interface ICliBuilder
{
    public void AddService<T>() where T : new();

    public Task BuildAsync(string[] args);
}