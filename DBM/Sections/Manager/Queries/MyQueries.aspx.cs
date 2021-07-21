using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;

public partial class Sections_Queries_My_Queries : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        infoPanel.Controls.Clear();

        headConnectionPanel.Controls.Clear();
        if (Session["Connection"] != null)
        {
            String name = ((ConnectionData)Session["Connection"]).Name.Trim();
            if (name.Equals(""))
            {
                headConnectionPanel.Controls.Add(new LiteralControl(String.Format("<span class='info'>Подключен</span>")));
            }
            else
            {
                headConnectionPanel.Controls.Add(new LiteralControl(String.Format("<span class='info'>Подключен к \"{0}\"</span>", Server.HtmlEncode(name))));
            }
        }

        StringBuilder connectionsTableBuilder = new StringBuilder();
        connectionsTableBuilder.Append("<table id='queriesTable' width='100%' align='center'>");
        connectionsTableBuilder.Append("<thead>");
        connectionsTableBuilder.Append("<tr>");
        connectionsTableBuilder.Append("<th>Название</th>");
        connectionsTableBuilder.Append("<th>Тип</th>");
        connectionsTableBuilder.Append("</tr>");
        connectionsTableBuilder.Append("</thead>");
        connectionsTableBuilder.Append("<tbody>");

        if (Session["Connection"] == null)
        {
            infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Соединение не установлено</span>"));
        }
        else
        {
            ConnectionData connectionData = (ConnectionData)Session["Connection"];
            Dictionary<String, Dictionary<String, CommandData>> commandss = Profile.CommandsClass.Commands;
            if (commandss != null)
            {
                Dictionary<String, CommandData> commands = null;
                if (commandss.TryGetValue(connectionData.Name, out commands))
                {
                    if (commands != null)
                    {
                        int i = 0;
                        foreach (String name in commands.Keys)
                        {
                            ++i;
                            connectionsTableBuilder.Append(String.Format("<tr class='rowQueriesTable' onclick=\"setQueryValues(this,'{0}');\">", i));
                            connectionsTableBuilder.Append("<td>");
                            connectionsTableBuilder.Append(Server.HtmlEncode(name));
                            connectionsTableBuilder.Append(String.Format("<input type='hidden' id='nameHidden{0}' value='{1}'>", i, Server.HtmlEncode(name)));
                            connectionsTableBuilder.Append(String.Format("<input type='hidden' id='queryHidden{0}' value='{1}'>", i, Server.HtmlEncode(commands[name].Query)));
                            connectionsTableBuilder.Append("</td>");
                            connectionsTableBuilder.Append("<td>");
                            connectionsTableBuilder.Append(commands[name].Type);
                            connectionsTableBuilder.Append("</td>");
                            connectionsTableBuilder.Append("</tr>");
                        }
                    }
                }
            }
        }

        connectionsTableBuilder.Append("</tbody>");
        connectionsTableBuilder.Append("</table>");

        queriesPanel.Controls.Clear();
        queriesPanel.Controls.Add(new LiteralControl(connectionsTableBuilder.ToString()));
    }

    protected void button_Click(object sender, EventArgs e)
    {
        if (Session["Connection"] == null)
        {
            infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Соединение не установлено</span>"));
        }
        else
        {
            Page.Validate();
            bool isValid = true;
            StringBuilder errorMessageBuilder = new StringBuilder();
            if (sender.Equals(saveServerButton))
            {
                if (!newNameRequiredFieldValidator.IsValid)
                {
                    isValid = false;
                    errorMessageBuilder.Append("<br /><span class='error'>Поле \"Название\" не заполнено</span>");
                }
                if (!queryRequiredFieldValidator.IsValid)
                {
                    isValid = false;
                    errorMessageBuilder.Append("<br /><span class='error'>Поле запроса не заполнено</span>");
                }
            }

            if (isValid)
            {
                String oldName = Request.Params["oldName"].Trim();
                if (oldName == null || oldName.Equals(""))
                {
                    infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Не указано название запроса</span>"));
                }
                else
                {
                    ConnectionData connectionData = (ConnectionData)Session["Connection"];
                    Dictionary<String, CommandData> commands = Profile.CommandsClass.Commands[connectionData.Name];
                    if (commands == null || !commands.ContainsKey(oldName))
                    {
                        infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Запрос не найден</span>"));
                    }
                    else
                    {
                        if (sender.Equals(saveServerButton))
                        {
                            String query = queryTextBox.Text.Trim();
                            String type = new Regex("([a-zA-Z]+)").Match(query).Groups[1].Value.ToUpper();
                            String newName = newNameTextBox.Text.Trim();

                            CommandData commandData = commands[oldName];
                            commandData.Query = query;
                            commandData.Type = type;
                            if (!oldName.Equals(newName))
                            {
                                commandData.Name = newName;
                                commands.Remove(oldName);
                                commands.Add(newName, commandData);
                            }
                            else
                            {
                                commands[oldName] = commandData;
                            }
                        }
                        else if (sender.Equals(deleteQueryServerButton))
                        {
                            commands.Remove(oldName);
                        }

                        Profile.CommandsClass.Commands[connectionData.Name] = commands;
                        Profile.Save();
                        Server.Transfer(Request.FilePath);
                    }
                }
            }
            else
            {
                infoPanel.Controls.Add(new LiteralControl(errorMessageBuilder.ToString()));
            }
        }
    }

    protected void prepareServerButton_Click(object sender, EventArgs e)
    {
        if (Session["Connection"] == null)
        {
            infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Соединение не установлено</span>"));
        }
        else
        {
            Page.Validate();
            if (queryRequiredFieldValidator.IsValid)
            {
                Session["preparedQuery"] = queryTextBox.Text.Trim();
                Response.Redirect("Create.aspx");
            }
            else
            {
                infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Поле запроса не заполнено</span>"));
            }
        }
    }
}