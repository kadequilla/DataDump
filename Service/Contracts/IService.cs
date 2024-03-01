using DemoDataDump.Service.Implementations;

namespace DemoDataDump.Service.Contracts;

public interface IService
{
    public Task Write();
    public Task Read();
}