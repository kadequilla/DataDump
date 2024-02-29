using CommandLine;
using DemoDataDump.Data;
using DemoDataDump.DTOs;
using Microsoft.IO;

namespace DemoDataDump.Builder;

public class CliBuilder : ICliBuilder
{
    public static readonly CliBuilder Instance = new();
    private List<dynamic> ObjectInstances { get; } = [];

    public void AddService<T>() where T : new()
    {
        ObjectInstances.Add(new T());
    }

    public void Build(string[] args)
    {
        var dataCon = DataConnection.Instance;
        dataCon.OpenConnection();

        Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(o =>
            {
                if (o.Write)
                {
                    RunWrite(dataCon);
                }
            });
        dataCon.CloseConnection();
    }

    private void RunWrite(DataConnection dataCon)
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Writing . . .");

            foreach (var instance in ObjectInstances)
            {
                instance.SetNpgsqlConn(dataCon.NpgsqlConnection);
                instance.Write();
            }

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Successfully export sql data to parquet file.");
            Console.ResetColor();
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();
        }
    }
}