<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="GlobalError.aspx.cs" Inherits="GlobalError" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="" />
    <meta name="description" content="Глобальная ошибка" />
    <title>Глобальная ошибка</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="filler">
        </div>
        <asp:Panel ID="globalErrorMessagePane" runat="server">
        </asp:Panel>
        <div class="filler">
        </div>
    </div>
</asp:Content>
