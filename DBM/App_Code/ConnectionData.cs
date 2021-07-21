using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

/// <summary>
/// Summary description for ConnectionData
/// </summary>
[Serializable()]
public class ConnectionData
{
    private String type = "";
    private String server = "";
    private String ip = "";
    private String port = "";
    private String db = "";
    private String user = "";
    private String password = "";
    private String name = "";

    public ConnectionData() { }

    public String Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }
    public String Server
    {
        get
        {
            return server;
        }
        set
        {
            server = value;
        }
    }
    public String Ip
    {
        get
        {
            return ip;
        }
        set
        {
            ip = value;
        }
    }
    public String Port
    {
        get
        {
            return port;
        }
        set
        {
            port = value;
        }
    }
    public String Db
    {
        get
        {
            return db;
        }
        set
        {
            db = value;
        }
    }
    public String User
    {
        get
        {
            return user;
        }
        set
        {
            user = value;
        }
    }
    public String Password
    {
        get
        {
            return password;
        }
        set
        {
            password = value;
        }
    }
    public String Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
}