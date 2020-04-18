using System;
using System.Net;
using System.Threading.Tasks;
using EmailSender.Models;
using EmailSender.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Controllers
{
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        private readonly IEmailService emailService;
        public EmailSenderController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [Route("Email/Send/{token}")]
        [HttpPost]
        public IActionResult Send(String token, MailDTO mail)
        {
            var code = this.emailService.SendEmail(token, mail);

            return this.StatusCode((int)code,
                                    new
                                    {
                                        Id = Guid.NewGuid(),
                                        Date = DateTime.Now,
                                        Success = code == HttpStatusCode.OK
                                    });
        }
    }
}
