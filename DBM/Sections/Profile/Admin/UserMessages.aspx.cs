using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Xml.Linq;

public partial class Sections_Profile_Admin_UserMessages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        infoPanel.Controls.Clear();

        StringBuilder messagesTableBuilder = new StringBuilder();
        messagesTableBuilder.Append("<table id='messagesTable' width='100%' align='center'>");
        messagesTableBuilder.Append("<thead>");
        messagesTableBuilder.Append("<tr>");
        messagesTableBuilder.Append("<th>Имя пользователя</th>");
        messagesTableBuilder.Append("<th>Тема</th>");
        messagesTableBuilder.Append("<th>Дата сообщения</th>");
        messagesTableBuilder.Append("</tr>");
        messagesTableBuilder.Append("</thead>");
        messagesTableBuilder.Append("<tbody>");

        SqlConnection connection = null;
        try
        {
            String connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();

            String selectString = "SELECT MessageID,SubjectText,MessageDate,UserID FROM UserMessages";
            SqlCommand selectCommand = new SqlCommand(selectString, connection);
            SqlDataReader dataReader = selectCommand.ExecuteReader();
            while (dataReader.Read())
            {
                int messageID = dataReader.GetInt32(0);
                String subject = dataReader.GetString(1);
                String messageDate = dataReader.GetDateTime(2).ToString("yyyy.MM.dd");
                System.Guid userID = dataReader.GetGuid(3);

                MembershipUser user = Membership.GetUser(userID);
                String userName = user.UserName;
                String email = user.Email;

                messagesTableBuilder.Append(String.Format("<tr class='rowMessagesTable' onclick='setMessageValues({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\");'>"
                    , messageID, Corrector.ArgumentHtmlEncoding(userName), Corrector.ArgumentHtmlEncoding(email), Corrector.ArgumentHtmlEncoding(subject), messageDate));
                messagesTableBuilder.Append("<td>");
                messagesTableBuilder.Append(Server.HtmlEncode(userName));
                messagesTableBuilder.Append("</td>");
                messagesTableBuilder.Append("<td>");
                messagesTableBuilder.Append(Server.HtmlEncode(subject));
                messagesTableBuilder.Append("</td>");
                messagesTableBuilder.Append("<td>");
                messagesTableBuilder.Append(messageDate);
                messagesTableBuilder.Append("</td>");
                messagesTableBuilder.Append("</tr>");
            }
        }
        catch
        {
            infoPanel.Controls.Clear();
            infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Не удалось получить сообщения, повторите позже</span>"));
        }
        finally
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        messagesTableBuilder.Append("</tbody>");
        messagesTableBuilder.Append("</table>");

        messagesPanel.Controls.Clear();
        messagesPanel.Controls.Add(new LiteralControl(messagesTableBuilder.ToString()));
    }

    [WebMethod]
    public static String GetMessage(int id)
    {
        SqlConnection connection = null;
        try
        {
            String connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();

            String selectString = "SELECT MessageText FROM UserMessages WHERE MessageID=@MessageID";
            SqlCommand selectCommand = new SqlCommand(selectString, connection);
            selectCommand.Parameters.Add("@MessageID", SqlDbType.Int);
            selectCommand.Parameters["@MessageID"].Value = id;
            String message = selectCommand.ExecuteScalar().ToString();

            String deleteString = "DELETE FROM UserMessages WHERE MessageID=@MessageID";
            SqlCommand deleteCommand = new SqlCommand(deleteString, connection);
            deleteCommand.Parameters.Add("@MessageID", SqlDbType.Int);
            deleteCommand.Parameters["@MessageID"].Value = id;
            deleteCommand.ExecuteNonQuery();

            return message;
        }
        catch
        {
            return "<span style='color:#FF0000;'>Произошла ошибка, повторите позже</span>";
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