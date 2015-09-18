using System.ComponentModel;

namespace Edumenu.Models
{
    public class School : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name_FI { get; set; }
        public string Name_EN { get; set; }
        public string NameShort_EN { get; set; }
        public string NameShort_FI { get; set; }

        private bool isSelected = false;
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

        public static School tut = new School
        {
            Name_EN = "Tampere University of Technology",
            Name_FI = "Tampereen teknillinen yliopisto",
            NameShort_EN = "TUT",
            NameShort_FI = "TTY",
        };
        public static School uta = new School
        {
            Name_EN = "University of Tampere",
            Name_FI = "Tampereen yliopisto",
            NameShort_EN = "UTA",
            NameShort_FI = "TAY",
        };
        public static School tays = new School
        {
            Name_EN = "Tampere University Hospital",
            Name_FI = "Tampereen yliopistollinen sairaala",
            NameShort_EN = "TAYS",
            NameShort_FI = "TAYS",
        };
        public static School tamk = new School
        {
            Name_EN = "Tampere University of Applied Sciences",
            Name_FI = "Tampereen ammattikorkeakoulu",
            NameShort_EN = "TAMK",
            NameShort_FI = "TAMK",
        };
        public static School takk = new School
        {
            Name_EN = "Tampere Adult Education Centre",
            Name_FI = "Tampereen Aikuiskoulutuskeskus",
            NameShort_EN = "TAKK",
            NameShort_FI = "TAKK",
        };
    }
}
