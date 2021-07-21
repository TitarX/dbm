using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.Data;
using ExcelLibrary;
using System.IO;

public partial class Sections_Queries_Create : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        infoPanel.Controls.Clear();
        resultPanel.Controls.Clear();
        exportPanel.Controls.Clear();
        gridView.DataSource = null;
        gridView.DataBind();

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

        if (Session["preparedQuery"] != null)
        {
            queryTextBox.Text = Session["preparedQuery"].ToString().Trim();
            Session["preparedQuery"] = null;
        }
    }
    protected void resetButton_Click(object sender, EventArgs e)
    {
        Server.Transfer(Request.FilePath);
    }
    protected void button_Click(object sender, EventArgs e)
    {
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
                Page.Validate();
                if (sender.Equals(sendButton))
                {
                    StringBuilder errorMessageBuilder = new StringBuilder();
                    bool isValid = true;
                    if (!queryRequiredFieldValidator.IsValid)
                    {
                        isValid = false;
                        errorMessageBuilder.Append("<br /><span class='error'>Поле запроса не заполнено</span>");
                    }
                    String commandName = "";
                    Dictionary<String, Dictionary<String, CommandData>> allCommands = null;
                    Dictionary<String, CommandData> commandsOfThisConnection = null;
                    if (Request.Params["saveCheckBox"] != null)
                    {
                        if (Request.Params["commandName"] == null || Request.Params["commandName"].Trim().Equals(""))
                        {
                            isValid = false;
                            errorMessageBuilder.Append("<br /><span class='error'>Поле \"Название\" не заполнено</span>");
                        }
                        else
                        {
                            commandName = Request.Params["commandName"].Trim();
                            allCommands = Profile.CommandsClass.Commands;
                            if (allCommands == null)
                            {
                                allCommands = new Dictionary<String, Dictionary<String, CommandData>>();
                                commandsOfThisConnection = new Dictionary<String, CommandData>();
                            }
                            else
                            {
                                if (!Profile.CommandsClass.Commands.TryGetValue(connectionData.Name, out commandsOfThisConnection))
                                {
                                    commandsOfThisConnection = new Dictionary<String, CommandData>();
                                }
                                else
                                {
                                    if (commandsOfThisConnection.ContainsKey(commandName))
                                    {
                                        isValid = false;
                                        errorMessageBuilder.Append("<br /><span class='error'>У вас уже есть запрос с таким названием для текущего подключения</span>");
                                    }
                                }
                            }
                        }
                    }

                    if (isValid)
                    {
                        String query = queryTextBox.Text.Trim();
                        String type = new Regex("([a-zA-Z]+)").Match(query).Groups[1].Value.ToUpper();

                        try
                        {
                            connection.Open();
                            DbCommand command = connection.CreateCommand();
                            command.CommandText = query;
                            if (type.Equals("SELECT"))
                            {
                                DbDataAdapter dataAdapter = connectionManager.GetDataAdapter(connectionData.Type);
                                if (dataAdapter == null)
                                {
                                    infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Не удалось получить таблицу</span>"));
                                }
                                else
                                {
                                    dataAdapter.SelectCommand = command;
                                    DataSet dataSet = new DataSet();
                                    dataAdapter.Fill(dataSet);
                                    gridView.DataSource = dataSet;
                                    gridView.DataBind();

                                    StringBuilder exportButtonBuilder = new StringBuilder();
                                    exportButtonBuilder.Append("<input type='button' id='excelExportButton' name='excelExportButton' value='Экспортировать в Excel' onclick='excelExport();' />");
                                    exportButtonBuilder.Append("&nbsp;");
                                    exportButtonBuilder.Append("<input type='button' id='csvExportButton' name='csvExportButton' value='Экспортировать в CSV' onclick='csvExport();' />");
                                    exportPanel.Controls.Add(new LiteralControl(exportButtonBuilder.ToString()));
                                }
                            }
                            else
                            {
                                String result = command.ExecuteNonQuery().ToString();
                                resultPanel.Controls.Add(new LiteralControl(result));
                            }

                            if (!commandName.Equals(""))
                            {
                                CommandData commandData = new CommandData();
                                commandData.Query = query;
                                commandData.Type = type;
                                commandData.Name = commandName;
                                commandsOfThisConnection.Add(commandName, commandData);
                                allCommands.Remove(connectionData.Name);
                                allCommands.Add(connectionData.Name, commandsOfThisConnection);
                                Profile.CommandsClass.Commands = allCommands;
                                Profile.Save();
                            }
                        }
                        catch (Exception ex)
                        {
                            infoPanel.Controls.Add(new LiteralControl(String.Format("<br /><span class='error'>Не удалось выполнить запрос</span><br /><span class='error'>{0}</span>", ex.Message)));
                        }
                        finally
                        {
                            if (connection != null)
                            {
                                connection.Close();
                            }
                        }
                    }
                    else
                    {
                        infoPanel.Controls.Add(new LiteralControl(errorMessageBuilder.ToString()));
                    }
                }
                else
                {
                    if (!queryRequiredFieldValidator.IsValid)
                    {
                        infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Поле запроса не заполнено</span>"));
                    }
                    else
                    {
                        String query = queryTextBox.Text.Trim();
                        try
                        {
                            connection.Open();
                            DbCommand command = connection.CreateCommand();
                            command.CommandText = query;
                            DataSet dataSet = new DataSet();
                            DbDataAdapter dataAdapter = connectionManager.GetDataAdapter(connectionData.Type);
                            dataAdapter.SelectCommand = command;
                            if (dataAdapter == null)
                            {
                                infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Не удалось получить таблицу</span>"));
                            }
                            else
                            {
                                dataAdapter.Fill(dataSet);

                                if (sender.Equals(excelExportProcessButton))
                                {
                                    MemoryStream memoryStream = null;
                                    try
                                    {
                                        memoryStream = new MemoryStream();
                                        ExcelLibrary.DataSetHelper.CreateWorkbook(memoryStream, dataSet);

                                        Response.Clear();
                                        Response.ContentType = "application/vnd.ms2-excel";
                                        Response.AddHeader("content-disposition", "attachment;filename=Table.xls");

                                        memoryStream.WriteTo(Response.OutputStream);
                                        Response.End();
                                    }
                                    catch
                                    {
                                        infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Не удалось экспортировать таблицу</span>"));
                                    }
                                    finally
                                    {
                                        if (memoryStream != null)
                                        {
                                            memoryStream.Close();
                                        }
                                    }
                                }
                                else if (sender.Equals(csvExportProcessButton))
                                {

                                    String listSeparator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
                                    String newLine = Environment.NewLine;

                                    StringBuilder csvFileBuilder = new StringBuilder();
                                    DataTable dataTable = dataSet.Tables[0];
                                    String[] columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();
                                    csvFileBuilder.Append(String.Join(listSeparator, columnNames));
                                    foreach (DataRow dataRow in dataTable.Rows)
                                    {
                                        csvFileBuilder.Append(newLine);
                                        String[] fields = dataRow.ItemArray.Select(field => field.ToString()).ToArray();
                                        csvFileBuilder.Append(String.Join(listSeparator, fields));
                                    }

                                    Response.Clear();
                                    Response.ContentType = "application/csv";
                                    Response.AddHeader("content-disposition", "attachment;filename=Table.csv");
                                    Response.ContentEncoding = Encoding.GetEncoding("windows-1251");

                                    Response.Write(csvFileBuilder.ToString());
                                    Response.End();
                                }
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
            }
        }
    }
}