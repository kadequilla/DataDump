using DemoDataDump.Data;

namespace DemoDataDump.Builder;

public class CliBuilder : ICliBuilder
{
    public static readonly CliBuilder Instance = new();
    private List<dynamic> ObjectInstances { get; } = [];

    public void AddService<T>() where T : new()
    {
        ObjectInstances.Add(new T());
    }

    public async Task BuildAsync(string[] args)
    {
        var dataCon = DataConnection.Instance;
        dataCon.OpenConnection();

        if (args.First().Equals("write"))
        {
            await RunWrite(dataCon);
        }
        else if (args.First().Equals("read"))
        {
            await RunRead(dataCon);
        }

        Utils.Println(ConsoleColor.DarkGreen, "DONE \u2713");
        dataCon.CloseConnection();
    }

    private async Task RunWrite(DataConnection dataCon)
    {
        try
        {
            Utils.Println(ConsoleColor.Blue, "Writing . . ");
            foreach (var instance in ObjectInstances)
            {
                instance.SetDbConnInstance(dataCon.NpgsqlConnection);
                await instance.Write();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    private async Task RunRead(DataConnection dataCon)
    {
        try
        {
            Utils.Println(ConsoleColor.Blue, "Reading . . .");
            foreach (var instance in ObjectInstances)
            {
                instance.SetDbConnInstance(dataCon.SqlConnection);
                await instance.Read();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}