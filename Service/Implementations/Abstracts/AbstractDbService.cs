﻿using System.Data.SqlClient;
using System.Transactions;
using Npgsql;

namespace DemoDataDump.Service.Implementations.Abstracts;

public abstract class AbstractDbService
{
    protected NpgsqlConnection NPglSqlConn = null!;
    protected SqlConnection SqlConnection = null!;


    public void SetDbConnInstance(NpgsqlConnection npgsqlConnection)
    {
        NPglSqlConn = npgsqlConnection;
    }

    public void SetDbConnInstance(SqlConnection sqlConnection)
    {
        SqlConnection = sqlConnection;
    }
}