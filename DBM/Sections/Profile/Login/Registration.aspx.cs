using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sections_Profile_Login_Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CreateUserWizard_ContinueButtonClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Sections/Profile/My/Profile.aspx");
    }
}