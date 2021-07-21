using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

/// <summary>
/// Summary description for CommandClass
/// </summary>
[Serializable()]
public class CommandsClass
{
    private Dictionary<String, Dictionary<String, CommandData>> commands = null;

    public CommandsClass() { }

    public Dictionary<String, Dictionary<String, CommandData>> Commands
    {
        get
        {
            return commands;
        }
        set
        {
            commands = value;
        }
    }
}