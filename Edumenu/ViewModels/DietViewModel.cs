using Edumenu.Models;
using System.Collections.ObjectModel;

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
