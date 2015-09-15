using Edumenu.Models;
using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Edumenu.ViewModels
{

    public class RestaurantViewModel //: INotifyPropertyChanged
    {
        public ObservableCollection<Restaurant> restaurantsVisible { get; set; }
        public ObservableCollection<Restaurant> restaurantsAll { get; set; }

        public RestaurantViewModel()
        {
            this.restaurantsVisible = new ObservableCollection<Restaurant>();
            this.restaurantsAll = new ObservableCollection<Restaurant>();

            // Add all the restaurants from the Restaurant class to the observablecollection
            var bindingFlags = BindingFlags.Static | BindingFlags.Public;
            Type myType = typeof(Restaurant);
            FieldInfo[] fields = myType.GetFields(bindingFlags);
            foreach (FieldInfo field in fields)
            {
                restaurantsAll.Add((Restaurant)field.GetValue(this));
            }
        }
    }
}
