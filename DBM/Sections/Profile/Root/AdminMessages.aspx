<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="AdminMessages.aspx.cs" Inherits="Sections_Profile_Root_AdminMessages" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="сообщения от администраторов" />
    <meta name="description" content="Сообщения от администраторов" />
    <title>Сообщения от администраторов</title>
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
            |<a href="Admins.aspx">Администраторы</a>| |<a href="AdminMessages.aspx"><span class="thispage">Сообщения
                администраторов</span></a>|
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
