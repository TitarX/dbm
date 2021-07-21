using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.Common;

public partial class Sections_Connections_My_Connections : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        infoPanel.Controls.Clear();

        StringBuilder connectionsTableBuilder = new StringBuilder();
        connectionsTableBuilder.Append("<table id='connectionsTable' width='100%' align='center'>");
        connectionsTableBuilder.Append("<thead>");
        connectionsTableBuilder.Append("<tr>");
        connectionsTableBuilder.Append("<th>Название</th>");
        connectionsTableBuilder.Append("<th>Тип</th>");
        connectionsTableBuilder.Append("</tr>");
        connectionsTableBuilder.Append("</thead>");
        connectionsTableBuilder.Append("<tbody>");

        Dictionary<String, ConnectionData> connections = Profile.ConnectionsClass.Connections;
        if (connections != null)
        {
            int i = 0;
            foreach (String name in connections.Keys)
            {
                ++i;
                connectionsTableBuilder.Append(String.Format("<tr class='rowConnectionsTable' onclick='setConnectionValues(this,\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\");'>"
                    , Corrector.ArgumentHtmlEncoding(connections[name].Server), connections[name].Port, Corrector.ArgumentHtmlEncoding(connections[name].Db)
                    , Corrector.ArgumentHtmlEncoding(connections[name].User), Corrector.ArgumentHtmlEncoding(connections[name].Password), i));
                connectionsTableBuilder.Append("<td>");
                connectionsTableBuilder.Append(Server.HtmlEncode(name));
                connectionsTableBuilder.Append(String.Format("<input type='hidden' id='nameHidden{0}' value='{1}'>", i, Server.HtmlEncode(name)));
                connectionsTableBuilder.Append("</td>");
                connectionsTableBuilder.Append("<td>");
                connectionsTableBuilder.Append(connections[name].Type);
                connectionsTableBuilder.Append(String.Format("<input type='hidden' id='typeHidden{0}' value='{1}'>", i, connections[name].Type));
                connectionsTableBuilder.Append("</td>");
                connectionsTableBuilder.Append("</tr>");
            }
        }

        connectionsTableBuilder.Append("</tbody>");
        connectionsTableBuilder.Append("</table>");

        connectionsPanel.Controls.Clear();
        connectionsPanel.Controls.Add(new LiteralControl(connectionsTableBuilder.ToString()));
    }

    protected void button_Click(object sender, EventArgs e)
    {
        String oldName = Request.Params["oldName"].Trim();
        if (oldName == null || oldName.Equals(""))
        {
            infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Не указано название соединения</span>"));
        }
        else
        {
            if (sender.Equals(deleteConnectionServerButton))
            {
                Dictionary<String, ConnectionData> connections = Profile.ConnectionsClass.Connections;
                if (connections == null || !connections.ContainsKey(oldName))
                {
                    infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Соединение не найдено</span>"));
                }
                else
                {
                    Dictionary<String, Dictionary<String, CommandData>> commands = Profile.CommandsClass.Commands;
                    if (commands != null && commands.ContainsKey(oldName))
                    {
                        commands.Remove(oldName);
                        Profile.CommandsClass.Commands = commands;
                    }
                    connections.Remove(oldName);
                    Profile.ConnectionsClass.Connections = connections;
                    Profile.Save();
                    Server.Transfer(Request.FilePath);
                }
            }
            else
            {
                Page.Validate();
                StringBuilder errorMessageBuilder = new StringBuilder();
                bool isValid = true;
                if (sender.Equals(saveServerButton))
                {
                    if (!newNameRequiredFieldValidator.IsValid)
                    {
                        isValid = false;
                        errorMessageBuilder.Append("<br /><span class='error'>Поле \"Название\" не заполнено</span>");
                    }
                }
                if (!serverRequiredFieldValidator.IsValid)
                {
                    isValid = false;
                    errorMessageBuilder.Append("<br /><span class='error'>Поле \"Сервер\" не заполнено</span>");
                }
                if (!portRequiredFieldValidator.IsValid)
                {
                    isValid = false;
                    errorMessageBuilder.Append("<br /><span class='error'>Поле \"Порт\" не заполнено</span>");
                }
                if (!dbRequiredFieldValidator.IsValid)
                {
                    isValid = false;
                    switch (typeDropDownList.SelectedValue)
                    {
                        case "Oracle":
                            errorMessageBuilder.Append("<br /><span class='error'>Поле \"SID / Сервис\" не заполнено</span>");
                            break;
                        case "Firebird":
                            errorMessageBuilder.Append("<br /><span class='error'>Поле \"БД / Путь\" не заполнено</span>");
                            break;
                        default:
                            errorMessageBuilder.Append("<br /><span class='error'>Поле \"БД\" не заполнено</span>");
                            break;
                    }
                }
                if (!userRequiredFieldValidator.IsValid)
                {
                    isValid = false;
                    errorMessageBuilder.Append("<br /><span class='error'>Поле \"Пользователь\" не заполнено</span>");
                }
                if (!passwordRequiredFieldValidator.IsValid)
                {
                    isValid = false;
                    errorMessageBuilder.Append("<br /><span class='error'>Поле \"Пароль\" не заполнено</span>");
                }

                if (isValid)
                {
                    ConnectionData connectionData = new ConnectionData();
                    connectionData.Type = typeDropDownList.SelectedValue;
                    connectionData.Server = serverTextBox.Text.Trim();
                    connectionData.Port = portTextBox.Text.Trim();
                    if (connectionData.Type.Equals("Firebird"))
                    {
                        connectionData.Db = Corrector.EscapeBackslash(dbTextBox.Text.Trim());
                    }
                    else
                    {
                        connectionData.Db = dbTextBox.Text.Trim();
                    }
                    connectionData.User = userTextBox.Text.Trim();
                    connectionData.Password = passwordTextBox.Text.Trim();

                    ConnectionManager connectionManager = new ConnectionManager();
                    DbConnection connection = null;
                    try
                    {
                        connection = connectionManager.TryConnection(connectionData);
                    }
                    catch (Exception ex)
                    {
                        connection = null;
                        Session["Connection"] = null;
                        infoPanel.Controls.Add(new LiteralControl(String.Format("<br /><span class='error'>Не удалось подключиться к базе данных.</span><br /><span class='error'>{0}</span>", ex.Message)));
                    }

                    if (connection != null)
                    {
                        if (sender.Equals(saveServerButton))
                        {
                            String newName = newNameTextBox.Text.Trim();

                            Dictionary<String, ConnectionData> connections = Profile.ConnectionsClass.Connections;
                            if (connections == null || !connections.ContainsKey(oldName))
                            {
                                infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Соединение не найдено</span>"));
                            }
                            else
                            {
                                if (newName.Equals(oldName))
                                {
                                    connections.Remove(oldName);
                                    connections.Add(oldName, connectionData);
                                    Profile.ConnectionsClass.Connections = connections;
                                    Profile.Save();
                                }
                                else
                                {
                                    connectionData.Name = newName;
                                    connections.Remove(oldName);
                                    connections.Add(newName, connectionData);

                                    Dictionary<String, Dictionary<String, CommandData>> allCommands = Profile.CommandsClass.Commands;
                                    if (allCommands != null && allCommands.ContainsKey(oldName))
                                    {
                                        Dictionary<String, CommandData> commandsOfThisConnection = allCommands[oldName];
                                        allCommands.Remove(oldName);
                                        allCommands.Add(newName, commandsOfThisConnection);
                                    }

                                    Profile.CommandsClass.Commands = allCommands;
                                    Profile.ConnectionsClass.Connections = connections;
                                    Profile.Save();
                                }
                                Server.Transfer(Request.FilePath);
                            }
                        }
                        else if (sender.Equals(connectServerButton))
                        {
                            connectionData.Name = oldName;
                            Session["Connection"] = connectionData;

                            Response.Redirect("~/Sections/Manager/Queries/Create.aspx");
                        }
                    }
                }
                else
                {
                    infoPanel.Controls.Add(new LiteralControl(errorMessageBuilder.ToString()));
                }
            }
        }
    }
}