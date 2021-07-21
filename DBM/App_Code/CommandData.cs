using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

/// <summary>
/// Summary description for CommandData
/// </summary>
[Serializable()]
public class CommandData
{
    private String query = "";
    private String type = "";
    private String name = "";

	public CommandData(){}

    public String Query
    {
        get
        {
            return query;
        }
        set
        {
            query = value;
        }
    }
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