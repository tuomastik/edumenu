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

        internal void ParseAmica(string sourceCode)
        {
            try
            {
                // Initialize
                string restaurantMenu = "";
                bool correctDayFound = false;

                XElement rss = XElement.Parse(sourceCode);
                var days = rss.Elements().Elements();
                foreach (XElement day in days) // Loop through days
                {
                    if (!day.Name.LocalName.ToString().Contains("item"))
                    {
                        continue;
                    }
                    var dayAttributes = day.Elements();
                    foreach (var dayAttribute in dayAttributes) // Loop through day attributes
                    {
                        if (dayAttribute.Name.LocalName.ToString().Contains("title"))
                        {
                            if (!dayAttribute.Value.ToString().ToLower().Contains(Globals.selectedDay.ToLower()))
                            {
                                // Skip if not the selected day
                                break;
                            }
                            correctDayFound = true;
                        }
                        else if (dayAttribute.Name.LocalName.ToString().Contains("description"))
                        {
                            restaurantMenu = dayAttribute.Value.ToString();
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (correctDayFound)
                    {
                        break;
                    }
                };
                this.menu = PurifyMenu(restaurantMenu);
            }
            catch
            {
                // Silently swallow an error condition and continue execution.
                // This is okay, because menu strings were initialized properly before execution.
            }
            finally
            {
                UpdateProgress();
            }
        }

        private void UpdateProgress()
        {
            Globals.nRestaurantsProcessed += 1;
            int totalSchoolRestaurants = 0;
            foreach (Restaurant restaurant in App.RestaurantViewModel.restaurantsAll)
            { 
                if (restaurant.school.name_fi.Equals(school.name_fi))
                {
                    totalSchoolRestaurants += 1;
                }
            }
            if (totalSchoolRestaurants != 0) // Do not divide by zero
            { 
                Globals.progress = (int)((double)Globals.nRestaurantsProcessed / (double)totalSchoolRestaurants * 100);
            }
            if (Globals.nRestaurantsProcessed.Equals(totalSchoolRestaurants))
            {
                Globals.allRestaurantsProcessed = true;
            }
        }

        internal void ParseCampusRavita(string p)
        {
            for (int i=0; i< 10*10; ++i)
            {
                continue;
            }
            UpdateProgress();
        }

        internal void ParseJuvenes(string p)
        {
            for (int i = 0; i < 10 *10*10* 10 * 10; ++i)
            {
                continue;
            }
            UpdateProgress();
        }

        internal void ParseSodexo(string p)
        {
            for (int i = 0; i < 10 * 10 * 10 * 10; ++i)
            {
                continue;
            }
            UpdateProgress();
        }

        private string PurifyMenu(string dirtyMenu)
        {
            dirtyMenu = dirtyMenu.Replace("&lt;br&gt;", "");
            dirtyMenu = dirtyMenu.Replace("<br>", "");
            // Remove whitespace inside curly and square brackets
            //  \s +     # Match whitespace
            //  (?=      # only if followed by...
            //  [^ ()] * # any number of characters except parentheses
            //  \)       # and a closing parenthesis
            //  )        # End of lookahead assertion
            dirtyMenu = Regex.Replace(dirtyMenu, "\\s+(?=[^()]*\\))", "");
            //dirtyMenu = Regex.Replace(dirtyMenu, "\\s+(?=[^[]]*\\])", "");
            string cleanMenu = dirtyMenu.Trim();
            return cleanMenu;
        }

    }
}
