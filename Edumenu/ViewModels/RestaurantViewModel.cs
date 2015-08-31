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
                Menu = "Lorem ipsum dolor sit amet, qui ei dicta pertinax, postea adipiscing eum id. Nam solet interpretaris at. Labitur reprehendunt ea eos, etiam graeci scripta sea ad. Modo mnesarchum ut cum. Te vix illum option fabulas, no illud detracto sea, eos te summo exerci sapientem. Primis corrumpit complectitur pri ei, ad sed doctus laoreet tacimates, vim omnium delenit id. Te vel case praesent prodesset, in pertinax mediocritatem pro, ea viderer epicurei principes mel."
            });
            Restaurants.Add(new Restaurant()
            {
                Name = "Edison",
                Menu = "Eam ut inermis albucius, cu mei neglegentur contentiones. Vix ei exerci soluta graecis, nam principes sadipscing ea. In vel illum dignissim definitiones, cu per dolorem tacimates necessitatibus. Ei pri tation consul quidam, putent ullamcorper consequuntur in mel."
            });
            Restaurants.Add(new Restaurant()
            {
                Name = "Newton",
                Menu = "Aliquip definitiones duo ne, vix mucius graecis facilisis ex, vix graeco nusquam accumsan in. Cu est quidam semper persecuti, pri an omnis graeco voluptua, tempor essent suscipiantur at sit. Ei laoreet qualisque tincidunt est, graece ponderum ei sea. Dicit aliquando ne eam, deserunt postulant instructior eos at, feugiat explicari cu sed. Vim an officiis prodesset honestatis, qui quidam aliquip bonorum eu. Per perfecto similique deterruisset ad, in pri iudicabit appellantur consequuntur."
            });
        }
    }
}
