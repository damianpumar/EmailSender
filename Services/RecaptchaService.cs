using System;
using System.Net;
using System.Text.Json;
using EmailSender.Models;
using Microsoft.Extensions.Configuration;

namespace EmailSender.Services
{
    public class RecaptchaService : IRecaptchaService
    {
        private readonly IConfiguration configuration;

        public RecaptchaService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Boolean Validate(String token, String recaptchaToken)
        {
            if (String.IsNullOrEmpty(token)) return false;

            using (var client = new WebClient())
            {
                var secret = this.configuration[$"{token}:RecaptchaSecret"];

                if (string.IsNullOrEmpty(secret)) return false;

                var googleReply = client.DownloadString(String.Format(this.configuration["RecaptchaSite"], secret, recaptchaToken));

                var reCaptcha = JsonSerializer.Deserialize<ReCaptcha>(googleReply);

                return reCaptcha.Success;
            }
        }
    }
}