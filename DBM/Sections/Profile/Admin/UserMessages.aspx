<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="UserMessages.aspx.cs" Inherits="Sections_Profile_Admin_UserMessages" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="сообщения от пользователей" />
    <meta name="description" content="Сообщения от пользователей" />
    <title>Сообщения от пользователей</title>
    <link href="/CSS/jQuery/Tables/demo_table_jui.css" rel="Stylesheet" type="text/css" />
    <link href="/CSS/Sections/Profile/Messages.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/JS/jQuery/Tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/JS/Sections/Profile/Messages.js"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="filler">
        </div>
        <div>
            |<a href="Users.aspx">Пользователи</a>| |<a href="UserMessages.aspx"><span class="thispage">Сообщения
                пользователей</span></a>|
        </div>
        <div class="filler">
        </div>
        <asp:Panel ID="infoPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <asp:Panel ID="messagesPanel" runat="server">
        </asp:Panel>
        <div class="filler">
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true"
        ScriptMode="Release">
    </asp:ScriptManager>
</asp:Content>
