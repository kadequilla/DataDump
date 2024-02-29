using CommandLine;

namespace DemoDataDump.DTOs;

public class Options
{
    [Option('w', "write", Required = false, HelpText = "Set output to verbose messages.")]
    public bool Write { get; set; }
}