using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sections_Connection_Drop : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Connection"] != null)
        {
            Session["Connection"] = null;
        }
        Response.Redirect("~/Sections/Manager/Connections/Create.aspx");
    }
}