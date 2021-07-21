using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Data.Common;

public partial class Sections_Connection_Create : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        infoPanel.Controls.Clear();
        if (!Page.IsPostBack)
        {
            serverTextBox.Text = Request.UserHostAddress;
        }
    }
    protected void resetButton_Click(object sender, EventArgs e)
    {
        Server.Transfer(Request.FilePath);
    }
    protected void sendButton_Click(object sender, EventArgs e)
    {
        Page.Validate();
        StringBuilder errorMessageBuilder = new StringBuilder();
        bool isValid = true;
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
        String connectionName = "";
        if (Request.Params["saveCheckBox"] != null)
        {
            if (Request.Params["connectionName"] == null || Request.Params["connectionName"].Trim().Equals(""))
            {
                isValid = false;
                errorMessageBuilder.Append("<br /><span class='error'>Поле \"Название\" не заполнено</span>");
            }
            else
            {
                connectionName = Request.Params["connectionName"].Trim();
                Dictionary<String, ConnectionData> connections = Profile.ConnectionsClass.Connections;
                if (connections == null)
                {
                    connections = new Dictionary<String, ConnectionData>();
                }
                if (connections.ContainsKey(connectionName))
                {
                    isValid = false;
                    errorMessageBuilder.Append("<br /><span class='error'>У вас уже есть подключение с таким названием</span>");
                }
            }
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
                infoPanel.Controls.Add(new LiteralControl(String.Format("<br /><span class='error'>Не удалось подключиться к базе данных</span><br /><span class='error'>{0}</span>", ex.Message)));
            }

            if (connection != null)
            {
                Session["Connection"] = connectionData;

                if (!connectionName.Equals(""))
                {
                    connectionData.Name = connectionName;
                    Dictionary<String, ConnectionData> connections = Profile.ConnectionsClass.Connections;
                    if (connections == null)
                    {
                        connections = new Dictionary<String, ConnectionData>();
                    }
                    connections.Add(connectionName, connectionData);
                    Profile.ConnectionsClass.Connections = connections;
                    Profile.Save();
                }

                Response.Redirect("~/Sections/Manager/Queries/Create.aspx");
            }
        }
        else
        {
            infoPanel.Controls.Add(new LiteralControl(errorMessageBuilder.ToString()));
        }
    }
}