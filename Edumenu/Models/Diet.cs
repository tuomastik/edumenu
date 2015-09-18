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
            IconPath = new Uri("Assets/DietIcons/G.png", UriKind.Relative)
        };
        public static Diet milkFree = new Diet
        {
            Name_EN = "Milk-free",
            Name_FI = "Maidoton",
            NameShort = new List<string>() { "M" },
            IconPath = new Uri("Assets/DietIcons/M.png", UriKind.Relative)
        };
        public static Diet lactoseFree = new Diet
        {
            Name_EN = "Lactose-free",
            Name_FI = "Laktoositon",
            NameShort = new List<string>() { "L" },
            IconPath = new Uri("Assets/DietIcons/L.png", UriKind.Relative)
        };
        public static Diet lowLactose = new Diet
        {
            Name_EN = "Low-lactose",
            Name_FI = "Vähälaktoosinen",
            NameShort = new List<string>() { "VL" },
            IconPath = new Uri("Assets/DietIcons/VL.png", UriKind.Relative)
        };
        public static Diet vegetarian = new Diet
        {
            Name_EN = "Vegetarian",
            Name_FI = "Kasvis",
            NameShort = new List<string>() { "KA" },
            IconPath = new Uri("Assets/DietIcons/KA.png", UriKind.Relative)
        };
        public static Diet vegan = new Diet
        {
            Name_EN = "Vegan",
            Name_FI = "Vegaani",
            NameShort = new List<string>() { "VE", "Veg" },
            IconPath = new Uri("Assets/DietIcons/VE.png", UriKind.Relative)
        };
        public static Diet lite = new Diet
        {
            Name_EN = "Lite",
            Name_FI = "Kevyt",
            NameShort = new List<string>() { "LI" },
            IconPath = new Uri("Assets/DietIcons/Lite.png", UriKind.Relative)
        };
        public static Diet hot = new Diet
        {
            Name_EN = "Hot",
            Name_FI = "Tulinen",
            NameShort = new List<string>() { "HOT" },
            IconPath = new Uri("Assets/DietIcons/HOT.png", UriKind.Relative)
        };
        public static Diet withoutEgg = new Diet
        {
            Name_EN = "Without egg",
            Name_FI = "Kananmunaton",
            NameShort = new List<string>() { "K" },
            IconPath = new Uri("Assets/DietIcons/K.png", UriKind.Relative)
        };
        public static Diet withPork = new Diet
        {
            Name_EN = "With pork",
            Name_FI = "Sisältää sianlihaa",
            NameShort = new List<string>() { "SI", "Sia" },
            IconPath = new Uri("Assets/DietIcons/SI.png", UriKind.Relative)
        };
        public static Diet withBeef = new Diet
        {
            Name_EN = "With beef",
            Name_FI = "Sisältää naudanlihaa",
            NameShort = new List<string>() { "Nau" },
            IconPath = new Uri("Assets/DietIcons/Nau.png", UriKind.Relative)
        };
        public static Diet withFish = new Diet
        {
            Name_EN = "With fish",
            Name_FI = "Sisältää kalaa",
            NameShort = new List<string>() { "Kal" },
            IconPath = new Uri("Assets/DietIcons/Kal.png", UriKind.Relative)
        };
        public static Diet withGarlic = new Diet
        {
            Name_EN = "With garlic",
            Name_FI = "Sisältää valkosipulia",
            NameShort = new List<string>() { "VS" },
            IconPath = new Uri("Assets/DietIcons/VS.png", UriKind.Relative)
        };
        public static Diet withSweetPepper = new Diet
        {
            Name_EN = "With sweet pepper",
            Name_FI = "Sisältää paprikaa",
            NameShort = new List<string>() { "PAPR" },
            IconPath = new Uri("Assets/DietIcons/PAPR.png", UriKind.Relative)
        };
        public static Diet withCitrus = new Diet
        {
            Name_EN = "With citrus",
            Name_FI = "Sisältää sitrushedelmää",
            NameShort = new List<string>() { "SITRUS" },
            IconPath = new Uri("Assets/DietIcons/SITRUS.png", UriKind.Relative)
        };
        public static Diet withNuts = new Diet
        {
            Name_EN = "With nuts",
            Name_FI = "Sisältää pähkinää",
            NameShort = new List<string>() { "PÄ", "Päh" },
            IconPath = new Uri("Assets/DietIcons/pahkina.png", UriKind.Relative)
        };
        public static Diet withCelery = new Diet
        {
            Name_EN = "With celery",
            Name_FI = "Sisältää selleriä",
            NameShort = new List<string>() { "Se", "Sel" },
            IconPath = new Uri("Assets/DietIcons/SE.png", UriKind.Relative)
        };
        public static Diet withSoy = new Diet
        {
            Name_EN = "With soy",
            Name_FI = "Sisältää soijaa",
            NameShort = new List<string>() { "SO" },
            IconPath = new Uri("Assets/DietIcons/SOIJA.png", UriKind.Relative)
        };
        public static Diet nutritionalGuidelines = new Diet
        {
            Name_EN = "According nutritional guidelines",
            Name_FI = "Suositusten mukainen",
            NameShort = new List<string>() { "SM", "*" },
            IconPath = new Uri("Assets/DietIcons/SM.png", UriKind.Relative)
        };
        public static Diet withAllergens = new Diet
        {
            Name_EN = "Generally with allergens",
            Name_FI = "Sisältää yleisimpiä allergeeneja",
            NameShort = new List<string>() { "A" },
            IconPath = new Uri("Assets/DietIcons/A.png", UriKind.Relative)
        };
    }
}
