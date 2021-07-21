<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="ResetPassword.aspx.cs" Inherits="Sections_Profile_Login_ResetPassword" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="сброс пароля" />
    <meta name="description" content="Сброс пароля" />
    <title>Сброс пароля</title>
    <link href="/CSS/Sections/Profile/Login.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="fillerLoginClass">
        </div>
        <div>
            <asp:PasswordRecovery ID="PasswordRecovery" align="center" runat="server" BackColor="#CCCCCC"
                BorderColor="#000000" BorderStyle="Double" SuccessText="Новый пароль выслан на email указанный при регистрации"
                GeneralFailureText="Не удалось сбросить пароль, проверьте правильность вводимых данных"
                SubmitButtonText="Отправить" UserNameLabelText="Имя пользователя: " UserNameFailureText="Вы ввели неверное имя пользователя"
                UserNameInstructionText="Введите имя пользователя" UserNameRequiredErrorMessage="Вы не ввели имя пользователя"
                UserNameTitleText="Сброс пароля" AnswerLabelText="Ответ" AnswerRequiredErrorMessage="Вы не ввели ответ"
                QuestionTitleText="Защитный вопрос" QuestionLabelText="Вопрос" QuestionInstructionText="Введите ответ на секретный вопрос, указанный при регистрации"
                QuestionFailureText="Ответ не верный">
                <MailDefinition Subject="Сброс пароля" From="idmiv@yahoo.com" Priority="High" />
                <TitleTextStyle CssClass="titleClass" />
                <InstructionTextStyle CssClass="instructionClass" />
                <SuccessTextStyle CssClass="infoTextClass" />
                <TextBoxStyle CssClass="textBoxClass" />
                <LabelStyle CssClass="labelClass" />
                <HyperLinkStyle CssClass="linkClass" />
                <FailureTextStyle CssClass="errorTextClass" />
                <ValidatorTextStyle CssClass="validatorClass" />
            </asp:PasswordRecovery>
        </div>
        <div class="fillerLoginClass">
        </div>
    </div>
</asp:Content>
