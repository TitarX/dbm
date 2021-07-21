using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Data;
using System.Text;
using ExcelLibrary;
using System.IO;

public partial class Sections_Manager_DB_Tables_OpenTable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        infoPanel.Controls.Clear();
        headPanel.Controls.Clear();
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
                if (Session["tableToOpen"] == null)
                {
                    infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Недостаточно параметров</span>"));
                }
                else
                {
                    String tableToOpen = Session["tableToOpen"].ToString().Trim();
                    try
                    {
                        connection.Open();
                        DbCommand command = connection.CreateCommand();
                        if (connectionData.Type.Equals("PostgreSQL"))
                        {
                            command.CommandText = String.Format("SELECT * FROM \"{0}\"", tableToOpen);
                        }
                        else
                        {
                            command.CommandText = String.Format("SELECT * FROM {0}", tableToOpen);
                        }
                        DataSet dataSet = new DataSet();
                        DbDataAdapter dataAdapter = connectionManager.GetDataAdapter(connectionData.Type);
                        dataAdapter.SelectCommand = command;
                        if (dataAdapter == null)
                        {
                            infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Не удалось получить таблицу</span>"));
                        }
                        else
                        {
                            headPanel.Controls.Add(new LiteralControl(String.Format("<h2>Таблица \"{0}\"</h2>", Server.HtmlEncode(tableToOpen))));
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
                if (Session["tableToOpen"] == null)
                {
                    infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Недостаточно параметров</span>"));
                }
                else
                {
                    String tableToOpen = Session["tableToOpen"].ToString().Trim();
                    try
                    {
                        connection.Open();
                        DbCommand command = connection.CreateCommand();
                        if (connectionData.Type.Equals("PostgreSQL"))
                        {
                            command.CommandText = String.Format("SELECT * FROM \"{0}\"", tableToOpen);
                        }
                        else
                        {
                            command.CommandText = String.Format("SELECT * FROM {0}", tableToOpen);
                        }
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
                                    Response.AddHeader("content-disposition", String.Format("attachment;filename={0}.xls", tableToOpen.Trim()));

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
                                Response.AddHeader("content-disposition", String.Format("attachment;filename={0}.csv", tableToOpen.Trim()));
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