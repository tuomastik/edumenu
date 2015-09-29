using Windows.Storage;

namespace Edumenu.Models
{
    class AppSettings
    {
        public string SelectedSchool
        {
            get
            {
                InitializeIfNotSet("SelectedSchool", "TTY");
                return (string)ApplicationData.Current.LocalSettings.Values["SelectedSchool"];
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["SelectedSchool"] = value;
            }
        }

        public string CurrentAppVersion
        {
            get
            {
                InitializeIfNotSet("CurrentAppVersion", "1.0.0.0");
                return (string)ApplicationData.Current.LocalSettings.Values["CurrentAppVersion"];
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["CurrentAppVersion"] = value;
            }
        }

        // Initialize setting if it has not been set already
        private void InitializeIfNotSet(string settingName, object defaultValue)
        {
            var localSettings = ApplicationData.Current.LocalSettings;

            if (!localSettings.Values.ContainsKey(settingName))
            {
                localSettings.Values[settingName] = defaultValue;
            }
        }
    }
}
