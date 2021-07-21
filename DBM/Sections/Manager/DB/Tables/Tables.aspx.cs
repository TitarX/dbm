using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.Common;

public partial class Sections_DB_Tables_Tables : System.Web.UI.Page
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
        connectionsTableBuilder.Append("<input type='hidden' id='tableName' name='tableName' value='' />");
        connectionsTableBuilder.Append("<table id='objectsTable' width='100%' align='center'>");
        connectionsTableBuilder.Append("<thead>");
        connectionsTableBuilder.Append("<tr>");
        connectionsTableBuilder.Append("<th>Имя таблицы</th>");
        connectionsTableBuilder.Append("<th>Открыть</th>");
        connectionsTableBuilder.Append("<th>Удалить</th>");
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
                infoPanel.Controls.Add(new LiteralControl(String.Format("<br /><span class='error'>Не удалось подключиться к базе данных</span><br /><span class='error'>{0}</span>", ex.Message)));
            }

            if (connection != null)
            {
                try
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    switch (connectionData.Type)
                    {
                        case "PostgreSQL":
                            command.CommandText = "SELECT tablename FROM pg_tables WHERE schemaname <> all (ARRAY['pg_catalog'::name, 'information_schema'::name])";
                            break;
                        case "MySQL":
                            command.CommandText = "SHOW TABLES";
                            break;
                        case "Microsoft SQL Server":
                            command.CommandText = "SELECT name FROM sys.tables WHERE type_desc = 'USER_TABLE'";
                            break;
                        case "Oracle":
                            command.CommandText = "SELECT TABLE_NAME FROM USER_TABLES";
                            break;
                        case "IBM DB2":
                            command.CommandText = "SELECT TABLE_NAME FROM sysibm.TABLES WHERE TABLE_SCHEMA NOT LIKE 'SYS%'";
                            break;
                        case "Firebird":
                            command.CommandText = "SELECT RDB$RELATION_NAME FROM RDB$RELATIONS WHERE (RDB$SYSTEM_FLAG = 0)";
                            break;
                    }
                    DbDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        String tableName = Server.HtmlEncode(dataReader.GetString(0));
                        String tableNameForArgument = Corrector.ArgumentHtmlEncoding(dataReader.GetString(0));
                        connectionsTableBuilder.Append("<tr>");
                        connectionsTableBuilder.Append("<td>");
                        connectionsTableBuilder.Append(tableName);
                        connectionsTableBuilder.Append("</td>");
                        connectionsTableBuilder.Append("<td>");
                        connectionsTableBuilder.Append("<input type='button' id='" + tableName + "OpenButton' name='" + tableName + "OpenButton' value='Открыть' onclick='openTable(\"" + tableNameForArgument + "\");' />");
                        connectionsTableBuilder.Append("</td>");
                        connectionsTableBuilder.Append("<td>");
                        connectionsTableBuilder.Append("<input type='button' id='" + tableName + "DropButton' name='" + tableName + "DropButton' value='Удалить' onclick='dropTable(\"" + tableNameForArgument + "\");' />");
                        connectionsTableBuilder.Append("</td>");
                        connectionsTableBuilder.Append("</tr>");
                    }
                }
                catch
                {
                    infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Не удалось получить список таблиц</span>"));
                }
                finally
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                }
            }
        }

        connectionsTableBuilder.Append("</tbody>");
        connectionsTableBuilder.Append("</table>");

        tablesPanel.Controls.Clear();
        tablesPanel.Controls.Add(new LiteralControl(connectionsTableBuilder.ToString()));
    }

    protected void dropTableProcessButton_Click(object sender, EventArgs e)
    {
        String tableName = "";
        bool isValid = true;
        if (Session["Connection"] == null)
        {
            infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Соединение не установлено</span>"));
            isValid = false;
        }
        else
        {
            ConnectionData connectionData = (ConnectionData)Session["Connection"];
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
                infoPanel.Controls.Add(new LiteralControl(String.Format("<br /><span class='error'>Не удалось подключиться к базе данных</span><br /><span class='error'>{0}</span>", ex.Message)));
            }

            if (connection != null)
            {
                try
                {
                    tableName = Request.Params["tableName"];
                    if (tableName == null || tableName.Equals(""))
                    {
                        infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Недостаточно параметров</span>"));
                        isValid = false;
                    }
                    else
                    {
                        connection.Open();
                        DbCommand command = connection.CreateCommand();
                        command.CommandText = String.Format("DROP TABLE {0}", tableName);
                        command.ExecuteNonQuery();
                    }
                }
                catch
                {
                    infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Не удалось удалить таблицу</span>"));
                    isValid = false;
                }
                finally
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                }

                if (isValid)
                {
                    Server.Transfer(Request.FilePath);
                }
            }
        }
    }

    protected void openTableProcessButton_Click(object sender, EventArgs e)
    {
        String tableToOpen = Request.Params["tableName"];
        if (tableToOpen == null || tableToOpen.Equals(""))
        {
            infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Недостаточно параметров</span>"));
        }
        else
        {
            Session["tableToOpen"] = tableToOpen.Trim();
            Response.Redirect("OpenTable.aspx");
        }
    }
}