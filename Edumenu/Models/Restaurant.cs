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
                string dayOfWeek = "";

                XElement rss = XElement.Parse(sourceCode);
                var dayNodes = rss.Element("channel").Elements("item");
                foreach (XElement dayNode in dayNodes)
                {
                    dayOfWeek = dayNode.Element("title").ToString().ToLower();
                    if (!dayOfWeek.Contains(Globals.selectedDay.ToLower()))
                    {
                        // Skip if not the selected day
                        continue;
                    }
                    menu = CleanMenu(dayNode.Element("description").Value.ToString());
                    break;
                }
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

        internal void ParseCampusRavita(string p)
        {
            for (int i=0; i< 10*10; ++i)
            {
                continue;
            }
            UpdateProgress();
        }

        internal void ParseJuvenes(string sourceCode)
        {
            try
            {
                string dayOfWeek = "";
                string dayOfWeekAndMenu = "";
                string dayOfWeekEndSequence = "</strong></p>";

                XElement rss = XElement.Parse(sourceCode);
                var dayNodes = rss.Element("channel").Elements("item");
                foreach (XElement dayNode in dayNodes)
                {
                    dayOfWeekAndMenu = dayNode.Element("description").Value.ToString();
                    if (!dayOfWeekAndMenu.Contains(dayOfWeekEndSequence))
                    {
                        // Skip if not able to resolve the day of week
                        continue;
                    }
                    dayOfWeek = dayOfWeekAndMenu.Substring(0,
                        dayOfWeekAndMenu.IndexOf(dayOfWeekEndSequence) +
                        dayOfWeekEndSequence.Length);
                    if (!dayOfWeek.ToLower().Contains(Globals.selectedDay.ToLower()))
                    {
                        // Skip if not the selected day
                        continue;
                    }
                    menu = CleanMenu(dayOfWeekAndMenu.Substring(
                        dayOfWeekAndMenu.IndexOf(dayOfWeekEndSequence) +
                        dayOfWeekEndSequence.Length));
                    break;
                }
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

        internal void ParseSodexo(string p)
        {
            for (int i = 0; i < 10 * 10 * 10 * 10; ++i)
            {
                continue;
            }
            UpdateProgress();
        }

        private string CleanMenu(string dirtyMenu)
        {
            switch (company)
            {
                case Company.Amica:
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

                    // Remove uppercase from menu type title
                    // Split by newlines
                    string[] lines = dirtyMenu.Split(new string[]
                        { "\r\n", "\n" }, StringSplitOptions.None);
                    dirtyMenu = String.Empty;
                    foreach (string line in lines)
                    {
                        if (line.Count(f => f == ':').Equals(1))
                        {
                            dirtyMenu += FirstCharToUpper(line);
                            continue;
                        }
                        dirtyMenu += line + Environment.NewLine;
                    }
                    break;
                case Company.Juvenes:
                    dirtyMenu = dirtyMenu.Replace("<ul>", "").Replace("</ul>", "");
                    dirtyMenu = dirtyMenu.Replace("<strong>", Environment.NewLine);
                    dirtyMenu = dirtyMenu.Replace("</strong>", "");
                    // The case when one food consists of multiple elements
                    dirtyMenu = dirtyMenu.Replace("</li><li>", Environment.NewLine);
                    dirtyMenu = dirtyMenu.Replace("<li>", Environment.NewLine);
                    dirtyMenu = dirtyMenu.Replace("</li>", Environment.NewLine);
                    dirtyMenu = dirtyMenu.Replace("&nbsp;", " ").Trim();
                    break;
            }
            string cleanMenu = dirtyMenu.Trim();
            return cleanMenu;
        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return input;
            }
            return input.First().ToString().ToUpper()
                + input.Substring(1).ToLower() + Environment.NewLine;
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
