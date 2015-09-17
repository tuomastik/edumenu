using Edumenu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

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


        public void ParseMenu(string sourceCode, Restaurant restaurant)
        {
            try
            {
                switch (restaurant.Firm)
                {
                    // Use correct parser for correct restaurant company
                    case Restaurant.Company.Amica:
                        restaurant.Menu = CleanAmica(ParseAmica(sourceCode));
                        break;
                    case Restaurant.Company.Campusravita:
                        restaurant.Menu = CleanCampusravita(FetchCampusravita(sourceCode));
                        break;
                    case Restaurant.Company.Juvenes:
                        restaurant.Menu = CleanJuvenes(FetchJuvenes(sourceCode));
                        break;
                    case Restaurant.Company.Sodexo:
                        restaurant.Menu = CleanSodexo(FetchSodexo(sourceCode));
                        break;
                    default:
                        // If restaurant's company is not known,
                        // go to the next restaurant
                        return;
                }
            }
            catch
            {
            }
            finally
            {
                if (string.IsNullOrWhiteSpace(restaurant.Menu))
                {
                    restaurant.Menu = "Ei ruokalistaa saatavilla";
                }
                MainPage.UpdateProgress(restaurant);
            }
        }

        private string ParseAmica(string sourceCode)
        {
            string menu = string.Empty;
            string dayOfWeek = string.Empty;
            XElement rss = XElement.Parse(sourceCode);
            var dayNodes = rss.Element("channel").Elements("item");
            foreach (XElement dayNode in dayNodes)
            {
                dayOfWeek = dayNode.Element("title").ToString().ToLower();
                if (dayOfWeek.IndexOf(App.DayViewModel.GetSelectedDay(),
                    StringComparison.OrdinalIgnoreCase) < 0)
                {
                    // Skip if not the selected day
                    continue;
                }
                menu = dayNode.Element("description").Value;
                if (!string.IsNullOrWhiteSpace(menu))
                {
                    return menu;
                }
            }
            return string.Empty;
        }

        private string CleanAmica(string menu)
        {
            menu = menu.Replace("&lt;br&gt;", string.Empty);
            menu = menu.Replace("<br>", string.Empty);
            menu = RemoveWhiteSpaceInsideBrackets(menu);
            menu = CapitalizeFirstLetterOfMenuTypeTitle(menu);
            return menu.Trim();
        }

        private string CapitalizeFirstLetterOfMenuTypeTitle(string menu)
        {
            // Remove uppercase from menu type title and capitalize the first letter.
            // Split by newlines
            string[] lines = menu.Split(new string[]
                { "\r\n", "\n" }, StringSplitOptions.None);
            menu = String.Empty;
            foreach (string line in lines)
            {
                if (line.Count(f => f == ':').Equals(1))
                {
                    menu += Utils.FirstCharToUpper(line) + Environment.NewLine;
                    continue;
                }
                menu += line + Environment.NewLine;
            }
            return menu;
        }

        private string RemoveWhiteSpaceInsideBrackets(string menu)
        {
            // Remove whitespace inside curly and square brackets
            //  \s +     # Match whitespace
            //  (?=      # only if followed by...
            //  [^ ()] * # any number of characters except parentheses
            //  \)       # and a closing parenthesis
            //  )        # End of lookahead assertion
            menu = Regex.Replace(menu, "\\s+(?=[^()]*\\))", string.Empty);
            return Regex.Replace(menu, "\\s+(?=[^[]]*\\])", string.Empty);
        }

        internal string FetchCampusravita(string sourceCode)
        {
            return string.Empty;
        }

        private string CleanCampusravita(string menu)
        {
            return string.Empty;
        }

        internal string FetchJuvenes(string sourceCode)
        {
            string menu = string.Empty;
            string dayOfWeek = string.Empty;
            string dayOfWeekAndMenu = string.Empty;
            string dayOfWeekEndSequence = "</strong></p>";

            XElement rss = XElement.Parse(sourceCode);
            var dayNodes = rss.Element("channel").Elements("item");
            foreach (XElement dayNode in dayNodes)
            {
                dayOfWeekAndMenu = dayNode.Element("description").Value;
                if (dayOfWeekAndMenu.IndexOf(dayOfWeekEndSequence,
                    StringComparison.OrdinalIgnoreCase) < 0)
                {
                    // Skip if not able to resolve the day of week
                    continue;
                }
                dayOfWeek = dayOfWeekAndMenu.Substring(0,
                    dayOfWeekAndMenu.IndexOf(dayOfWeekEndSequence) +
                    dayOfWeekEndSequence.Length);
                if (dayOfWeek.IndexOf(App.DayViewModel.GetSelectedDay(),
                    StringComparison.OrdinalIgnoreCase) < 0)
                {
                    // Skip if not the selected day
                    continue;
                }
                menu = dayOfWeekAndMenu.Substring(
                    dayOfWeekAndMenu.IndexOf(dayOfWeekEndSequence) +
                    dayOfWeekEndSequence.Length);
                if (!string.IsNullOrWhiteSpace(menu))
                {
                    return menu;
                }
            }
            return string.Empty;
        }

        private string CleanJuvenes(string menu)
        {
            menu = menu.Replace("<ul>", string.Empty).Replace("</ul>", string.Empty);
            menu = menu.Replace("<strong>", Environment.NewLine);
            menu = menu.Replace("</strong>", string.Empty);
            // The case when one food consists of multiple elements
            menu = menu.Replace("</li><li>", Environment.NewLine);
            menu = menu.Replace("<li>", Environment.NewLine);
            menu = menu.Replace("</li>", Environment.NewLine);
            menu = menu.Replace("&nbsp;", " ");
            // Replace 3 or more newlines with 2 newlines
            menu = Regex.Replace(menu, @"(\r\n){3,}", Environment.NewLine + Environment.NewLine);
            menu = RemoveWhiteSpaceInsideBrackets(menu);
            return menu.Trim();
        }

        private string FetchSodexo(string sourceCode)
        {
            string menu = string.Empty;
            string dayOfWeekAndMenu = string.Empty;

            XElement rss = XElement.Parse(sourceCode);
            string allDaysAndMenus = rss.Element("channel")
                .Element("item").Element("description").Value;
            List<string> daySplits = new List<string>(sourceCode.Split(
                new string[] { "<h2>" }, StringSplitOptions.RemoveEmptyEntries));
            foreach (string daySplit in daySplits)
            {
                if (daySplit.IndexOf(App.DayViewModel.GetSelectedDay(),
                    StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Selected day found
                    dayOfWeekAndMenu = daySplit;
                    break;
                }
            }
            if (dayOfWeekAndMenu.Equals(string.Empty))
            {
                // Selected day not found
                return string.Empty;
            }
            List<string> menuItems = new List<string>(dayOfWeekAndMenu.Split(
                new string[] { "<strong>" }, StringSplitOptions.RemoveEmptyEntries));
            List<string> menuItemSymbols = new List<string>(dayOfWeekAndMenu.Split(
                new string[] { "<span>" }, StringSplitOptions.RemoveEmptyEntries));
            string item = string.Empty;
            string itemSymbol = string.Empty;

            // Make sure that there are as many menuItems as there are menuItemSymbols
            if (!menuItems.Count().Equals(menuItemSymbols.Count()))
            {
                return string.Empty;
            }

            for (int a = 1; a < menuItems.Count(); a += 2)
            {
                item = menuItems[a].Substring(0, menuItems[a].IndexOf(
                    "</strong>", StringComparison.OrdinalIgnoreCase));
                int spanEndIndex = menuItemSymbols[a].IndexOf("</span>",
                    StringComparison.OrdinalIgnoreCase);
                if (spanEndIndex >= 0)
                {
                    itemSymbol = "(" + menuItemSymbols[a].Substring(0, spanEndIndex) + ")";
                    if (itemSymbol.Equals("()"))
                    {
                        itemSymbol = string.Empty;
                    }
                }
                menu += item + " " + itemSymbol + Environment.NewLine + Environment.NewLine;
            }
            menu += Environment.NewLine;
            return menu;
        }

        private string CleanSodexo(string menu)
        {
            // With some foods, menu item symbols are not inside <span> elements,
            // and thus there were no brackets added around the symbols in ParseSodexo.
            // Find those symbols and put brackets around them so that each company
            // menus are presented consistently.
            for (int i = 0; i < menu.Length; i++)
            {
                if (!menu[i].Equals(','))
                {
                    continue;
                }
                //if (!menu)
                //School instance = new School();
                //Type type = typeof(School);

                //Dictionary<string, object> properties = new Dictionary<string, object>();
                //foreach (System.Reflection.PropertyInfo prop in type.GetProperties())
                //{ 
                //    properties.Add(prop.Name, prop.GetValue(instance));
                //    Debug.WriteLine(prop.Name.ToString());
                //}
            }
            menu = menu.Replace("amp;", "");
            return menu.Trim();
        }

    }
}
