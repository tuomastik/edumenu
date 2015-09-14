using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edumenu.Models
{
    public class Diet
    {
        public string name_en { get; set; }
        public string name_fi { get; set; }
        public string nameShort { get; set; }
        public Uri iconPath { get; set; }

        public static Diet glutenFree = new Diet
        {
            name_en = "Gluten-free",
            name_fi = "Gluteeniton",
            nameShort = "G",
            iconPath = new Uri("Assets/DietIcons/G.png", UriKind.Relative)
        };
        public static Diet milkFree = new Diet
        {
            name_en = "Milk-free",
            name_fi = "Maidoton",
            nameShort = "M",
            iconPath = new Uri("Assets/DietIcons/M.png", UriKind.Relative)
        };
        public static Diet lactoseFree = new Diet
        {
            name_en = "Lactose-free",
            name_fi = "Laktoositon",
            nameShort = "L",
        };
        public static Diet lowLactose = new Diet
        {
            name_en = "Low-lactose",
            name_fi = "Vähälaktoosinen",
            nameShort = "VL",
        };
        public static Diet vegetarian = new Diet
        {
            name_en = "Vegetarian",
            name_fi = "Kasvis",
            nameShort = "KA",
        };
        public static Diet vegan = new Diet
        {
            name_en = "Vegan",
            name_fi = "Vegaani",
            nameShort = "VE",
        };
        public static Diet lite = new Diet
        {
            name_en = "Lite",
            name_fi = "Kevyt",
            nameShort = "LI",
        };
        public static Diet hot = new Diet
        {
            name_en = "Hot",
            name_fi = "Tulinen",
            nameShort = "HOT",
        };
        public static Diet withoutEgg = new Diet
        {
            name_en = "Without egg",
            name_fi = "Kananmunaton",
            nameShort = "K",
        };
        public static Diet withPork = new Diet
        {
            name_en = "With pork",
            name_fi = "Sisältää sianlihaa",
            nameShort = "SI",
        };
        public static Diet withGarlic = new Diet
        {
            name_en = "With garlic",
            name_fi = "Sisältää valkosipulia",
            nameShort = "VS",
        };
        public static Diet withSweetPepper = new Diet
        {
            name_en = "With sweet pepper",
            name_fi = "Sisältää paprikaa",
            nameShort = "PAPR",
        };
        public static Diet withCitrus = new Diet
        {
            name_en = "With citrus",
            name_fi = "Sisältää sitrushedelmää",
            nameShort = "SITRUS",
        };
        public static Diet withNuts = new Diet
        {
            name_en = "With nuts",
            name_fi = "Sisältää pähkinää",
            nameShort = "PÄ",
        };
        public static Diet withCelery = new Diet
        {
            name_en = "With celery",
            name_fi = "Sisältää selleriä",
            nameShort = "SE",
        };
        public static Diet withSoy = new Diet
        {
            name_en = "With soy",
            name_fi = "Sisältää soijaa",
            nameShort = "SO",
        };
    }
}
