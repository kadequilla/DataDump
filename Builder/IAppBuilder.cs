namespace DemoDataDump.Builder;

public interface IAppBuilder
{
    public void AddService<T>() where T : new();

    public Task BuildAsync(string[] args);
}