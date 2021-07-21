<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="MessageToRoot.aspx.cs" Inherits="Sections_Profile_Admin_MessageToRoot" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <link href="/CSS/Sections/Contacts.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/JS/Sections/Contacts/Feedback.js"></script>
    <meta name="keywords" content="отправить сообщение главному администратору" />
    <meta name="description" content="Отправить сообщение главному администратору" />
    <title>Отправить сообщение главному администратору</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="filler">
        </div>
        <div>
            |<a href="Profile.aspx">Мой профиль</a>| |<a href="MessageToRoot.aspx"><span class="thispage">Отправить
                сообщение главному администратору</span></a>|
        </div>
        <div class="filler">
        </div>
        <div class="warning">
            Все поля обязательны для заполнения
        </div>
        <asp:Panel ID="infoPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <table align="center" id="contactsWrapper">
            <tr>
                <th colspan="2" id="contactsHeader">
                    Отправить сообщение главному администратору
                </th>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Тема:
                </td>
                <td>
                    <asp:TextBox ID="subjectTextBox" runat="server" Size="50" MaxLength="50" onfocus="setBackgroundColor(this);"
                        onblur="removeBackgroundColor(this);"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="subjectRequiredFieldValidator" runat="server" ErrorMessage=""
                        ControlToValidate="subjectTextBox" EnableClientScript="False"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Сообщение:
                </td>
                <td>
                    <asp:TextBox ID="messageTextBox" runat="server" TextMode="MultiLine" Columns="50"
                        MaxLength="500" Rows="15" onfocus="setBackgroundColor(this);" onblur="removeBackgroundColor(this);"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="messageRequiredFieldValidator" runat="server" ErrorMessage=""
                        ControlToValidate="messageTextBox" EnableClientScript="False"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="resetButton" runat="server" Text="Сбросить" OnClick="resetButton_Click" />
                </td>
                <td class="toRight">
                    <asp:Button ID="sendButton" runat="server" Text="Отправить" OnClick="sendButton_Click" />
                </td>
            </tr>
        </table>
        <div class="filler">
        </div>
    </div>
</asp:Content>
