using System;
using EmailSender.Models;

namespace EmailSender.Services
{
    public interface IEmailBuilder
    {
        Mail Build(String token, MailDTO mail);
    }
}