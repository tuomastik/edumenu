using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows;

namespace Edumenu.Models
{
    public enum Company
    {
        Sodexo,
        Juvenes,
        Amica,
        Campusravita,
    }

    public class Restaurant
    {
        public string name { get; set; }
        public string menu { get; set; }
        public Company company { get; set; }
        public School school { get; set; }
        public Uri menuUrl { get; set; }
        public Uri homeUrl { get; set; }

        internal void ParseMenu(string sourceCode)
        {
            try
            {
                switch (company)
                {
                    // Use correct parser for correct restaurant company
                    case Company.Amica:
                        FetchAmica(sourceCode);
                        CleanAmica();
                        break;
                    case Company.Campusravita:
                        FetchCampusravita(sourceCode);
                        CleanCampusravita();
                        break;
                    case Company.Juvenes:
                        FetchJuvenes(sourceCode);
                        CleanJuvenes();
                        break;
                    case Company.Sodexo:
                        FetchSodexo(sourceCode);
                        CleanSodexo();
                        break;
                    default:
                        // If restaurant's company is not known,
                        // go to the next restaurant
                        return;
                }
            }
            catch
            {
                // Silently swallow an error condition and continue execution.
                // This is okay, because menu strings have already been initialized properly.
            }
            finally
            {
                UpdateProgress();
            }
        }

        internal void FetchAmica(string sourceCode)
        {
            string dayOfWeek = string.Empty;
            XElement rss = XElement.Parse(sourceCode);
            var dayNodes = rss.Element("channel").Elements("item");
            foreach (XElement dayNode in dayNodes)
            {
                dayOfWeek = dayNode.Element("title").ToString().ToLower();
                if (dayOfWeek.IndexOf(Globals.selectedDay,
                    StringComparison.OrdinalIgnoreCase) < 0)
                {
                    // Skip if not the selected day
                    continue;
                }
                menu = dayNode.Element("description").Value;
                break;
            }
        }

        private void CleanAmica()
        {
            menu = menu.Replace("&lt;br&gt;", string.Empty);
            menu = menu.Replace("<br>", string.Empty);

            // Remove whitespace inside curly and square brackets
            //  \s +     # Match whitespace
            //  (?=      # only if followed by...
            //  [^ ()] * # any number of characters except parentheses
            //  \)       # and a closing parenthesis
            //  )        # End of lookahead assertion
            menu = Regex.Replace(menu, "\\s+(?=[^()]*\\))", string.Empty);
            //dirtyMenu = Regex.Replace(dirtyMenu, "\\s+(?=[^[]]*\\])", string.Empty);

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
            menu = menu.Trim();
        }

        internal void FetchCampusravita(string sourceCode)
        {
           //
        }

        private void CleanCampusravita()
        {
            //
        }

        internal void FetchJuvenes(string sourceCode)
        {
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
                if (dayOfWeek.IndexOf(Globals.selectedDay,
                    StringComparison.OrdinalIgnoreCase) < 0)
                {
                    // Skip if not the selected day
                    continue;
                }
                menu = dayOfWeekAndMenu.Substring(
                    dayOfWeekAndMenu.IndexOf(dayOfWeekEndSequence) +
                    dayOfWeekEndSequence.Length);
                break;
            }
        }

        private void CleanJuvenes()
        {
            menu = menu.Replace("<ul>", string.Empty).Replace("</ul>", string.Empty);
            menu = menu.Replace("<strong>", Environment.NewLine);
            menu = menu.Replace("</strong>", string.Empty);
            // The case when one food consists of multiple elements
            menu = menu.Replace("</li><li>", Environment.NewLine);
            menu = menu.Replace("<li>", Environment.NewLine);
            menu = menu.Replace("</li>", Environment.NewLine);
            menu = menu.Replace("&nbsp;", " ");
            menu = menu.Trim();
        }

        internal void FetchSodexo(string sourceCode)
        {
            string dayOfWeekAndMenu = string.Empty;

            XElement rss = XElement.Parse(sourceCode);
            string allDaysAndMenus = rss.Element("channel")
                .Element("item").Element("description").Value;
            List<string> daySplits = new List<string>(sourceCode.Split(
                new string[] { "<h2>" }, StringSplitOptions.RemoveEmptyEntries));
            foreach (string daySplit in daySplits)
            {
                if (daySplit.IndexOf(Globals.selectedDay.ToString(),
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
                return;
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
                return;
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
        }

        private void CleanSodexo()
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


        }

        private void UpdateProgress()
        {
            Globals.nRestaurantsProcessed += 1;
            int totalSchoolRestaurants = 0;
            foreach (Restaurant restaurant in App.RestaurantViewModel.restaurantsAll)
            {
                // Count the number of restaurants belonging to the selected school
                if (restaurant.school.name_fi.Equals(school.name_fi))
                {
                    totalSchoolRestaurants += 1;
                }
            }
            if (totalSchoolRestaurants != 0) // Do not divide by zero
            {
                Globals.progress = (int)((double)Globals.nRestaurantsProcessed /
                    (double)totalSchoolRestaurants * 100);
            }
            if (Globals.nRestaurantsProcessed.Equals(totalSchoolRestaurants))
            {
                Globals.allRestaurantsProcessed = true;
            }
        }

    }
}
