using DemoDataDump.Constants;
using Parquet.Serialization;

namespace DemoDataDump.Service.Implementations;

public abstract class AServiceBase<TModel> : AdbService where TModel : new()
{
    protected async Task WriteFile(IEnumerable<TModel> objectInstances, string fileName)
    {
        var filePath = Path.Combine(ConstantPath.RootPath, fileName);
        //generate here...
        await ParquetSerializer.SerializeAsync(objectInstances, filePath);
        Utils.Println(ConsoleColor.Cyan, $"Generated file '{fileName}'");
    }

    protected async Task<IList<TModel>> ReadFile(string fileName)
    {
        var filePath = Path.Combine(ConstantPath.RootPath, fileName);
        return await ParquetSerializer.DeserializeAsync<TModel>(filePath);
    }
}