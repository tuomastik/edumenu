using System;
using System.Collections.Generic;

namespace Edumenu.Models
{
    public class Diet
    {
        public string Name_EN { get; set; }
        public string Name_FI { get; set; }
        // Different companies use different short names for the same diets.
        // Add all possible short names to this list.
        public List<string> NameShort { get; set; }
        public Uri IconPath { get; set; }

        public static Diet glutenFree = new Diet
        {
            Name_EN = "Gluten-free",
            Name_FI = "Gluteeniton",
            NameShort = new List<string>() { "G" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/G.png")
        };
        public static Diet milkFree = new Diet
        {
            Name_EN = "Milk-free",
            Name_FI = "Maidoton",
            NameShort = new List<string>() { "M" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/M.png")
        };
        public static Diet lactoseFree = new Diet
        {
            Name_EN = "Lactose-free",
            Name_FI = "Laktoositon",
            NameShort = new List<string>() { "L" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/L.png")
        };
        public static Diet lowLactose = new Diet
        {
            Name_EN = "Low-lactose",
            Name_FI = "Vähälaktoosinen",
            NameShort = new List<string>() { "VL" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/VL.png")
        };
        public static Diet vegetarian = new Diet
        {
            Name_EN = "Vegetarian",
            Name_FI = "Kasvis",
            NameShort = new List<string>() { "KA" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/KA.png")
        };
        public static Diet vegan = new Diet
        {
            Name_EN = "Vegan",
            Name_FI = "Vegaani",
            NameShort = new List<string>() { "VE", "Veg" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/VE.png")
        };
        public static Diet lite = new Diet
        {
            Name_EN = "Lite",
            Name_FI = "Kevyt",
            NameShort = new List<string>() { "LI" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/Lite.png")
        };
        public static Diet hot = new Diet
        {
            Name_EN = "Hot",
            Name_FI = "Tulinen",
            NameShort = new List<string>() { "HOT" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/HOT.png")
        };
        public static Diet withoutEgg = new Diet
        {
            Name_EN = "Without egg",
            Name_FI = "Kananmunaton",
            NameShort = new List<string>() { "K" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/K.png")
        };
        public static Diet withPork = new Diet
        {
            Name_EN = "With pork",
            Name_FI = "Sisältää sianlihaa",
            NameShort = new List<string>() { "SI", "Sia" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/SI.png")
        };
        public static Diet withBeef = new Diet
        {
            Name_EN = "With beef",
            Name_FI = "Sisältää naudanlihaa",
            NameShort = new List<string>() { "Nau" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/Nau.png")
        };
        public static Diet withFish = new Diet
        {
            Name_EN = "With fish",
            Name_FI = "Sisältää kalaa",
            NameShort = new List<string>() { "Kal" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/Kal.png")
        };
        public static Diet withGarlic = new Diet
        {
            Name_EN = "With garlic",
            Name_FI = "Sisältää valkosipulia",
            NameShort = new List<string>() { "VS" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/VS.png")
        };
        public static Diet withSweetPepper = new Diet
        {
            Name_EN = "With sweet pepper",
            Name_FI = "Sisältää paprikaa",
            NameShort = new List<string>() { "PAPR" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/PAPR.png")
        };
        public static Diet withCitrus = new Diet
        {
            Name_EN = "With citrus",
            Name_FI = "Sisältää sitrushedelmää",
            NameShort = new List<string>() { "SITRUS" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/SITRUS.png")
        };
        public static Diet withNuts = new Diet
        {
            Name_EN = "With nuts",
            Name_FI = "Sisältää pähkinää",
            NameShort = new List<string>() { "PÄ", "Päh" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/pahkina.png")
        };
        public static Diet withCelery = new Diet
        {
            Name_EN = "With celery",
            Name_FI = "Sisältää selleriä",
            NameShort = new List<string>() { "Se", "Sel" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/SE.png")
        };
        public static Diet withSoy = new Diet
        {
            Name_EN = "With soy",
            Name_FI = "Sisältää soijaa",
            NameShort = new List<string>() { "SO" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/SOIJA.png")
        };
        public static Diet nutritionalGuidelines = new Diet
        {
            Name_EN = "According nutritional guidelines",
            Name_FI = "Suositusten mukainen",
            NameShort = new List<string>() { "SM", "*" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/SM.png")
        };
        public static Diet withAllergens = new Diet
        {
            Name_EN = "Generally with allergens",
            Name_FI = "Sisältää yleisimpiä allergeeneja",
            NameShort = new List<string>() { "A" },
            IconPath = new Uri("ms-appx:///Assets/DietIcons/A.png")
        };
    }
}
