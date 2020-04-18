using System;
using EmailSender.Models;
using Microsoft.Extensions.Configuration;

namespace EmailSender.Services
{
    public class EmailBuilder : IEmailBuilder
    {
        private readonly IConfiguration configuration;

        public EmailBuilder(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Mail Build(string token, MailDTO mail)
        {
            var mailConfiguration = this.configuration.GetSection(token).Get<MailConfiguration>();

            return new Mail(mail, mailConfiguration);
        }
    }
}