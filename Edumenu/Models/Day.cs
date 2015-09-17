using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edumenu.Models
{
    class Day : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //private static string selected = Utils.FirstCharToUpper(new CultureInfo("fi-FI").
        //    DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek));

        private string name = string.Empty;
        private bool isSelected = false;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

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
