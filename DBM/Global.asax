<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        if (ex is HttpRequestValidationException)
        {
            Application["GlobalError"] = "Обнаружено потенциально опасное значение";
            Response.Redirect("~/GlobalError.aspx");
        }
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        if (User != null)
        {
            if (User.Identity.IsAuthenticated && Roles.Enabled)
            {
                if (!Roles.IsUserInRole("User") && Roles.RoleExists("User"))
                {
                    Roles.AddUserToRole(User.Identity.Name, "User");
                }
            }
        }
    }
       
</script>
