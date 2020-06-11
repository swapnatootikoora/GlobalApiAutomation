using System;
using System.Text;

namespace Common.Helpers
{
    public static class GenericLibrary
    {
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public static string RandomString(int size, bool lowerCase = true)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
