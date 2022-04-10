using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EmailSender.Models;

namespace EmailSender.Services
{
    public class SMTPEmailService : IEmailService
    {
        private readonly IEmailBuilder emailBuilder;

        public SMTPEmailService(IEmailBuilder emailBuilder)
        {
            this.emailBuilder = emailBuilder;
        }

        public HttpStatusCode SendEmail(String token, MailDTO mail)
        {
            try
            {
                Task.Run(() =>
                {
                    var email = this.emailBuilder.Build(token, mail);

                    this.Send(email);
                });

                return HttpStatusCode.OK;
            }
            catch (System.Exception)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private void Send(Mail mail)
        {
            using (var client = new SmtpClient(mail.Host, mail.Port))
            {
                client.UseDefaultCredentials = false;
                client.EnableSsl = mail.EnableSsl;
                client.Credentials = new NetworkCredential(mail.From, mail.Password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                this.Send(client, mail, mail.From, mail.BodyForMe);

                if (mail.SendCopyToContact)
                {
                    this.Send(client, mail, mail.Data.Email, mail.BodyForContact);
                }
            }
        }

        private void Send(SmtpClient client, Mail mail, String to, String body)
        {
            using (var message = new MailMessage())
            {
                message.To.Add(to);
                message.From = new MailAddress(mail.From, mail.Name);
                message.Subject = mail.Subject;
                message.IsBodyHtml = true;
                message.Body = body;

                client.Send(message);
            }
        }
    }
}