using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class GlobalError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Application["GlobalError"] == null)
        {
            if (Request.UrlReferrer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
        }
        else
        {
            StringBuilder errorMessageBuilder = new StringBuilder();
            errorMessageBuilder.Append(String.Format("<div class='globalError'>{0}</div>"
                , Application["GlobalError"].ToString().Trim()));
            if (Request.UrlReferrer != null)
            {
                errorMessageBuilder.Append("<div class='filler'></div>");
                errorMessageBuilder.Append(String.Format("<div style='text-align:center;'><a href='{0}'>Вернуться на предыдущую страницу</a></div>"
                    , Request.UrlReferrer.ToString()));
            }
            globalErrorMessagePane.Controls.Add(new LiteralControl(errorMessageBuilder.ToString()));
            Application["GlobalError"] = null;
        }
    }
}