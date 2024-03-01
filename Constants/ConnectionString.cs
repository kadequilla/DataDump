namespace DemoDataDump.Constants;

public static class ConnectionString
{
    public static readonly string PgConnString =
        "Host=localhost;Port=5432;Database=demo_db;Username=postgres;Password=1234;";

    public static readonly string MssqlConnString =
        "Server=BFI-ITG10\\MSSQLSERVER01;Database=demo_db;Trusted_Connection=True;TrustServerCertificate=true;";
}