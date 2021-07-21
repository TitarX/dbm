using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Web.Security;
using System.Text;

public partial class Sections_Contacts_Feedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        infoPanel.Controls.Clear();
    }

    protected void sendButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (attachmentFileUpload.PostedFile.ContentLength != 0)
            {
                if (attachmentFileUpload.PostedFile.ContentLength > 10485760)
                {
                    infoPanel.Controls.Clear();
                    infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Присоединённый файл слишком большой</span>"));
                }
                else
                {
                    Attachment attachment = new Attachment(attachmentFileUpload.PostedFile.InputStream,
                        Path.GetFileName(attachmentFileUpload.PostedFile.FileName));

                    SendEmail(attachment);

                }
            }
            else
            {
                SendEmail(null);
            }

            infoPanel.Controls.Clear();
            infoPanel.Controls.Add(new LiteralControl("<br /><span class='info'>Сообщение успешно отправлено</span>"));
        }
        catch
        {
            infoPanel.Controls.Clear();
            infoPanel.Controls.Add(new LiteralControl("<br /><span class='error'>Не удалось отправить сообщение</span>"));
        }
    }

    private void SendEmail(Attachment attachment)
    {
        try
        {
            String to = "idmiv@yahoo.com";
            String subject = subjectTextBox.Text.Trim();
            String text = messageTextBox.Text.Trim();

            String userName = User.Identity.Name;
            if (!userName.Equals(""))
            {
                MembershipUser user = Membership.GetUser(userName);
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.Append(userName);
                messageBuilder.Append("\n");
                messageBuilder.Append(user.Email);
                messageBuilder.Append("\n");
                messageBuilder.Append("----------------");
                messageBuilder.Append("\n");
                messageBuilder.Append(text);
                text = messageBuilder.ToString();
            }

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("titarx@yandex.ru");
            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.Body = text;
            if (attachment != null)
            {
                mailMessage.Attachments.Add(attachment);
            }

            NetworkCredential credential = new NetworkCredential("titarx", "83aQ7572438ya");

            SmtpClient smtpClient = new SmtpClient("smtp.yandex.ru", 465);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credential;
            smtpClient.Send(mailMessage);
        }
        catch
        {
            throw;
        }
    }

    protected void resetButton_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.RawUrl);
        Server.Transfer(Request.FilePath);
    }
}