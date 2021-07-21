using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Sections_Profile_My_Profile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        infoPanel.Controls.Clear();
        infoPanel.Controls.Add(new LiteralControl(String.Format("<br /><span class='info'>Вы вошли как {0}</span>", Server.HtmlEncode(Membership.GetUser().UserName))));
    }

    protected void ChangePassword_ContinueButtonClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Sections/Manager/Connections/MyConnections.aspx");
    }
}