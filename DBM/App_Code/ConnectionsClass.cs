using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

/// <summary>
/// Summary description for ConnectionsClass
/// </summary>
[Serializable()]
public class ConnectionsClass
{
    private Dictionary<String, ConnectionData> connections = null;

    public ConnectionsClass() { }

    public Dictionary<String, ConnectionData> Connections
    {
        get
        {
            return connections;
        }
        set
        {
            connections = value;
        }
    }
}