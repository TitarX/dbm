using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class SqlTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection connection = null;
        try
        {
            String connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();

            String commandString = "SELECT * FROM AdminMessages WHERE MessageID=1";
            SqlCommand command = new SqlCommand(commandString, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                infoPanel.Controls.Clear();
                infoPanel.Controls.Add(new LiteralControl(dataReader.GetInt32(0).ToString()));
            }
        }
        catch(Exception ex)
        {
            infoPanel.Controls.Clear();
            infoPanel.Controls.Add(new LiteralControl(ex.Message));
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