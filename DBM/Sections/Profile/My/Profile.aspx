<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="Profile.aspx.cs" Inherits="Sections_Profile_My_Profile" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="мой профиль" />
    <meta name="description" content="Мой профиль" />
    <title>Мой профиль</title>
    <link href="/CSS/Sections/Profile/Login.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="filler">
        </div>
        <div>
            <asp:LoginView ID="LoginView1" runat="server">
                <RoleGroups>
                    <asp:RoleGroup Roles="Root">
                        <ContentTemplate>
                        </ContentTemplate>
                    </asp:RoleGroup>
                </RoleGroups>
                <RoleGroups>
                    <asp:RoleGroup Roles="Admin">
                        <ContentTemplate>
                            |<a href="Profile.aspx"><span class="thispage">Мой профиль</span></a>| |<a href="MessageToRoot.aspx">Отправить
                                сообщение главному администратору</a>|
                        </ContentTemplate>
                    </asp:RoleGroup>
                </RoleGroups>
                <RoleGroups>
                    <asp:RoleGroup Roles="User">
                        <ContentTemplate>
                            |<a href="Profile.aspx"><span class="thispage">Мой профиль</span></a>| |<a href="MessageToAdmin.aspx">Отправить
                                сообщение администратору</a>|
                        </ContentTemplate>
                    </asp:RoleGroup>
                </RoleGroups>
            </asp:LoginView>
        </div>
        <div class="filler">
        </div>
        <asp:Panel ID="infoPanel" runat="server" CssClass="infoPanel">
        </asp:Panel>
        <div>
            <asp:ChangePassword ID="ChangePassword" align="center" runat="server" BackColor="#CCCCCC"
                BorderColor="#000000" BorderStyle="Double" SuccessText="Пароль успешно изменён"
                CancelButtonText="Отменить" ChangePasswordButtonText="Отправить" ChangePasswordFailureText="Не удалось сменить пароль, проверьте правильность вводимых данных"
                ChangePasswordTitleText="Смена пароля" InstructionText="Введите старый и новый пароль, не менее 6 символов"
                ConfirmNewPasswordLabelText="Подтвердите новый пароль: " ConfirmPasswordCompareErrorMessage="Пароли не совпадают"
                ConfirmPasswordRequiredErrorMessage="Вы не ввели подтверждение нового пароля"
                ContinueButtonText="Продолжить" CreateUserText="" CreateUserUrl="" EditProfileText=""
                EditProfileUrl="" NewPasswordLabelText="Новый пароль: " NewPasswordRegularExpressionErrorMessage="Вы ввели неверный старый пароль"
                NewPasswordRequiredErrorMessage="Вы не ввели новый пароль" PasswordLabelText="Старый пароль: "
                PasswordHintText="" SuccessTitleText="" UserNameLabelText="Имя пользователя"
                UserNameRequiredErrorMessage="Вы не ввели имя пользователя" OnContinueButtonClick="ChangePassword_ContinueButtonClick">
                <TitleTextStyle CssClass="titleClass" />
                <InstructionTextStyle CssClass="instructionClass" />
                <SuccessTextStyle CssClass="infoTextClass" />
                <TextBoxStyle CssClass="textBoxClass" />
                <LabelStyle CssClass="labelClass" />
                <HyperLinkStyle CssClass="linkClass" />
                <FailureTextStyle CssClass="errorTextClass" />
                <ValidatorTextStyle CssClass="validatorClass" />
            </asp:ChangePassword>
        </div>
        <div class="filler">
        </div>
    </div>
</asp:Content>
