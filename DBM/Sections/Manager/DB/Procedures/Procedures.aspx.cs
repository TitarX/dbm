using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.Common;

public partial class Sections_DB_Procedures_Procedures : System.Web.UI.Page
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
        connectionsTableBuilder.Append("<input type='hidden' id='procedureName' name='procedureName' value='' />");
        connectionsTableBuilder.Append("<table id='objectsTable' width='100%' align='center'>");
        connectionsTableBuilder.Append("<thead>");
        connectionsTableBuilder.Append("<tr>");
        connectionsTableBuilder.Append("<th>Имя процедуры</th>");
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
                            command.CommandText = "SELECT proname FROM pg_proc WHERE proowner IN (SELECT nspowner FROM pg_namespace WHERE (nspname NOT LIKE 'information_%') AND (nspname NOT LIKE 'pg_%'))";
                            break;
                        case "MySQL":
                            command.CommandText = "SHOW PROCEDURE STATUS";
                            break;
                        case "Microsoft SQL Server":
                            command.CommandText = "SELECT name FROM sys.procedures";
                            break;
                        case "Oracle":
                            command.CommandText = "SELECT OBJECT_NAME FROM USER_PROCEDURES";
                            break;
                        case "IBM DB2":
                            command.CommandText = "SELECT PROCEDURE_NAME FROM sysibm.SQLPROCEDURES WHERE (PROCEDURE_SCHEM NOT LIKE 'SYS%') AND (PROCEDURE_SCHEM NOT LIKE 'SQL%')";
                            break;
                        case "Firebird":
                            command.CommandText = "SELECT RDB$PROCEDURE_NAME FROM RDB$PROCEDURES WHERE (RDB$SYSTEM_FLAG = 0)";
                            break;
                    }
                    DbDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        String procedureName = null;
                        String procedureNameForAgrument = null;
                        if (connectionData.Type.Equals("MySQL"))
                        {
                            procedureName = Server.HtmlEncode(dataReader.GetString(1));
                            procedureNameForAgrument = Corrector.ArgumentHtmlEncoding(dataReader.GetString(1));
                        }
                        else
                        {
                            procedureName = Server.HtmlEncode(dataReader.GetString(0));
                            procedureNameForAgrument = Corrector.ArgumentHtmlEncoding(dataReader.GetString(0));
                        }
                        if (procedureName != null)
                        {
                            connectionsTableBuilder.Append("<tr>");
                            connectionsTableBuilder.Append("<td>");
                            connectionsTableBuilder.Append(procedureName);
                            connectionsTableBuilder.Append("</td>");
                            connectionsTableBuilder.Append("<td>");
                            connectionsTableBuilder.Append("<input type='button' id='" + procedureName + "DropButton' name='" + procedureName + "DropButton' value='Удалить' onclick='dropProcedure(\"" + procedureNameForAgrument + "\");' />");
                            connectionsTableBuilder.Append("</td>");
                            connectionsTableBuilder.Append("</tr>");
                        }
                    }
                }
                catch
                {
                    infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Не удалось получить список процедур</span>"));
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

        proceduresPanel.Controls.Clear();
        proceduresPanel.Controls.Add(new LiteralControl(connectionsTableBuilder.ToString()));
    }

    protected void dropProcedureProcessButton_Click(object sender, EventArgs e)
    {
        String procedureName = "";
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
                    procedureName = Request.Params["procedureName"];
                    if (procedureName == null || procedureName.Equals(""))
                    {
                        infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Недостаточно параметров</span>"));
                        isValid = false;
                    }
                    else
                    {
                        connection.Open();
                        DbCommand command = connection.CreateCommand();
                        if (connectionData.Type.Equals("PostgreSQL"))
                        {
                            command.CommandText = String.Format("DROP FUNCTION \"{0}\"()", procedureName);
                        }
                        else
                        {
                            command.CommandText = String.Format("DROP PROCEDURE {0}", procedureName);
                        }
                        command.ExecuteNonQuery();
                    }
                }
                catch
                {
                    infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Не удалось удалить процедуру</span>"));
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
}