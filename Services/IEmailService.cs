using System;
using System.Net;
using System.Threading.Tasks;
using EmailSender.Models;

namespace EmailSender.Services
{
    public interface IEmailService
    {
        HttpStatusCode SendEmail(String token, MailDTO mail);
    }
}