using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Data.Common;
using System.Data.SqlClient;
using Devart.Data.PostgreSql;
using Npgsql;
using Mono.Security;
using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;
using IBM.Data.DB2;
using FirebirdSql.Data.FirebirdClient;

/// <summary>
/// Summary description for ConnectionManager
/// </summary>
public class ConnectionManager
{
    public ConnectionManager() { }

    public DbConnection TryConnection(ConnectionData connectionData)
    {
        DbConnection connection = null;
        switch (connectionData.Type)
        {
            case "PostgreSQL":
                //connection = new PgSqlConnection();
                //connection.ConnectionString = String.Format("Host={0};Port={1};Database={2};User Id={3};Password={4};"
                //    , connectionData.Server, connectionData.Port, connectionData.Db, connectionData.User, connectionData.Password);
                connection = new NpgsqlConnection();
                connection.ConnectionString = String.Format("Server={0};Port={1};Database={2};User Id={3};Password={4};"
                    , connectionData.Server, connectionData.Port, connectionData.Db, connectionData.User, connectionData.Password);
                break;
            case "MySQL":
                connection = new MySqlConnection();
                connection.ConnectionString = String.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};"
                    , connectionData.Server, connectionData.Port, connectionData.Db, connectionData.User, connectionData.Password);
                break;
            case "Microsoft SQL Server":
                if (new Regex("[a-zA-Zа-яА-Я]").IsMatch(connectionData.Server))
                {
                    connectionData.Ip = Dns.GetHostAddresses(connectionData.Server)[0].ToString();
                }
                else
                {
                    connectionData.Ip = connectionData.Server;
                }
                connection = new SqlConnection();
                connection.ConnectionString = String.Format("Data Source={0},{1};Initial Catalog={2};User ID={3};Password={4};"
                    , connectionData.Ip, connectionData.Port, connectionData.Db, connectionData.User, connectionData.Password);
                //connection.ConnectionString = String.Format("Data Source={0},{1};Network Library=DBMSSOCN;Initial Catalog={2};User ID={3};Password={4};"
                //    , connectionData.Ip, connectionData.Port, connectionData.Db, connectionData.User, connectionData.Password);
                break;
            case "Oracle":
                connection = new OracleConnection();
                connection.ConnectionString = String.Format("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)"
                + "(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={2})));"
                + "User Id={3};Password={4};"
                    , connectionData.Server, connectionData.Port, connectionData.Db, connectionData.User, connectionData.Password);
                break;
            case "IBM DB2":
                connection = new DB2Connection();
                connection.ConnectionString = String.Format("Server={0}:{1};Database={2};UID={3};PWD={4};"
                    , connectionData.Server, connectionData.Port, connectionData.Db, connectionData.User, connectionData.Password);
                break;
            case "Firebird":
                connection = new FbConnection();
                //connection.ConnectionString = String.Format("User={3};Password={4};Database={2};DataSource={0}; Port={1};Dialect=3; Charset=NONE;Role=;Connection lifetime=15;Pooling=true; MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;"
                //    , connectionData.Server, connectionData.Port, connectionData.Db, connectionData.User, connectionData.Password);
                connection.ConnectionString = String.Format("User={3};Password={4};Database={2};DataSource={0}; Port={1};"
                    , connectionData.Server, connectionData.Port, connectionData.Db, connectionData.User, connectionData.Password);
                break;
        }

        if (connection != null)
        {
            try
            {
                connection.Open();
                connection.Close();
            }
            catch
            {
                if (connection != null)
                {
                    connection.Close();
                }
                connection = null;

                throw;
            }
        }

        return connection;
    }

    public DbDataAdapter GetDataAdapter(String type)
    {
        DbDataAdapter dataAdapter = null;

        switch (type)
        {
            case "PostgreSQL":
                dataAdapter = new NpgsqlDataAdapter();
                break;
            case "MySQL":
                dataAdapter = new MySqlDataAdapter();
                break;
            case "Microsoft SQL Server":
                dataAdapter = new SqlDataAdapter();
                break;
            case "Oracle":
                dataAdapter = new OracleDataAdapter();
                break;
            case "IBM DB2":
                dataAdapter = new DB2DataAdapter();
                break;
            case "Firebird":
                dataAdapter = new FbDataAdapter();
                break;
        }

        return dataAdapter;
    }
}