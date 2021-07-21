<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Sections_Profile_Login_Login" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="вход" />
    <meta name="description" content="Вход" />
    <title>Вход</title>
    <link href="/CSS/Sections/Profile/Login.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="fillerLoginClass">
        </div>
        <div>
            <asp:Login ID="Login" runat="server" align="center" TitleText="Вход" InstructionText="Введите ваше имя пользователя и пароль"
                FailureText="Не удалось выполнить вход, убедитесь в правильности вводимых данных"
                UserNameLabelText="Имя пользователя: " PasswordLabelText="Пароль: " UserNameRequiredErrorMessage="Вы не ввели имя пользователя"
                PasswordRequiredErrorMessage="Вы не ввели пароль" LoginButtonText="Отправить"
                RememberMeText="Запомнить" CreateUserUrl="~/Sections/Profile/Login/Registration.aspx"
                CreateUserText="Регистрация" PasswordRecoveryUrl="~/Sections/Profile/Login/ResetPassword.aspx"
                DestinationPageUrl="~/Sections/Profile/My/Profile.aspx" PasswordRecoveryText="Сброс пароля"
                BackColor="#CCCCCC" BorderColor="#000000" BorderStyle="Double" OnLoggedIn="Login_LoggedIn">
                <TitleTextStyle CssClass="titleClass" />
                <InstructionTextStyle CssClass="instructionClass" />
                <TextBoxStyle CssClass="textBoxClass" />
                <LabelStyle CssClass="labelClass" />
                <HyperLinkStyle CssClass="linkClass" />
                <CheckBoxStyle CssClass="checkboxClass" />
                <FailureTextStyle CssClass="errorTextClass" />
                <ValidatorTextStyle CssClass="validatorClass" />
            </asp:Login>
        </div>
        <div class="fillerLoginClass">
        </div>
    </div>
</asp:Content>
