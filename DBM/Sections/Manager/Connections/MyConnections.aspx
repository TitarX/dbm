<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="MyConnections.aspx.cs" Inherits="Sections_Connections_My_Connections" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="мои подключения, подключиться к базе данных" />
    <meta name="description" content="Мои подключения" />
    <title>Мои подключения</title>
    <link href="/CSS/jQuery/Tables/demo_table_jui.css" rel="Stylesheet" type="text/css" />
    <link href="/CSS/Sections/Manager/Connections/MyConnections.css" rel="Stylesheet"
        type="text/css" />
    <script type="text/javascript" src="/JS/jQuery/Tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/JS/jQuery/SimpleModal/jquery.simplemodal-1.4.2.js"></script>
    <script type="text/javascript" src="/JS/Sections/Manager/Connections/MyConnections.js"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="filler">
        </div>
        <div>
            |<a href="Create.aspx">Создать подключение</a>| |<a href="MyConnections.aspx"><span
                class="thispage">Мои подключения</span></a>|
        </div>
        <div class="filler">
        </div>
        <asp:Panel ID="infoPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <asp:Panel ID="connectionsPanel" runat="server">
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
                        <input type="button" id="deleteConnectionButton" name="deleteConnectionButton" value="Удалить"
                            onclick="deleteConnection();" />
                        <div style="display: none">
                            <asp:Button ID="deleteConnectionServerButton" runat="server" Text="Button" OnClick="button_Click" />
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
                    <td>
                        Тип:
                    </td>
                    <td>
                        <asp:DropDownList ID="typeDropDownList" runat="server" onchange="typeChanged($('#MainContentPlaceHolder_typeDropDownList').val());">
                            <asp:ListItem Text="PostgreSQL" Value="PostgreSQL" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="MySQL" Value="MySQL"></asp:ListItem>
                            <asp:ListItem Text="Microsoft SQL Server" Value="Microsoft SQL Server"></asp:ListItem>
                            <asp:ListItem Text="Oracle" Value="Oracle"></asp:ListItem>
                            <asp:ListItem Text="IBM DB2" Value="IBM DB2"></asp:ListItem>
                            <asp:ListItem Text="Firebird" Value="Firebird"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr onmouseover="setBackgroundColor(this);" onmouseout="removeBackgroundColor(this);">
                    <td>
                        Сервер:
                    </td>
                    <td>
                        <asp:TextBox ID="serverTextBox" runat="server" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="serverRequiredFieldValidator" runat="server" ErrorMessage=""
                            EnableClientScript="False" ControlToValidate="serverTextBox"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr onmouseover="setBackgroundColor(this);" onmouseout="removeBackgroundColor(this);">
                    <td>
                        Порт:
                    </td>
                    <td>
                        <asp:TextBox ID="portTextBox" runat="server" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="portRequiredFieldValidator" runat="server" ErrorMessage=""
                            EnableClientScript="False" ControlToValidate="portTextBox"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr onmouseover="setBackgroundColor(this);" onmouseout="removeBackgroundColor(this);">
                    <td id="dbID">
                        <span class="dbClass">БД:</span>
                    </td>
                    <td>
                        <asp:TextBox ID="dbTextBox" runat="server" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="dbRequiredFieldValidator" runat="server" ErrorMessage=""
                            EnableClientScript="False" ControlToValidate="dbTextBox"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr onmouseover="setBackgroundColor(this);" onmouseout="removeBackgroundColor(this);">
                    <td>
                        Пользователь:
                    </td>
                    <td>
                        <asp:TextBox ID="userTextBox" runat="server" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="userRequiredFieldValidator" runat="server" ErrorMessage=""
                            EnableClientScript="False" ControlToValidate="userTextBox"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr onmouseover="setBackgroundColor(this);" onmouseout="removeBackgroundColor(this);">
                    <td>
                        Пароль:
                    </td>
                    <td>
                        <asp:TextBox ID="passwordTextBox" runat="server" TextMode="Password" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="passwordRequiredFieldValidator" runat="server" ErrorMessage=""
                            EnableClientScript="False" ControlToValidate="passwordTextBox"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <input type="button" id="resetButton" name="resetButton" value="Сбросить" onclick="resetConnectionValues();" />
                    </td>
                    <td align="right">
                        <input type="button" id="saveButton" name="saveButton" value="Сохранить" onclick="saveConnectionValues();" />
                        <div style="display: none">
                            <asp:Button ID="saveServerButton" runat="server" Text="Button" OnClick="button_Click" />
                        </div>
                        <input type="button" id="connectButton" name="connectButton" value="Подключить" onclick="connectF();" />
                        <div style="display: none">
                            <asp:Button ID="connectServerButton" runat="server" Text="Button" OnClick="button_Click" />
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
