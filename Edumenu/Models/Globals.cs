using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edumenu.Models
{
    public class Globals : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static string selectedDay = Utils.FirstCharToUpper(new CultureInfo("fi-FI").
            DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek));

        public static string SelectedDay
        {
            get
            {
                return selectedDay;
            }
            set
            {
                selectedDay = value;
                OnPropertyChanged("SelectedDay");
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


        


        public static int progress; // Total progress 0...1
        public static int nRestaurantsProcessed; // Restaurants processed per school
        public static bool allRestaurantsProcessed; // Can we exit the background thread?
    }
}
