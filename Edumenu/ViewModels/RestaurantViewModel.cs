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

            // Restaurants will be presented in the same order that
            // they are added to the restaurantsAll

            // TUT
            restaurantsAll.Add(Restaurant.hertsi);
            restaurantsAll.Add(Restaurant.reaktori);
            restaurantsAll.Add(Restaurant.newton);
            restaurantsAll.Add(Restaurant.newtonRoheeXtra);
            // UTA
            restaurantsAll.Add(Restaurant.yliopistonRavintola);
            restaurantsAll.Add(Restaurant.minerva);
            restaurantsAll.Add(Restaurant.linna);
            restaurantsAll.Add(Restaurant.pinni);
            restaurantsAll.Add(Restaurant.salaattibaari);
            restaurantsAll.Add(Restaurant.utaFusionKitchen);
            restaurantsAll.Add(Restaurant.roheeXtra);
            restaurantsAll.Add(Restaurant.intro);
            restaurantsAll.Add(Restaurant.cafeAlakuppila);
            // TAYS
            restaurantsAll.Add(Restaurant.medicaBio);
            restaurantsAll.Add(Restaurant.medicaArvo);
            restaurantsAll.Add(Restaurant.medicaArvoFusionKitchen);
            // TAMK
            restaurantsAll.Add(Restaurant.campusravita);
            restaurantsAll.Add(Restaurant.pirteria);
            restaurantsAll.Add(Restaurant.ziberia);
            // TAKK
            restaurantsAll.Add(Restaurant.nasta);
            restaurantsAll.Add(Restaurant.nastaFusionKitchen);
            restaurantsAll.Add(Restaurant.ratamo);

        }
    }
}
