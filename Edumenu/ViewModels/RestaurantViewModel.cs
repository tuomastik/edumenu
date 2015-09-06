using Edumenu.Models;
using System;
using System.Collections.ObjectModel;

namespace Edumenu.ViewModels
{

    public class RestaurantViewModel //: INotifyPropertyChanged
    {
        /// <summary>
        /// A collection for RestaurantViewModel objects.
        /// </summary>
        public ObservableCollection<Restaurant> restaurants { get; set; }
        public ObservableCollection<Restaurant> restaurants_tut { get; set; }

        public RestaurantViewModel()
        {   
            this.restaurants = new ObservableCollection<Restaurant>();
            this.restaurants_tut = new ObservableCollection<Restaurant>();

            restaurants.Add(new Restaurant()
            {
                name = "Newton",
                menuUrl = new Uri("http://www.juvenes.fi/tabid/337/moduleid/1149/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/ttykampus/newton.aspx"),
                menu = "Lorem ipsum dolor sit amet, qui ei dicta pertinax, postea adipiscing eum id. Nam solet interpretaris at. Labitur reprehendunt ea eos, etiam graeci scripta sea ad. Modo mnesarchum ut cum. Te vix illum option fabulas, no illud detracto sea, eos te summo exerci sapientem. Primis corrumpit complectitur pri ei, ad sed doctus laoreet tacimates, vim omnium delenit id. Te vel case praesent prodesset, in pertinax mediocritatem pro, ea viderer epicurei principes mel."
            });

            restaurants_tut.Add(new Restaurant()
            {
                name = "Zip",
                menu = "asd",
                menuUrl = new Uri("httP://moi"),
                homeUrl = new Uri("httP://JEE")
            });

            //restaurants.Add(new Restaurant()
            //{
            //    name = "Edison",
            //    menuUrl = new Uri(""),
            //    menu = "Eam ut inermis albucius, cu mei neglegentur contentiones. Vix ei exerci soluta graecis, nam principes sadipscing ea. In vel illum dignissim definitiones, cu per dolorem tacimates necessitatibus. Ei pri tation consul quidam, putent ullamcorper consequuntur in mel."
            //});
            //restaurants.Add(new Restaurant()
            //{
            //    name = "Newton",
            //    menuUrl = new Uri(""),
            //    menu = "Aliquip definitiones duo ne, vix mucius graecis facilisis ex, vix graeco nusquam accumsan in. Cu est quidam semper persecuti, pri an omnis graeco voluptua, tempor essent suscipiantur at sit. Ei laoreet qualisque tincidunt est, graece ponderum ei sea. Dicit aliquando ne eam, deserunt postulant instructior eos at, feugiat explicari cu sed. Vim an officiis prodesset honestatis, qui quidam aliquip bonorum eu. Per perfecto similique deterruisset ad, in pri iudicabit appellantur consequuntur."
            //});
        }
    }
}
