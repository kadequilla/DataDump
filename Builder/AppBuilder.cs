using DemoDataDump.Data;

namespace DemoDataDump.Builder;

public class AppBuilder : IAppBuilder
{
    public static readonly AppBuilder Instance = new();
    private List<dynamic> ObjectInstances { get; } = [];

    public void AddService<T>() where T : new()
    {
        ObjectInstances.Add(new T());
    }

    public async Task BuildAsync(string[] args)
    {
        var dataCon = DataConnection.Instance;
        await dataCon.OpenConnectionAsync();

        //RUN ALL ASYNC
        //Step 1: Write Parquet file.
        await RunWrite(dataCon);
        Utils.Println(ConsoleColor.DarkGreen, "DONE WRITING \u2713 \n");

        //Step 2: Insert Parquet file data to database.
        await RunRead(dataCon);
        Utils.Println(ConsoleColor.DarkGreen, "DONE INSERT TO DATABASE \u2713 \n");

        /* RUN PER ARGS
         * if (args.First().Equals("write"))
            {
                await RunWrite(dataCon);
            }
            else if (args.First().Equals("read"))
            {
                await RunRead(dataCon);
            }
         */

        await dataCon.CloseConnectionAsync();
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