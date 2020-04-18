using System;

namespace EmailSender.Models
{
    public class MailConfiguration
    {
        public String From { get; set; }

        public String Password { get; set; }

        public String Host { get; set; }

        public int Port { get; set; }

        public Boolean SSL { get; set; }

        public String Subject { get; set; }

        public String Name { get; set; }

        public String Website { get; set; }

        public Boolean SendCopyToContact { get; set; }

        public String TemplateForMe { get; set; }

        public String TemplateForContact { get; set; }
    }
}