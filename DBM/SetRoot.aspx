<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetRoot.aspx.cs" Inherits="Temp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="keywords" content="" />
    <meta name="description" content="Set root" />
    <title>Set root</title>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div id="content">
        <div class="filler">
        </div>
        <table align="center">
            <tr>
                <td>
                    <asp:Label ID="userLabel" runat="server" Text="Пользователь:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="userTextBox" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="roleLabel" runat="server" Text="Роль:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="roleTextBox" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="deleteUserButton" runat="server" Text="Удалить пользователя" 
                        OnClick="deleteUserButton_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="createRoleButton" runat="server" Text="Создать роль" 
                        OnClick="createRoleButton_Click" />
                    <asp:Button ID="deleteRoleButton" runat="server" Text="Удалить роль" 
                        OnClick="deleteRoleButton_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="addUserToRoleButton" runat="server" 
                        Text="Добавить пользователя к роли" OnClick="addUserToRoleButton_Click" />
                    <asp:Button ID="deleteUserFromRoleButton" runat="server" Text="Удалить пользователя из роли"
                        OnClick="deleteUserFromRoleButton_Click" />
                </td>
            </tr>
        </table>
        <asp:Panel ID="infoPanel" runat="server">
        </asp:Panel>
        <div class="filler">
        </div>
    </div>
    </form>
</body>
</html>
