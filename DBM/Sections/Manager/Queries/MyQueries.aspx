<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="MyQueries.aspx.cs" Inherits="Sections_Queries_My_Queries" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="мои запросы, отправить запрос, запрос к базе данных, sql запрос" />
    <meta name="description" content="Мои запросы" />
    <title>Мои запросы</title>
    <link href="/CSS/jQuery/Tables/demo_table_jui.css" rel="Stylesheet" type="text/css" />
    <link href="/CSS/Sections/Manager/Queries/MyQueries.css" rel="Stylesheet"
        type="text/css" />
    <script type="text/javascript" src="/JS/jQuery/Tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/JS/jQuery/SimpleModal/jquery.simplemodal-1.4.2.js"></script>
    <script type="text/javascript" src="/JS/Sections/Manager/Queries/MyQueries.js"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="filler">
        </div>
        <asp:Panel ID="headConnectionPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <div>
            |<a href="Create.aspx">Создать запрос</a>|
            |<a href="MyQueries.aspx"><span class="thispage">Мои запросы</span></a>|
        </div>
        <div class="filler">
        </div>
        <asp:Panel ID="infoPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <asp:Panel ID="queriesPanel" runat="server">
        </asp:Panel>
        <div id="basic-modal-content">
            <table width="100%">
                <tr>
                    <td colspan="2" align="center">
                        <span class="warning">Все поля обязательны для заполнения</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr align="right">
                    <td colspan="2">
                        <input type="button" id="deleteQueryButton" name="deleteQueryButton" value="Удалить"
                            onclick="deleteQuery();" />
                        <div style="display: none">
                            <asp:Button ID="deleteQueryServerButton" runat="server" Text="Button" OnClick="button_Click" />
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
                        Название:
                    </td>
                    <td>
                        <asp:TextBox ID="newNameTextBox" runat="server" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="newNameRequiredFieldValidator" runat="server" ErrorMessage=""
                            EnableClientScript="False" ControlToValidate="newNameTextBox"></asp:RequiredFieldValidator>
                        <input type="hidden" id="oldName" name="oldName" value="" />
                    </td>
                </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="queryTextBox" runat="server" TextMode="MultiLine" Columns="80" Rows="10"
                        onfocus="setBackgroundColor(this);" onblur="removeBackgroundColor(this);"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="queryRequiredFieldValidator" runat="server" ErrorMessage=""
                        EnableClientScript="False" ControlToValidate="queryTextBox"></asp:RequiredFieldValidator>
                </td>
            </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <input type="button" id="resetButton" name="resetButton" value="Сбросить" onclick="resetQueryValues();" />
                    </td>
                    <td align="right">
                        <input type="button" id="saveButton" name="saveButton" value="Сохранить" onclick="saveQueryValues();" />
                        <div style="display: none">
                            <asp:Button ID="saveServerButton" runat="server" Text="Button" OnClick="button_Click" />
                        </div>
                        <input type="button" id="prepareButton" name="prepareButton" value="Подготовить к выполнению" onclick="prepareF();" />
                        <div style="display: none">
                            <asp:Button ID="prepareServerButton" runat="server" Text="Button" OnClick="prepareServerButton_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id='confirm'>
            <!--
            <div class='header'>
                <span>Подтвердите удаление подключения</span>
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
