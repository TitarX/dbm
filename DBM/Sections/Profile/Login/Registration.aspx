<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.master" AutoEventWireup="true"
    CodeFile="Registration.aspx.cs" Inherits="Sections_Profile_Login_Registration" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <meta name="keywords" content="регистрация" />
    <meta name="description" content="Регистрация" />
    <title>Регистрация</title>
    <link href="/CSS/Sections/Profile/Login.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="content">
        <div class="fillerLoginClass">
        </div>
        <div>
            <asp:CreateUserWizard ID="CreateUserWizard" align="center" runat="server" BackColor="#CCCCCC"
                BorderColor="#000000" BorderStyle="Double" OnContinueButtonClick="CreateUserWizard_ContinueButtonClick"
                AnswerLabelText="Ответ на секретный вопрос: " AnswerRequiredErrorMessage="Вы не ввели ответ на секретный вопрос"
                CancelButtonText="Отмена" CompleteSuccessText="Вы успешно зарегистрировались"
                ConfirmPasswordCompareErrorMessage="Пароли не совпадают" ConfirmPasswordLabelText="Подтвердите пароль: "
                ConfirmPasswordRequiredErrorMessage="Вы не ввели подтверждение пароля" ContinueButtonText="Продолжить"
                CreateUserButtonText="Отправить" DuplicateEmailErrorMessage="Пользователь с введённым email уже существует"
                DuplicateUserNameErrorMessage="Пользователь с введённым именем уже существует"
                EditProfileText="" EditProfileUrl="" EmailLabelText="Email: " EmailRegularExpressionErrorMessage="Вы ввели некорректный email"
                EmailRegularExpression="^[_0-9a-zA-Zа-яА-Я][-._0-9a-zA-Zа-яА-Я]{0,29}[_0-9a-zA-Zа-яА-Я]@([0-9a-zA-Zа-яА-Я][-0-9a-zA-Zа-яА-Я]*[0-9a-zA-Zа-яА-Я]\.)+[a-zA-Zа-яА-Я]{2,8}$"
                EmailRequiredErrorMessage="Вы не ввели email" FinishPreviousButtonText="Назад"
                FinishCompleteButtonText="Завершить" InstructionText="Введите корректные данные, длинна пароля не менее 6 символов"
                InvalidAnswerErrorMessage="Вы ввели некорректный ответ на секретный вопрос" InvalidEmailErrorMessage="Вы ввели некорректный email"
                InvalidPasswordErrorMessage="Вы ввели некорректный пароль" InvalidQuestionErrorMessage="Вы ввели некорректный секретный вопрос"
                PasswordHintTex="" PasswordLabelText="Пароль: " PasswordRequiredErrorMessage="Вы не ввели пароль"
                QuestionLabelText="Секретный вопрос: " QuestionRequiredErrorMessage="Вы не ввели секретный вопрос"
                SkipLinkText="" StartNextButtonText="Продолжить" StepNextButtonText="Продолжить"
                StepPreviousButtonText="Назад" UnknownErrorMessage="" UserNameLabelText="Имя пользователя: "
                UserNameRequiredErrorMessage="Вы не ввели имя пользователя" HeaderText="Регистрация">
                <TitleTextStyle CssClass="disableTitleClass" />
                <HeaderStyle CssClass="titleClass" />
                <InstructionTextStyle CssClass="instructionClass" />
                <TextBoxStyle CssClass="textBoxClass" />
                <LabelStyle CssClass="labelClass" />
                <HyperLinkStyle CssClass="linkClass" />
                <ValidatorTextStyle CssClass="validatorClass" />
                <CompleteSuccessTextStyle CssClass="infoTextClass" />
                <ErrorMessageStyle CssClass="errorTextClass" />
                <WizardSteps>
                    <asp:CreateUserWizardStep ID="CreateUserWizardStep" runat="server">
                    </asp:CreateUserWizardStep>
                    <asp:CompleteWizardStep ID="CompleteWizardStep" runat="server">
                    </asp:CompleteWizardStep>
                </WizardSteps>
            </asp:CreateUserWizard>
        </div>
        <div class="fillerLoginClass">
        </div>
    </div>
</asp:Content>
