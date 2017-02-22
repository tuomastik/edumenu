using System.Linq;
using Windows.UI;
using Windows.UI.ViewManagement;

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

        internal static void ConfigureStatusBar()
        {
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.BackgroundOpacity = 1.0;
            statusBar.BackgroundColor = Colors.Black;
            statusBar.ForegroundColor = Colors.White;
        }
    }
}
