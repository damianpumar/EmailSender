using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailSender.Extensions
{
    public static class Cors
    {
        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder application)
        {
            var configuration = (IConfiguration)application.ApplicationServices.GetRequiredService(typeof(IConfiguration));

            var cors = configuration["CORS"].Split(',').ToArray();

            application.UseCors(builder => builder.WithOrigins(cors)
                         .AllowAnyHeader()
                         .AllowAnyMethod());

            return application;
        }
    }
}