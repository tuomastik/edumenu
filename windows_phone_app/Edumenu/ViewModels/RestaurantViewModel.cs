﻿using Edumenu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Edumenu.ViewModels
{

    public class RestaurantViewModel //: INotifyPropertyChanged
    {
        AppSettings appSettings = new AppSettings();
        public ObservableCollection<Restaurant> restaurantsVisible { get; set; }
        public ObservableCollection<Restaurant> restaurantsAll { get; set; }

        public RestaurantViewModel()
        {
            this.restaurantsVisible = new ObservableCollection<Restaurant>();
            this.restaurantsAll = new ObservableCollection<Restaurant>();

            // Restaurants will be presented in the same order that
            // they are added to the restaurantsAll

            // TUT
            restaurantsAll.Add(Restaurant.reaktori);
            restaurantsAll.Add(Restaurant.hertsi);
            restaurantsAll.Add(Restaurant.newton);
            //restaurantsAll.Add(Restaurant.newtonRoheeXtra);            // RSS feed not available
            //restaurantsAll.Add(Restaurant.cafeKonehuoneSaasBar);       // RSS feed not available
            //restaurantsAll.Add(Restaurant.cafeKonehuoneFusionKitchen); // RSS feed not available
            //restaurantsAll.Add(Restaurant.biitti);                     // RSS feed not available
            // UTA
            restaurantsAll.Add(Restaurant.yliopistonRavintola);
            restaurantsAll.Add(Restaurant.minerva);
            restaurantsAll.Add(Restaurant.linna);
            restaurantsAll.Add(Restaurant.pinni);
            //restaurantsAll.Add(Restaurant.salaattibaari);    // RSS feed not available
            //restaurantsAll.Add(Restaurant.utaFusionKitchen); // RSS feed not available
            //restaurantsAll.Add(Restaurant.roheeXtra);        // RSS feed not available
            //restaurantsAll.Add(Restaurant.intro);            // RSS feed not available
            restaurantsAll.Add(Restaurant.cafeAlakuppila);
            // TAYS
            //restaurantsAll.Add(Restaurant.medicaBio);               // RSS feed not available
            //restaurantsAll.Add(Restaurant.medicaArvo);              // RSS feed not available
            //restaurantsAll.Add(Restaurant.medicaArvoFusionKitchen); // RSS feed not available
            // TAMK
            //restaurantsAll.Add(Restaurant.campusravita); // Parser not implemented
            restaurantsAll.Add(Restaurant.pirteria);
            restaurantsAll.Add(Restaurant.ziberia);
            restaurantsAll.Add(Restaurant.live);
            restaurantsAll.Add(Restaurant.liveFusionKitchen);
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
                        restaurant.Menu = HideDietsIfNeeded(restaurant.Menu);
                        break;
                    case Restaurant.Company.Campusravita:
                        restaurant.Menu = CleanCampusravita(FetchCampusravita(sourceCode));
                        restaurant.Menu = HideDietsIfNeeded(restaurant.Menu);
                        break;
                    case Restaurant.Company.Juvenes:
                        restaurant.Menu = CleanJuvenes(FetchJuvenes(sourceCode));
                        restaurant.Menu = HideDietsIfNeeded(restaurant.Menu);
                        break;
                    case Restaurant.Company.Sodexo:
                        restaurant.Menu = CleanSodexo(FetchSodexo(sourceCode));
                        restaurant.Menu = HideDietsIfNeeded(restaurant.Menu);
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
            menu = menu.Replace("<p>", Environment.NewLine);
            menu = menu.Replace("</p>", string.Empty);
            menu = menu.Replace("<br />", Environment.NewLine + "- ");
            menu = menu.Replace("&nbsp;", string.Empty);
            menu = menu.Replace("- jälkiruoka:", "Jälkiruoka:");
            menu = menu.Replace("- Jälkiruoka:", "Jälkiruoka:");
            menu = menu.Replace(" 5,95/ opisk. 2,60", string.Empty);
            menu = menu.Replace(" 4,75 /opisk. 2,27", string.Empty);
            menu = menu.Replace(" 7,55/opisk. 4,95", string.Empty);
            menu = menu.Replace(" 7,55/ opisk. 4,95", string.Empty);
            menu = menu.Replace(" 1,90", string.Empty);
            menu = menu.Replace(" :", ":");
            menu = RemoveWhiteSpaceInsideBrackets(menu);
            menu = CapitalizeFirstLetterOfMenuTypeTitle(menu);
            // Replace 3 or more newlines with 2 newlines
            menu = Regex.Replace(menu, @"(\r\n){3,}", Environment.NewLine + Environment.NewLine);
            // Replace 3 or more newlines with 2 newlines
            menu = Regex.Replace(menu, @"(:\r\n\r\n)", ":" + Environment.NewLine + "- ");
            // Replace 2 or more spaces with 1 space
            menu = Regex.Replace(menu, @"( ){2,}", " ");
            menu = menu.Replace("()", "").Replace("( )", "");
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
            // Replace 2 or more spaces with 1 space
            menu = Regex.Replace(menu, @"( ){2,}", " ");
            menu = RemoveWhiteSpaceInsideBrackets(menu);
            menu = menu.Replace("()", "").Replace("( )", "");
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
            // TODO: With some foods, menu item symbols are not inside <span> elements,
            // and thus there were no brackets added around the symbols in ParseSodexo.
            // Find those symbols and put brackets around them so that each company
            // menus are presented consistently.
            //for (int i = 0; i < menu.Length; i++)
            //{
            //    if (!menu[i].Equals(','))
            //    {
            //        continue;
            //    }
            //}
            menu = menu.Replace("&nbsp;", " ").Replace("amp;", "");
            menu = menu.Replace(" )", ")").Replace("( ", "(");
            menu = menu.Replace(" ,", ",").Replace(", ", ",");
            // Replace 3 or more newlines with 2 newlines
            menu = Regex.Replace(menu, @"(\r\n){3,}", Environment.NewLine + Environment.NewLine);
            // Replace 2 or more spaces with 1 space
            menu = Regex.Replace(menu, @"( ){2,}", " ");
            menu = menu.Replace("()", "").Replace("( )", "");
            return menu.Trim();
        }

        private string HideDietsIfNeeded(string menu)
        {
            if (!appSettings.ShowDiets)
            {
                return Regex.Replace(menu, @" ?\(.*?\)", string.Empty);
            }
            return menu;
        }

        public void InitializeMenus(string text)
        {
            foreach (Restaurant restaurant in restaurantsAll)
            {
                restaurant.Menu = text;
            }
        }

        internal void UpdateVisibleRestaurants()
        {
            restaurantsVisible.Clear();
            foreach (Restaurant restaurant in restaurantsAll)
            {
                // Skip the restaurants which do not correspond to the selected school
                if (!App.SchoolViewModel.GetSelectedSchool().Equals(restaurant.School.NameShort_FI))
                {
                    continue;
                }
                else
                {
                    restaurantsVisible.Add(restaurant);
                }
            }
        }
    }
}
