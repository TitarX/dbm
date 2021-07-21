using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_Main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void loginStatus_LoggedOut(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect("~/Default.aspx");
    }
}
