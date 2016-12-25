using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WhereEat.Net
{
    public class Mail
    {
        public void sendMail(string body, string subject, string to)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.EnableSsl = true;
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("wheredoestoeattoday@gmail.com", "Tedesco24!");

            MailMessage mail = new MailMessage();
            mail.Sender = new MailAddress(to, "ENVIADOR");
            mail.From = new MailAddress("noreply@wheretoeat.com", "Where To Eat?");
            mail.To.Add(new MailAddress(to, "You!"));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            try
            {
                client.Send(mail);
            }
            catch (System.Exception error)
            {
                throw new Exception(string.Format("Message: {0} | Stack: {1}", error.Message, error.StackTrace));
            }
            finally
            {
                mail = null;
            }
        }
    }
}
