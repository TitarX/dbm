<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="Procedures.aspx.cs" Inherits="Sections_DB_Procedures_Procedures" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="процедуры текущей базы данных, просмотр объектов базы данных" />
    <meta name="description" content="Процедуры текущей базы данных" />
    <title>Процедуры текущей базы данных</title>
    <link href="/CSS/jQuery/Tables/demo_table_jui.css" rel="Stylesheet" type="text/css" />
    <link href="/CSS/Sections/Manager/DB.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/JS/jQuery/Tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/JS/Sections/Manager/DB.js"></script>
    <script type="text/javascript" src="/JS/Sections/Manager/DB/Procedures/Procedures.js"></script>
    <script type="text/javascript" src="/JS/jQuery/SimpleModal/jquery.simplemodal-1.4.2.js"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="filler">
        </div>
        <asp:Panel ID="headConnectionPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <div>
            |<a href="../Tables/Tables.aspx">Таблицы</a>| |<a href="Procedures.aspx"><span class="thispage">Процедуры</span></a>|
        </div>
        <div class="filler">
        </div>
        <asp:Panel ID="infoPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <asp:Panel ID="proceduresPanel" runat="server">
        </asp:Panel>
        <div style="display: none">
            <asp:Button ID="dropProcedureProcessButton" runat="server" Text="Button" OnClick="dropProcedureProcessButton_Click" />
        </div>
        <div id='confirm'>
            <!--
            <div class='header'>
                <span>Подтвердите удаление процедуры</span>
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
