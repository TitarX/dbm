<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="Create.aspx.cs" Inherits="Sections_Queries_Create" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="Создать запрос к базе данных, создать запрос, отправить запрос, запрос к базе данных, sql запрос" />
    <meta name="description" content="Создать запрос к базе данных" />
    <title>Создать запрос к базе данных</title>
    <link href="/CSS/jQuery/Tables/demo_table_jui.css" rel="Stylesheet" type="text/css" />
    <link href="/CSS/Sections/Contacts.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/JS/jQuery/Tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/JS/Sections/Manager/Queries/Create.js"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="filler">
        </div>
        <asp:Panel ID="headConnectionPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <div>
            <asp:LoginView ID="LoginView" runat="server">
                <RoleGroups>
                    <asp:RoleGroup Roles="User">
                        <ContentTemplate>
                            |<a href="Create.aspx"><span class="thispage">Создать запрос</span></a>| |<a href="MyQueries.aspx">Мои
                                запросы</a>|
                        </ContentTemplate>
                    </asp:RoleGroup>
                </RoleGroups>
            </asp:LoginView>
        </div>
        <div class="miniFiller">
        </div>
        <h3>
            Создать запрос к базе данных:</h3>
        <div class="warning">
            <asp:LoginView ID="nameWarningLoginView" runat="server">
                <RoleGroups>
                    <asp:RoleGroup Roles="User">
                        <ContentTemplate>
                            <br />
                            При сохранении запроса название должно быть уникальным для коллекции запросов текущего
                            подключения
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
                    Создать запрос
                </th>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Тип запроса:
                </td>
                <td>
                    <select id="queryType" onchange="typeChanged(this);">
                        <option id="other" value="Другой" selected="selected">Другой</option>
                        <option id="select" value="SELECT">SELECT</option>
                        <option id="insert" value="INSERT">INSERT</option>
                        <option id="update" value="UPDATE">UPDATE</option>
                        <option id="delete" value="DELETE">DELETE</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="hintID">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
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
                <td>
                </td>
                <td>
                    <asp:LoginView ID="saveLoginView1" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="User">
                                <ContentTemplate>
                                    <% 
                                        if (Session["Connection"] != null && !((ConnectionData)Session["Connection"]).Name.Equals(""))
                                        {
                                            Response.Write("<input type='checkbox' id='saveCheckBox' name='saveCheckBox' onclick='changeNameTextBoxState(this);' />Сохранить");
                                        }
                                    %>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LoginView ID="saveLoginView2" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="User">
                                <ContentTemplate>
                                    <% 
                                        if (Session["Connection"] != null && !((ConnectionData)Session["Connection"]).Name.Equals(""))
                                        {
                                            Response.Write("Название:");
                                        }
                                    %>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                </td>
                <td>
                    <asp:LoginView ID="saveLoginView3" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="User">
                                <ContentTemplate>
                                    <% 
                                        if (Session["Connection"] != null && !((ConnectionData)Session["Connection"]).Name.Equals(""))
                                        {
                                            Response.Write("<input type='text' id='commandName' name='commandName' size='50' disabled='disabled' onfocus='setBackgroundColor(this);' onblur='removeBackgroundColor(this);' />");
                                        }
                                    %>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
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
                    <asp:Button ID="sendButton" runat="server" Text="Отправить" OnClick="button_Click" />
                </td>
            </tr>
        </table>
        <br />
        <hr />
        <h3>
            Результат:</h3>
        <asp:Panel ID="resultPanel" runat="server">
        </asp:Panel>
        <asp:Panel ID="exportPanel" runat="server" CssClass="exportPanel">
        </asp:Panel>
        <asp:GridView ID="gridView" runat="server" Width="100%">
        </asp:GridView>
        <div style="display: none">
            <asp:Button ID="excelExportProcessButton" runat="server" Text="Button" OnClick="button_Click" />
            <asp:Button ID="csvExportProcessButton" runat="server" Text="Button" OnClick="button_Click" />
        </div>
        <div class="filler">
        </div>
    </div>
</asp:Content>
