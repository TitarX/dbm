using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Temp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void createRoleButton_Click(object sender, EventArgs e)
    {
        try
        {
            String roleName = roleTextBox.Text.Trim();
            Roles.CreateRole(roleName);
            infoPanel.Controls.Add(new LiteralControl(String.Format("<font color='#008000'>Роль {0} успешно создана</font>", roleName)));
        }
        catch (Exception ex)
        {
            infoPanel.Controls.Add(new LiteralControl(String.Format("<font color='#FF0000'>{0}</font>", ex.Message)));
            //infoPanel.Controls.Add(new LiteralControl(Roles.Enabled.ToString()));
        }
    }
    protected void deleteRoleButton_Click(object sender, EventArgs e)
    {
        try
        {
            String roleName = roleTextBox.Text.Trim();
            Roles.DeleteRole(roleTextBox.Text.Trim());
            infoPanel.Controls.Add(new LiteralControl(String.Format("<font color='#008000'>Роль {0} успешно удалена</font>", roleName)));
        }
        catch (Exception ex)
        {
            infoPanel.Controls.Add(new LiteralControl(String.Format("<font color='#FF0000'>{0}</font>", ex.Message)));
        }
    }
    protected void addUserToRoleButton_Click(object sender, EventArgs e)
    {
        try
        {
            String userName = userTextBox.Text.Trim();
            String roleName = roleTextBox.Text.Trim();
            Roles.AddUserToRole(userName, roleName);
            infoPanel.Controls.Add(new LiteralControl(String.Format("<font color='#008000'>Пользователь {0} успешно добавлен к роли {1}</font>", userName, roleName)));
        }
        catch (Exception ex)
        {
            infoPanel.Controls.Add(new LiteralControl(String.Format("<font color='#FF0000'>{0}</font>", ex.Message)));
        }
    }
    protected void deleteUserFromRoleButton_Click(object sender, EventArgs e)
    {
        try
        {
            String userName = userTextBox.Text.Trim();
            String roleName = roleTextBox.Text.Trim();
            Roles.RemoveUserFromRole(userName, roleName);
            infoPanel.Controls.Add(new LiteralControl(String.Format("<font color='#008000'>Пользователь {0} успешно удалён из роли {1}</font>", userName, roleName)));
        }
        catch (Exception ex)
        {
            infoPanel.Controls.Add(new LiteralControl(String.Format("<font color='#FF0000'>{0}</font>", ex.Message)));
        }
    }
    protected void deleteUserButton_Click(object sender, EventArgs e)
    {
        try
        {
            String user = userTextBox.Text.Trim();

            if (Membership.DeleteUser(user, true))
            {
                infoPanel.Controls.Add(new LiteralControl(String.Format("<font color='#008000'>Пользователь {0} успешно удалён</font>", user)));
            }
            else
            {
                infoPanel.Controls.Add(new LiteralControl(String.Format("<font color='#FF0000'>Не удалось удалить пользователя {0}</font>", user)));
            }
        }
        catch (Exception ex)
        {
            infoPanel.Controls.Add(new LiteralControl(String.Format("<font color='#FF0000'>{0}</font>", ex.Message)));
        }
    }
}