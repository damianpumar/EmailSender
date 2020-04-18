using System;
using System.Collections.Generic;
using System.IO;

namespace EmailSender.Extensions
{
    public static class TemplateReader
    {
        private static IDictionary<String, String> templates = new Dictionary<String, String>();

        public static String ReadTemplate(this String fileName)
        {
            if (templates.TryGetValue(fileName, out String body))
                return body;

            templates.Add(fileName, File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Resources", fileName)));

            return fileName.ReadTemplate();
        }
    }
}