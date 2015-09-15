using System.ComponentModel;
using Windows.Storage;

namespace Edumenu.Models
{
    class AppSettings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
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
                OnPropertyChanged("SelectedSchool");
            }
        }

        public bool FirstLaunch
        {
            get
            {
                InitializeIfNotSet("FirstLaunch", true);
                return (bool)ApplicationData.Current.LocalSettings.Values["FirstLaunch"];
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["FirstLaunch"] = value;
                OnPropertyChanged("FirstLaunch");
            }
        }

        // Initialize setting if it has not been set already
        private void InitializeIfNotSet(string settingName, object settingVal)
        {
            var localSettings = ApplicationData.Current.LocalSettings;

            if (!localSettings.Values.ContainsKey(settingName))
            {
                localSettings.Values[settingName] = settingVal;
            }
        }

        // OnPropertyChanged notifies the view of the changes made
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
