<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="Create.aspx.cs" Inherits="Sections_Connection_Create" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="создать подключение к базе данных, подключиться к базе данных" />
    <meta name="description" content="Создать подключение к базе данных" />
    <title>Создать подключение к базе данных</title>
    <link href="/CSS/Sections/Contacts.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/JS/Sections/Manager/Connections/Create.js"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="filler">
        </div>
        <div>
            <asp:LoginView ID="menuLoginView" runat="server">
                <RoleGroups>
                    <asp:RoleGroup Roles="User">
                        <ContentTemplate>
                            |<a href="Create.aspx"><span class="thispage">Создать подключение</span></a>| |<a
                                href="MyConnections.aspx">Мои подключения</a>|
                        </ContentTemplate>
                    </asp:RoleGroup>
                </RoleGroups>
            </asp:LoginView>
        </div>
        <div class="miniFiller">
        </div>
        <h3>
            Создать подключение к базе данных:</h3>
        <div class="warning">
            Все поля обязательны для заполнения
            <asp:LoginView ID="nameWarningLoginView" runat="server">
                <RoleGroups>
                    <asp:RoleGroup Roles="User">
                        <ContentTemplate>
                            <br />
                            При сохранении подключения название должно быть уникальным в вашей коллекции подключений
                        </ContentTemplate>
                    </asp:RoleGroup>
                </RoleGroups>
            </asp:LoginView>
        </div>
        <asp:Panel ID="infoPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <table align="center" id="contactsWrapper">
            <tr>
                <th colspan="2" id="contactsHeader">
                    Создать подключение
                </th>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Тип:
                </td>
                <td>
                    <asp:DropDownList ID="typeDropDownList" runat="server" onchange="typeChanged(this);">
                        <asp:ListItem Text="PostgreSQL" Value="PostgreSQL" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="MySQL" Value="MySQL"></asp:ListItem>
                        <asp:ListItem Text="Microsoft SQL Server" Value="Microsoft SQL Server"></asp:ListItem>
                        <asp:ListItem Text="Oracle" Value="Oracle"></asp:ListItem>
                        <asp:ListItem Text="IBM DB2" Value="IBM DB2"></asp:ListItem>
                        <asp:ListItem Text="Firebird" Value="Firebird"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Сервер:
                </td>
                <td>
                    <asp:TextBox ID="serverTextBox" runat="server" Size="50" onfocus="setBackgroundColor(this);"
                        onblur="removeBackgroundColor(this);"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="serverRequiredFieldValidator" runat="server" ErrorMessage=""
                        EnableClientScript="False" ControlToValidate="serverTextBox"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Порт:
                </td>
                <td>
                    <asp:TextBox ID="portTextBox" runat="server" Size="50" Text="5432" onfocus="setBackgroundColor(this);"
                        onblur="removeBackgroundColor(this);"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="portRequiredFieldValidator" runat="server" ErrorMessage=""
                        EnableClientScript="False" ControlToValidate="portTextBox"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td id="dbID">
                    <span class="dbClass">БД:</span>
                </td>
                <td>
                    <asp:TextBox ID="dbTextBox" runat="server" Size="50" onfocus="setBackgroundColor(this);"
                        onblur="removeBackgroundColor(this);"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="dbRequiredFieldValidator" runat="server" ErrorMessage=""
                        EnableClientScript="False" ControlToValidate="dbTextBox"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Пользователь:
                </td>
                <td>
                    <asp:TextBox ID="userTextBox" runat="server" Size="50" onfocus="setBackgroundColor(this);"
                        onblur="removeBackgroundColor(this);"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="userRequiredFieldValidator" runat="server" ErrorMessage=""
                        EnableClientScript="False" ControlToValidate="userTextBox"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Пароль:
                </td>
                <td>
                    <asp:TextBox ID="passwordTextBox" runat="server" TextMode="Password" Size="50" onfocus="setBackgroundColor(this);"
                        onblur="removeBackgroundColor(this);"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="passwordRequiredFieldValidator" runat="server" ErrorMessage=""
                        EnableClientScript="False" ControlToValidate="passwordTextBox"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <asp:LoginView ID="saveLoginView" runat="server">
                <RoleGroups>
                    <asp:RoleGroup Roles="User">
                        <ContentTemplate>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <input type="checkbox" id="saveCheckBox" name="saveCheckBox" onclick="changeNameTextBoxState(this);">Сохранить
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Название:
                                </td>
                                <td>
                                    <input type="text" id="connectionName" name="connectionName" size="50" disabled="disabled"
                                        onfocus="setBackgroundColor(this);" onblur="removeBackgroundColor(this);">
                                </td>
                            </tr>
                        </ContentTemplate>
                    </asp:RoleGroup>
                </RoleGroups>
            </asp:LoginView>
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
                    <asp:Button ID="sendButton" runat="server" Text="Подключить" OnClick="sendButton_Click" />
                </td>
            </tr>
        </table>
        <div class="filler">
        </div>
    </div>
</asp:Content>
