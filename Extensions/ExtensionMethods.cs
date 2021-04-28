using System;

namespace Thue_online.Extensions
{
    public static class ExtensionMethods
    {
        public static bool HasRepetition(this String str)
        {
            for (int i = 1; i <= str.Length / 2; i++)
            {
                for (int j = 0; j <= str.Length - (2 * i); j++)
                {
                    var firstPart = str.Substring(j, i);
                    var secondPart = str.Substring(j + i, i);
                    if (String.Equals(firstPart, secondPart))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static (string,string) FindRepetition(this String str)
        {
            for (int i = 1; i <= str.Length / 2; i++)
            {
                for (int j = 0; j <= str.Length - (2 * i); j++)
                {
                    var firstPart = str.Substring(j, i);
                    var secondPart = str.Substring(j + i, i);
                    if (String.Equals(firstPart, secondPart))
                    {
                        return (firstPart, secondPart);
                    }
                }
            }
            return (String.Empty, String.Empty);
        }
    }
}
