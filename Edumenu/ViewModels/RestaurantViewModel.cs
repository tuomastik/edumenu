using Edumenu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edumenu.ViewModels
{

    public class RestaurantViewModel //: INotifyPropertyChanged
    {
        /// <summary>
        /// A collection for RestaurantViewModel objects.
        /// </summary>
        public ObservableCollection<Restaurant> Restaurants { get; private set; }

        public RestaurantViewModel()
        {            // Items to collect
            this.Restaurants = new ObservableCollection<Restaurant>();

            Restaurants.Add(new Restaurant()
            {
                Name = "Motivaattori",
                Menu = "Kuhaa jauhelihakastikkeessa makkaraperunoilla ja suolakurkkukastikkeella."
            });
            Restaurants.Add(new Restaurant()
            {
                Name = "Edison",
                Menu = "Ketsuppia sipulipedillä."
            });

        }
    }
}
