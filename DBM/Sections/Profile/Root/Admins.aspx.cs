using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text;
using System.Configuration.Provider;
using System.Net;
using System.Net.Mail;

public partial class Sections_Profile_Root_Admins : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        infoPanel.Controls.Clear();

        StringBuilder usersTableBuilder = new StringBuilder();
        usersTableBuilder.Append("<table id='usersTable' width='100%' align='center'>");
        usersTableBuilder.Append("<thead>");
        usersTableBuilder.Append("<tr>");
        usersTableBuilder.Append("<th>Администратор</th>");
        usersTableBuilder.Append("<th>Имя</th>");
        usersTableBuilder.Append("<th>Email</th>");
        usersTableBuilder.Append("<th>Блокировка</th>");
        usersTableBuilder.Append("<th>Online</th>");
        usersTableBuilder.Append("</tr>");
        usersTableBuilder.Append("</thead>");
        usersTableBuilder.Append("<tbody>");

        MembershipUserCollection userCollection = Membership.GetAllUsers();
        foreach (MembershipUser user in userCollection)
        {
            String userName = user.UserName;
            if (!Roles.IsUserInRole(userName, "Root"))
            {
                String email = user.Email;
                String question = user.PasswordQuestion;
                bool isAdmin = Roles.IsUserInRole(userName, "Admin");
                bool isLock = user.IsLockedOut;
                usersTableBuilder.Append(String.Format("<tr class='rowUsersTable' onclick='setUserValues(this,\"true\",\"{0}\",\"{1}\",\"{2}\");'>",
                    Corrector.ArgumentHtmlEncoding(userName), Corrector.ArgumentHtmlEncoding(email), isAdmin));

                usersTableBuilder.Append("<td>");
                usersTableBuilder.Append("<input type='checkbox' id='isAdmin' name='isAdmin' disabled");
                if (isAdmin)
                {
                    usersTableBuilder.Append(" checked");
                }
                usersTableBuilder.Append(" />");
                usersTableBuilder.Append("</td>");

                usersTableBuilder.Append("<td>");
                usersTableBuilder.Append(Server.HtmlEncode(userName));
                usersTableBuilder.Append("</td>");

                usersTableBuilder.Append("<td>");
                usersTableBuilder.Append(Server.HtmlEncode(email));
                usersTableBuilder.Append("</td>");

                usersTableBuilder.Append("<td>");
                usersTableBuilder.Append("<input type='checkbox' id='isLocked' name='isLocked' disabled");
                if (isLock)
                {
                    usersTableBuilder.Append(" checked");
                }
                usersTableBuilder.Append(" />");
                usersTableBuilder.Append("</td>");

                usersTableBuilder.Append("<td>");
                usersTableBuilder.Append("<input type='checkbox' id='isOnline' name='isOnline' disabled");
                if (user.IsOnline)
                {
                    usersTableBuilder.Append(" checked");
                }
                usersTableBuilder.Append(" />");
                usersTableBuilder.Append("</td>");

                usersTableBuilder.Append("</tr>");
            }
        }
        usersTableBuilder.Append("</tbody>");
        usersTableBuilder.Append("</table>");

        adminsPanel.Controls.Clear();
        adminsPanel.Controls.Add(new LiteralControl(usersTableBuilder.ToString()));
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {
        Page.Validate();
        bool isValid = true;
        StringBuilder errorMessageBuilder = new StringBuilder();
        String userName = "";
        if (Request.Params["userName"] != null)
        {
            userName = Request.Params["userName"].Trim();
        }
        if (userName.Equals(""))
        {
            isValid = false;
            errorMessageBuilder.Append("<br /><span class='error'>Поле \"Имя\" не заполнено</span>");
        }
        if (!emailRequiredFieldValidator.IsValid)
        {
            isValid = false;
            errorMessageBuilder.Append("<br /><span class='error'>Поле \"Email\" не заполнено</span>");
        }

        if (isValid)
        {
            String email = emailTextBox.Text.Trim();
            bool isAdmin = Request.Params["adminCheckbox"] != null;

            MembershipUser user = Membership.GetUser(userName);
            if (user == null)
            {
                infoPanel.Controls.Clear();
                infoPanel.Controls.Add(new LiteralControl(String.Format("<br /><span class='error'>Пользователь {0} не найден</span>", Server.HtmlEncode(userName))));
            }
            else
            {
                bool toContinue = true;

                try
                {
                    user.Email = email;
                    Membership.UpdateUser(user);
                }
                catch (ProviderException)
                {
                    toContinue = false;
                }

                if (toContinue)
                {
                    if (Roles.IsUserInRole(userName, "Admin"))
                    {
                        if (!isAdmin)
                        {
                            Roles.RemoveUserFromRole(userName, "Admin");
                        }
                    }
                    else
                    {
                        if (isAdmin)
                        {
                            Roles.AddUserToRole(userName, "Admin");
                        }
                    }

                    Server.Transfer(Request.FilePath);
                }
                else
                {
                    infoPanel.Controls.Clear();
                    infoPanel.Controls.Add(new LiteralControl(String.Format("<br /><span class='error'>Указанный email уже зарегистрирован в системе или некорректен</span>")));
                }
            }
        }
        else
        {
            infoPanel.Controls.Clear();
            infoPanel.Controls.Add(new LiteralControl(errorMessageBuilder.ToString()));
        }
    }

    protected void button_Click(object sender, EventArgs e)
    {
        String userName = "";
        if (Request.Params["userName"] != null)
        {
            userName = Request.Params["userName"].Trim();
        }
        if (userName.Equals(""))
        {
            infoPanel.Controls.Clear();
            infoPanel.Controls.Add(new LiteralControl(String.Format("<br /><span class='error'>Поле \"Имя\" не заполнено</span>")));
        }
        else
        {
            MembershipUser user = Membership.GetUser(userName);
            if (user == null)
            {
                infoPanel.Controls.Clear();
                infoPanel.Controls.Add(new LiteralControl(String.Format("<br /><span class='error'>Пользователь {0} не найден</span>", Server.HtmlEncode(userName))));
            }
            else
            {
                if (sender.Equals(unlockUserServerButton))
                {
                    user.UnlockUser();
                    Server.Transfer(Request.FilePath);
                }
                else if (sender.Equals(resetPasswordServerButton))
                {
                    String newPassword = user.ResetPassword();

                    bool toContinue = true;
                    try
                    {
                        String to = user.Email;
                        String subject = "Сброс пароля dbm.webcentrum.ru";
                        String text = String.Format("Ваш новый пароль: {0}", newPassword);

                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress("titarx@yandex.ru");
                        mailMessage.To.Add(to);
                        mailMessage.Subject = subject;
                        mailMessage.Body = text;

                        NetworkCredential credential = new NetworkCredential("titarx", "83aQ7572438ya");

                        SmtpClient smtpClient = new SmtpClient("smtp.yandex.ru", 465);
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = credential;
                        smtpClient.Send(mailMessage);
                    }
                    catch
                    {
                        toContinue = false;
                    }

                    if (toContinue)
                    {
                        infoPanel.Controls.Clear();
                        infoPanel.Controls.Add(new LiteralControl(String.Format("<br /><span class='info'>Новый пароль для пользователя {0} сгенерирован и отправлен на email указанный при регистрации</span>"
                            , Server.HtmlEncode(userName))));
                    }
                    else
                    {
                        infoPanel.Controls.Clear();
                        infoPanel.Controls.Add(new LiteralControl(String.Format("<br /><span class='warning'>Новый пароль для пользователя {0} сгенерирован, но его отправка на email указанный при регистрации завершилась неудачно, повторите попытку позже или сообщите пользователю его новый пароль: {1}</span>"
                            , Server.HtmlEncode(userName), Server.HtmlEncode(newPassword))));
                    }
                }
                else if (sender.Equals(completeDeleteServerButton) || sender.Equals(incompleteDeleteServerButton))
                {
                    if (Membership.DeleteUser(userName, sender.Equals(completeDeleteServerButton)))
                    {
                        infoPanel.Controls.Clear();
                        Server.Transfer(Request.FilePath);
                        //infoPanel.Controls.Add(new LiteralControl(String.Format("<br /><span class='info'>Пользователь {0} и все связанные с ним данные успешно удалены</span>", Server.HtmlEncode(userName))));
                    }
                    else
                    {
                        infoPanel.Controls.Clear();
                        infoPanel.Controls.Add(new LiteralControl(String.Format("<br /><span class='error'>Не удалось удалить пользователя {0}</span>", Server.HtmlEncode(userName))));
                    }
                }
            }
        }
    }
}