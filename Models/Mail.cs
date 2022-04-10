using System;
using EmailSender.Extensions;

namespace EmailSender.Models
{
    public class Mail
    {
        public Mail(MailDTO mail, MailConfiguration mailConfiguration)
        {
            this.Data = mail;

            this.Host = mailConfiguration.Host;
            this.Password = mailConfiguration.Password;
            this.Port = mailConfiguration.Port;
            this.EnableSsl = mailConfiguration.SSL;
            this.From = mailConfiguration.From;
            this.Name = mailConfiguration.Name;
            this.Subject = String.Format(mailConfiguration.Subject, mailConfiguration.Website);
            this.SendCopyToContact = mailConfiguration.SendCopyToContact;
            this.BodyForMe = mailConfiguration.TemplateForMe
                                              .ReadTemplate()
                                              .Replace("#ME#", mailConfiguration.Name)
                                              .Replace("#NAME#", mail.Name)
                                              .Replace("#EMAIL#", mail.Email)
                                              .Replace("#MESSAGE#", mail.Message)
                                              .ReplaceIfNotEmpty("#SURNAME#", mail.Surname)
                                              .ReplaceIfNotEmpty("#PHONE#", mail.Phone)
                                              .ReplaceIfNotEmpty("#COMPANY#", mail.Company)
                                              .Replace("#DATE#", DateTime.Now.ToLongDateString());


            if (mailConfiguration.SendCopyToContact)
            {
                this.BodyForContact = mailConfiguration.TemplateForContact
                                                       .ReadTemplate()
                                                       .Replace("#WebSiteSending#", mailConfiguration.Website)
                                                       .Replace("#ME#", mailConfiguration.Name)
                                                       .Replace("#DATE#", DateTime.Now.ToLongDateString());
            }
        }

        public MailDTO Data { get; }

        public String Host { get; }

        public String Password { get; }

        public int Port { get; }

        public Boolean EnableSsl { get; }

        public String From { get; }

        public String Name { get; }

        public String Subject { get; }

        public Boolean SendCopyToContact { get; }

        public string BodyForMe { get; }

        public string BodyForContact { get; }
    }
}