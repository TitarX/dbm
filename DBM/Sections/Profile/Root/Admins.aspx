<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="Admins.aspx.cs" Inherits="Sections_Profile_Root_Admins" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="администраторы" />
    <meta name="description" content="Администраторы" />
    <title>Администраторы</title>
    <link href="/CSS/Sections/Profile/Users.css" rel="Stylesheet" type="text/css" />
    <link href="/CSS/jQuery/Tables/demo_table_jui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/JS/jQuery/Tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/JS/jQuery/SimpleModal/jquery.simplemodal-1.4.2.js"></script>
    <script type="text/javascript" src="/JS/Sections/Profile/Users.js"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="filler">
        </div>
        <div>
            |<a href="Admins.aspx"><span class="thispage">Администраторы</span></a>| |<a href="AdminMessages.aspx">Сообщения
                администраторов</a>|
        </div>
        <div class="filler">
        </div>
        <asp:Panel ID="infoPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <asp:Panel ID="adminsPanel" runat="server">
        </asp:Panel>
        <div id="basic-modal-content">
            <table width="100%">
                <tr>
                    <td colspan="2" align="center">
                        <span class="warning">Отмеченные поля обязательно должны быть заполнены</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <input type="button" id="unlockUserButton" name="unlockUserButton" value="Разблокировать пользователя"
                            onclick="unlockUser();" />
                        <div style="display: none">
                            <asp:Button ID="unlockUserServerButton" runat="server" Text="Button" OnClick="button_Click" />
                        </div>
                    </td>
                    <td>
                        <input type="button" id="completeDeleteButton" name="completeDeleteButton" value="Удалить пользователя и все его данные"
                            onclick="completeDelete($('#MainContentPlaceHolder_nameTextBox').val());" />
                        <div style="display: none">
                            <asp:Button ID="completeDeleteServerButton" runat="server" Text="Button" OnClick="button_Click" />
                        </div>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <input type="button" id="resetPasswordButton" name="resetPasswordButton" value="Сбросить пароль пользователя"
                            onclick="resetPassword();" />
                        <div style="display: none">
                            <asp:Button ID="resetPasswordServerButton" runat="server" Text="Button" OnClick="button_Click" />
                        </div>
                    </td>
                    <td>
                        <input type="button" id="incompleteDeleteButton" name="incompleteDeleteButton" value="Удалить пользователя, оставив его данные"
                            onclick="incompleteDelete($('#MainContentPlaceHolder_nameTextBox').val());" />
                        <div style="display: none">
                            <asp:Button ID="incompleteDeleteServerButton" runat="server" Text="Button" OnClick="button_Click" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr onmouseover="setBackgroundColor(this);" onmouseout="removeBackgroundColor(this);">
                    <td>
                        Имя:
                    </td>
                    <td>
                        <input type="text" id="name" name="name" value="" readonly="readonly" style="background-color: #DDDDDD;
                            width: 300px;" />
                        <input type="hidden" id="userName" name="userName" value="" />
                        <span class="warning">*</span>
                    </td>
                </tr>
                <tr onmouseover="setBackgroundColor(this);" onmouseout="removeBackgroundColor(this);">
                    <td>
                        Email:
                    </td>
                    <td>
                        <asp:TextBox ID="emailTextBox" runat="server" Width="300px"></asp:TextBox>
                        <span class="warning">*</span>
                        <asp:RequiredFieldValidator ID="emailRequiredFieldValidator" runat="server" ErrorMessage=""
                            EnableClientScript="False" ControlToValidate="emailTextBox"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr onmouseover="setBackgroundColor(this);" onmouseout="removeBackgroundColor(this);">
                    <td colspan="2">
                        <input type="checkbox" id="adminCheckbox" name="adminCheckbox" />Администратор
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <input type="button" id="resetButton" name="resetButton" value="Сбросить" onclick="resetUserValues();" />
                    </td>
                    <td align="right">
                        <input type="button" id="saveButton" name="saveButton" value="Сохранить" onclick="saveUserValues();" />
                        <div style="display: none">
                            <asp:Button ID="saveServerButton" runat="server" Text="Button" OnClick="saveButton_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id='confirm'>
            <!--
            <div class='header'>
                <span>Подтвердите удаление пользователя</span>
            </div>
            -->
            <div class='message'>
            </div>
            <div class='buttons'>
                <div class='no simplemodal-close'>
                    Нет</div>
                <div class='yes'>
                    Да</div>
            </div>
        </div>
        <div class="filler">
        </div>
    </div>
</asp:Content>
