using System;
using EmailSender.Models;

namespace EmailSender.Services
{
    public interface IRecaptchaService
    {
        Boolean Validate(String token, String recaptchaToken);
    }
}