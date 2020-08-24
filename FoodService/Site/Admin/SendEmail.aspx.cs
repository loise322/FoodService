using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using TravelLine.Food.Core.Configs;

namespace TravelLine.Food.Site.Admin
{
    public partial class SendEmail : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {

        }

        protected bool IsValidEmail( string strIn )
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch( strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" );
        }

        protected string RemoveTags( string strIn )
        {
            Regex rex = new Regex( "<[^>]*>" );
            string strContent = rex.Replace( strIn, "" );
            return strContent;
        }

        protected void SendMail( object sender, EventArgs e )
        {
            string to = tbTo.Text;
            string subject = tbSubject.Text;
            string body = tbBody.Text;

            string from = ConfigService.SMTP_FROM;
            string login = ConfigService.SMTP_LOGIN;
            string password = ConfigService.SMTP_PASSWORD;
            string encoding = ConfigService.SMTP_ENCODING;
            string smtpHost = ConfigService.SMPT_HOST;
            string smtpPort = ConfigService.SMPT_PORT;
            bool enableSsl = ConfigService.SMTP_ENABLE_SSL;

            if ( !IsValidEmail( to ) )
            {
                lblStatus.Text = "Ошибка. Сообщение не отправлено. Неверный формат e-mail.";
                lblStatus.CssClass = "errorMessage";
            }
            else
            {
                try
                {
                    MailMessage Message = new MailMessage();
                    Message.Subject = subject;
                    Message.Body = body;
                    Message.From = new MailAddress( from );
                    Message.To.Add( new MailAddress( to ) );
                    Message.BodyEncoding = System.Text.Encoding.GetEncoding( encoding );
                    Message.SubjectEncoding = System.Text.Encoding.GetEncoding( encoding );

                    SmtpClient Smtp = new SmtpClient();
                    Smtp.Host = smtpHost;
                    Smtp.Port = int.Parse( smtpPort );
                    Smtp.EnableSsl = enableSsl;
                    Smtp.Credentials = new System.Net.NetworkCredential( login, password );

                    Smtp.Send( Message );

                    lblStatus.Text = "Сообщение успешно отправлено.";
                    lblStatus.CssClass = "okMessage";
                }
                catch ( System.Exception ex )
                {
                    string exceptionMessage = ex.Message;
                    lblStatus.Text = "Ошибка. Сообщение не отправлено. " + exceptionMessage;
                    lblStatus.CssClass = "errorMessage";
                }
            }
        }
    }
}
