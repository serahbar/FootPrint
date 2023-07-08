namespace FootPrint.Domain
{
    public static class StringExention
    {
        //TODO: replace value should reciver from db or a config file public const char ArabicKeChar = 'J';
        public const char ArabicYeChar1 = ' ';
        public const char ArabicYeChar2 = ' ';
        public const char PersianKeChar = ' ';
        public const char PersianYeChar = ' ';
        public static string SetPersianYeke(this string input)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            foreach (var item in keyValues)
            {
                input = input.Replace(item.Key, item.Value);

            }

            return input;
        }
    }
}
