using Edumenu.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Edumenu.ViewModels
{
    public class SchoolViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<School> schools { get; set; }
        AppSettings appSettings = new AppSettings();
        public string SelectedSchool
        {
            get
            {
                return GetSelectedSchool();
            }
            set
            {
                SelectSchool(value);
                OnPropertyChanged("SelectedSchool");
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

        public SchoolViewModel()
        {
            schools = new ObservableCollection<School>()
            {
                School.tut,
                School.uta,
                School.tays,
                School.tamk,
                School.takk
            };

            SelectedSchool = appSettings.SelectedSchool;
        }

        public void SelectSchool(string selectThisSchool)
        {
            // Set isSelected property of the school to be selected to true.
            // Set isSelected property of other schools to false.
            foreach (School school in schools)
            {
                if (school.NameShort_FI.ToLower().Equals(selectThisSchool.ToLower()) ||
                    school.NameShort_EN.ToLower().Equals(selectThisSchool.ToLower()))
                {
                    school.IsSelected = true;
                    continue;
                }
                school.IsSelected = false;
            }
            appSettings.SelectedSchool = selectThisSchool;
        }

        public string GetSelectedSchool()
        {
            // Returns the name of the selected school
            foreach (School school in schools)
            {
                if (school.IsSelected)
                {
                    return school.NameShort_FI;
                }
            }
            return string.Empty;
        }
    }
}
