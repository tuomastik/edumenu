using System.Linq;

namespace Edumenu.Models
{
    class Utils
    {
        public static string FirstCharToUpper(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
        }
    }
}
