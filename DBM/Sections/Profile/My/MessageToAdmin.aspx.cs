using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Sections_Profile_My_MessageToAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        infoPanel.Controls.Clear();
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
        if (!subjectRequiredFieldValidator.IsValid)
        {
            isValid = false;
            errorMessageBuilder.Append("<br /><span class='error'>Поле \"Тема\" не заполнено</span>");
        }
        if (!messageRequiredFieldValidator.IsValid)
        {
            isValid = false;
            errorMessageBuilder.Append("<br /><span class='error'>Поле \"Сообщение\" не заполнено</span>");
        }

        if (isValid)
        {
            String subject = subjectTextBox.Text.Trim();
            String message = messageTextBox.Text.Trim();
            DateTime date = DateTime.Now;
            System.Guid id = (System.Guid)Membership.GetUser().ProviderUserKey;
            SqlConnection connection = null;
            try
            {
                String connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString;
                connection = new SqlConnection(connectionString);
                connection.Open();

                String insertCommandString = "INSERT INTO UserMessages(SubjectText,MessageText,MessageDate,UserID)"
                    + "VALUES(@SubjectText,@MessageText,@MessageDate,@UserID)";
                SqlCommand insertCommand = new SqlCommand(insertCommandString, connection);
                insertCommand.Parameters.Add("@SubjectText", SqlDbType.NVarChar, 50);
                insertCommand.Parameters.Add("@MessageText", SqlDbType.NVarChar, 500);
                insertCommand.Parameters.Add("@MessageDate", SqlDbType.DateTime);
                insertCommand.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier);
                insertCommand.Parameters["@SubjectText"].Value = subject;
                insertCommand.Parameters["@MessageText"].Value = message;
                insertCommand.Parameters["@MessageDate"].Value = date;
                insertCommand.Parameters["@UserID"].Value = id;
                int insertResult = insertCommand.ExecuteNonQuery();
                if (insertResult > 0)
                {
                    infoPanel.Controls.Clear();
                    infoPanel.Controls.Add(new LiteralControl("<br /><span class='info'>Сообщение успешно отправлено</span>"));
                }
                else
                {
                    infoPanel.Controls.Clear();
                    infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Не удалось отправить сообщение, повторите позже</span>"));
                }
            }
            catch
            {
                infoPanel.Controls.Clear();
                infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Не удалось отправить сообщение, повторите позже</span>"));
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
            infoPanel.Controls.Clear();
            infoPanel.Controls.Add(new LiteralControl(errorMessageBuilder.ToString()));
        }
    }
}