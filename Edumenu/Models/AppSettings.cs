using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Edumenu.Models
{
    class AppSettings : INotifyPropertyChanged
    {
        // These fields hold the values for the public properties
        //private string _selectedSchool;
        //private bool _firstLaunch;

        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;
        

        public string selectedSchool
        {
            get
            {
                InitializeIfNotSet("selectedSchool", "TTY");
                return (string)ApplicationData.Current.LocalSettings.Values["selectedSchool"];
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["selectedSchool"] = value;
                OnPropertyChanged("selectedSchool");
            }
        }
        public bool firstLaunch
        {
            get
            {
                InitializeIfNotSet("firstLaunch", true);
                return (bool)ApplicationData.Current.LocalSettings.Values["firstLaunch"];
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["firstLaunch"] = value;
                OnPropertyChanged("firstLaunch");
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
