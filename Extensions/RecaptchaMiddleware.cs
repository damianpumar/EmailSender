using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EmailSender.Models;
using EmailSender.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace EmailSender.Extensions
{
    public class RecaptchaMiddleware
    {
        private readonly RequestDelegate next;

        public RecaptchaMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRecaptchaService recaptchaService)
        {
            var token = context.Request.Path.Value.Split('/').Last();

            var recaptcha = context.Request.Form["g-recaptcha-response"].ToString();

            if (String.IsNullOrEmpty(recaptcha) || !recaptchaService.Validate(token, recaptcha))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await context.Response.WriteAsync("Recaptcha token invalid.");

                return;
            }

            await this.next(context);
        }

        private async Task<ReCaptcha> ReadBody(HttpContext context)
        {
            ReCaptcha recaptcha;

            context.Request.EnableBuffering();

            using (var reader = new StreamReader(context.Request.Body,
                                    encoding: Encoding.UTF8,
                                    detectEncodingFromByteOrderMarks: false,
                                    bufferSize: 1024,
                                    leaveOpen: true))
            {
                var bodyString = await reader.ReadToEndAsync();

                recaptcha = JsonSerializer.Deserialize<ReCaptcha>(bodyString);

                context.Request.Body.Position = 0;
            }

            return recaptcha;
        }
    }

    public static class RecaptchaMiddlewareExtensions
    {
        public static IApplicationBuilder UseRecaptchaService(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RecaptchaMiddleware>();
        }
    }
}