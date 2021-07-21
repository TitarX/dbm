<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true" CodeFile="OpenTable.aspx.cs" Inherits="Sections_Manager_DB_Tables_OpenTable" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
    <meta name="keywords" content="просмотр таблицы" />
    <meta name="description" content="Просмотр таблицы" />
    <title>Просмотр таблицы</title>
    <link href="/CSS/jQuery/Tables/demo_table_jui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/JS/jQuery/Tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/JS/Sections/Manager/DB/Tables/OpenTable.js"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div id="content">
        <div class="filler">
        </div>
        <asp:Panel ID="headConnectionPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <div>
            |<a href="Tables.aspx">Таблицы</a>|
        </div>
        <div class="filler">
        </div>
        <asp:Panel ID="headPanel" runat="server" CssClass="headPanel">
        </asp:Panel>
        <asp:Panel ID="infoPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <asp:Panel ID="exportPanel" runat="server" CssClass="exportPanel">
        </asp:Panel>
        <asp:GridView ID="gridView" runat="server" Width="100%">
        </asp:GridView>
        <div style="display:none">
            <asp:Button ID="excelExportProcessButton" runat="server" Text="Button" OnClick="button_Click" />
            <asp:Button ID="csvExportProcessButton" runat="server" Text="Button" OnClick="button_Click" />
        </div>
        <div class="filler">
        </div>
    </div>
</asp:Content>

