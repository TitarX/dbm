<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="Feedback.aspx.cs" Inherits="Sections_Contacts_Feedback" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="обратная связь" />
    <meta name="description" content="Обратная связь" />
    <title>Обратная связь</title>
    <link href="/CSS/Sections/Contacts.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/JS/Sections/Contacts/Feedback.js"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="filler">
        </div>
        <asp:Panel ID="infoPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <table align="center" id="contactsWrapper">
            <tr>
                <th colspan="2" id="contactsHeader">
                    Обратная связь
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
