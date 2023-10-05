
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HelpDesk.UI.Repiostry
{
    public static class EmailSender 
    {
        

        public static void SendEmail(string email, string subject, string htmlMessage)
        {
            string fromMail = "onkar.rana27@gmail.com";
            string fromPassword = "Singh!(*@";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(email));
            message.Body = "<html><body> " + htmlMessage + " </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,DeliveryMethod= SmtpDeliveryMethod.Network
        };
            smtpClient.Send(message);
        }

    }
}
