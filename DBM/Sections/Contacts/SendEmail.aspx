<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="SendEmail.aspx.cs" Inherits="Sections_Contacts_SendEmail" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="отправить email" />
    <meta name="description" content="Отправить email" />
    <title>Отправить email</title>
    <link href="/CSS/Sections/Contacts.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/JS/Sections/Contacts/SendEmail.js"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="filler">
        </div>
        <div class="warning">
            Поля "Кому" и "От кого" обязательны для заполнения
        </div>
        <asp:Panel ID="infoPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <table align="center" id="contactsWrapper">
            <tr>
                <th colspan="2" id="contactsHeader">
                    Отправить email
                </th>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Кому (Email):
                </td>
                <td>
                    <asp:TextBox ID="toTextBox" runat="server" Size="50" onfocus="setBackgroundColor(this);"
                        onblur="removeBackgroundColor(this);"></asp:TextBox>
                    <span class="warning">*</span>
                    <asp:RequiredFieldValidator ID="toRequiredFieldValidator" runat="server" ErrorMessage=""
                        ControlToValidate="toTextBox" ForeColor="Red" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="toRegularExpressionValidator" runat="server"
                        ErrorMessage="" ValidationExpression="^[_0-9a-zA-Zа-яА-Я][-._0-9a-zA-Zа-яА-Я]{0,29}[_0-9a-zA-Zа-яА-Я]@([0-9a-zA-Zа-яА-Я][-0-9a-zA-Zа-яА-Я]*[0-9a-zA-Zа-яА-Я]\.)+[a-zA-Zа-яА-Я]{2,8}$"
                        ControlToValidate="toTextBox" ForeColor="Red" EnableClientScript="False"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    От кого (Email):
                </td>
                <td>
                    <asp:TextBox ID="fromTextBox" runat="server" Size="50" onfocus="setBackgroundColor(this);"
                        onblur="removeBackgroundColor(this);"></asp:TextBox>
                    <span class="warning">*</span>
                    <asp:RequiredFieldValidator ID="fromRequiredFieldValidator" runat="server" ErrorMessage=""
                        ControlToValidate="fromTextBox" ForeColor="Red" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="fromRegularExpressionValidator" runat="server"
                        ErrorMessage="" ValidationExpression="^[_0-9a-zA-Zа-яА-Я][-._0-9a-zA-Zа-яА-Я]{0,29}[_0-9a-zA-Zа-яА-Я]@([0-9a-zA-Zа-яА-Я][-0-9a-zA-Zа-яА-Я]*[0-9a-zA-Zа-яА-Я]\.)+[a-zA-Zа-яА-Я]{2,8}$"
                        ControlToValidate="fromTextBox" ForeColor="Red" EnableClientScript="False"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Тема:
                </td>
                <td>
                    <asp:TextBox ID="subjectTextBox" runat="server" Size="50" onfocus="setBackgroundColor(this);"
                        onblur="removeBackgroundColor(this);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Сообщение:
                </td>
                <td>
                    <asp:TextBox ID="messageTextBox" runat="server" TextMode="MultiLine" Columns="50"
                        Rows="15" onfocus="setBackgroundColor(this);" onblur="removeBackgroundColor(this);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Файл:
                </td>
                <td>
                    <asp:FileUpload ID="attachmentFileUpload" runat="server" Size="50" onfocus="setBackgroundColor(this);"
                        onblur="removeBackgroundColor(this);" />
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
