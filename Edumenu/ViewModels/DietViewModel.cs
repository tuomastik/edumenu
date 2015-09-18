using Edumenu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edumenu.ViewModels
{
    public class DietViewModel
    {
        public ObservableCollection<Diet> diets { get; set; }

        public DietViewModel()
        {
            diets = new ObservableCollection<Diet>()
            {
                Diet.glutenFree,
                Diet.milkFree,
                Diet.lactoseFree,
                Diet.lowLactose,
                Diet.vegetarian,
                Diet.vegan,
                Diet.lite,
                Diet.hot,
                Diet.withoutEgg,
                Diet.withPork,
                Diet.withBeef,
                Diet.withFish,
                Diet.withGarlic,
                Diet.withSweetPepper,
                Diet.withCitrus,
                Diet.withNuts,
                Diet.withCelery,
                Diet.withSoy,
                Diet.withAllergens,
                Diet.nutritionalGuidelines
            };
        }
    }
}
