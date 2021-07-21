<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="Tables.aspx.cs" Inherits="Sections_DB_Tables_Tables" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="таблицы текущей базы данных, просмотр объектов базы данных, открыть таблицу, изменить таблицу, добавить таблицу, удалить таблицу" />
    <meta name="description" content="Таблицы текущей базы данных" />
    <title>Таблицы текущей базы данных</title>
    <link href="/CSS/jQuery/Tables/demo_table_jui.css" rel="Stylesheet" type="text/css" />
    <link href="/CSS/Sections/Manager/DB.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/JS/jQuery/Tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/JS/Sections/Manager/DB.js"></script>
    <script type="text/javascript" src="/JS/Sections/Manager/DB/Tables/Tables.js"></script>
    <script type="text/javascript" src="/JS/jQuery/SimpleModal/jquery.simplemodal-1.4.2.js"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="filler">
        </div>
        <asp:Panel ID="headConnectionPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <div>
            |<a href="Tables.aspx"><span class="thispage">Таблицы</span></a>|
            |<a href="../Procedures/Procedures.aspx">Процедуры</a>|
        </div>
        <div class="filler">
        </div>
        <asp:Panel ID="infoPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <asp:Panel ID="tablesPanel" runat="server">
        </asp:Panel>
        <div style="display: none">
            <asp:Button ID="openTableProcessButton" runat="server" Text="Button" OnClick="openTableProcessButton_Click" />
            <asp:Button ID="dropTableProcessButton" runat="server" Text="Button" OnClick="dropTableProcessButton_Click" />
        </div>
        <div id='confirm'>
            <!--
            <div class='header'>
                <span>Подтвердите удаление таблицы</span>
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
