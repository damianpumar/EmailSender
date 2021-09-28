namespace EmailSender.Extensions
{
    public static class ReplaceExtensions
    {
        public static string ReplaceIfNotEmpty(this string str, string oldValue, string newValue)
        {
            if (!string.IsNullOrEmpty(newValue))
            {
                str = str.Replace(oldValue, newValue);
            }

            return str;
        }
    }
}
