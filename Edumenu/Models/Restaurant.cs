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
    public class Restaurant
    {
        public enum Company
        {
            Sodexo,
            Juvenes,
            Amica,
            Campusravita
        }

        // Automatic properties
        public string Name { get; set; }
        public string Menu { get; set; }
        public Company Firm { get; set; }
        public School School { get; set; }
        public Uri MenuUrl { get; set; }
        public Uri HomeUrl { get; set; }

        //-----------------------------
        // TUT
        //-----------------------------
        public static Restaurant hertsi = new Restaurant()
        {
            Name = "Hertsi",
            Firm = Restaurant.Company.Sodexo,
            School = School.tut,
            MenuUrl = new Uri("http://www.sodexo.fi/ruokalistat/rss/weekly_rss/12812/fi"),
            HomeUrl = new Uri("http://www.sodexo.fi/tty-tietotalo/ravintola-hertsi"),
        };
        public static Restaurant reaktori = new Restaurant()
        {
            Name = "Reaktori",
            Firm = Restaurant.Company.Amica,
            School = School.tut,
            MenuUrl = new Uri("http://www.amica.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=0812&language=fi"),
            HomeUrl = new Uri("http://www.amica.fi/ravintolat/ravintolat-kaupungeittain/tampere/reaktori/"),
        };
        public static Restaurant newton = new Restaurant()
        {
            Name = "Newton",
            Firm = Restaurant.Company.Juvenes,
            School = School.tut,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/337/moduleid/1149/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/ttykampus/newton.aspx"),
        };
        public static Restaurant newtonRoheeXtra = new Restaurant()
        {
            Name = "Newton - Rohee Xtra",
            Firm = Restaurant.Company.Juvenes,
            School = School.tut,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/348/moduleid/1187/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/ttykampus/newton/roheextra.aspx"),
        };
        //public static Restaurant cafeKonehuoneSaasBar = new Restaurant()
        //{
        //    name = "Café Konehuone - Såås Bar",
        //    company = Restaurant.Company.Juvenes,
        //    school = School.tut,
        //    menuUrl = new Uri("RSS FEED NOT PROVIDED (YET)"),
        //    homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/ttykampus/caf%C3%A9konehuone/s%C3%A5%C3%A5sbar.aspx"),
        //};
        //public static Restaurant cafeKonehuoneFusionKitchen = new Restaurant()
        //{
        //    name = "Café Konehuone - Fusion Kitchen",
        //    company = Restaurant.Company.Juvenes,
        //    school = School.tut,
        //    menuUrl = new Uri("RSS FEED NOT PROVIDED (YET)"),
        //    homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/ttykampus/caf%C3%A9konehuone/fusionkitchen.aspx"),
        //};
        //public static Restaurant biitti = new Restaurant()
        //{
        //    name = "Bitti",
        //    company = Company.Sodexo,
        //    school = Restaurant.School.tut,
        //    menuUrl = new Uri("RSS FEED NOT PROVIDED (YET)"),
        //    homeUrl = new Uri("http://www.sodexo.fi/tty-tietotalo/kahvila-bitti"),
        //};
        //-----------------------------
        // UTA
        //-----------------------------
        public static Restaurant yliopistonRavintola = new Restaurant()
        {
            Name = "Yliopiston Ravintola",
            Firm = Restaurant.Company.Juvenes,
            School = School.uta,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/344/moduleid/1147/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/yliopistonravintola.aspx"),
        };
        public static Restaurant minerva = new Restaurant()
        {
            Name = "Minerva",
            Firm = Restaurant.Company.Amica,
            School = School.uta,
            MenuUrl = new Uri("http://www.amica.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=0815&language=fi"),
            HomeUrl = new Uri("http://www.amica.fi/minerva"),
        };
        public static Restaurant linna = new Restaurant()
        {
            Name = "Linna",
            Firm = Restaurant.Company.Sodexo,
            School = School.uta,
            MenuUrl = new Uri("http://www.sodexo.fi/ruokalistat/rss/weekly_rss/92/fi"),
            HomeUrl = new Uri("http://www.sodexo.fi/linna"),
        };
        public static Restaurant pinni = new Restaurant()
        {
            Name = "Café & Lunch Pinni",
            Firm = Restaurant.Company.Juvenes,
            School = School.uta,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/304/moduleid/1184/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/caf%C3%A9lunchpinni.aspx"),
        };
        public static Restaurant salaattibaari = new Restaurant()
        {
            Name = "Salaattibaari",
            Firm = Restaurant.Company.Juvenes,
            School = School.uta,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/346/moduleid/1253/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/yliopistonravintola/salaattibaari.aspx"),
        };
        public static Restaurant utaFusionKitchen = new Restaurant()
        {
            Name = "Fusion Kitchen",
            Firm = Restaurant.Company.Juvenes,
            School = School.uta,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/345/moduleid/1155/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/yliopistonravintola/fusionkitchen.aspx"),
        };
        public static Restaurant roheeXtra = new Restaurant()
        {
            Name = "Rohee Xtra",
            Firm = Restaurant.Company.Juvenes,
            School = School.uta,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/1256/moduleid/3520/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/yliopistonravintola/roheextra.aspx"),
        };
        public static Restaurant intro = new Restaurant()
        {
            Name = "Intro",
            Firm = Restaurant.Company.Juvenes,
            School = School.uta,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/347/moduleid/1148/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/yliopistonravintola/intro.aspx"),
        };
        public static Restaurant cafeAlakuppila = new Restaurant()
        {
            Name = "Café Alakuppila",
            Firm = Restaurant.Company.Juvenes,
            School = School.uta,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/302/moduleid/1854/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/caf%C3%A9alakuppila.aspx"),
        };
        //-----------------------------
        // TAYS
        //-----------------------------
        public static Restaurant medicaBio = new Restaurant()
        {
            Name = "Medica Bio",
            Firm = Restaurant.Company.Juvenes,
            School = School.tays,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/305/moduleid/1185/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/taykaupinkampus/medicabio.aspx"),
        };
        public static Restaurant medicaArvo = new Restaurant()
        {
            Name = "Medica Arvo",
            Firm = Restaurant.Company.Juvenes,
            School = School.tays,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/306/moduleid/2223/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/taykaupinkampus/medicaarvo.aspx"),
        };
        public static Restaurant medicaArvoFusionKitchen = new Restaurant()
        {
            Name = "Medica Arvo - Fusion Kitchen",
            Firm = Restaurant.Company.Juvenes,
            School = School.tays,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/299/moduleid/2226/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/taykaupinkampus/medicaarvo/fusionkitchen.aspx"),
        };
        //-----------------------------
        // TAMK
        //-----------------------------
        public static Restaurant campusravita = new Restaurant()
        {
            Name = "Campusravita",
            Firm = Restaurant.Company.Campusravita,
            School = School.tamk,
            MenuUrl = new Uri("http://www.campusravita.fi/ruokalista"), //http://www.campusravita.fi/rss/index.php
            HomeUrl = new Uri("http://www.campusravita.fi/"),
        };
        public static Restaurant pirteria = new Restaurant()
        {
            Name = "Pirteria",
            Firm = Restaurant.Company.Amica,
            School = School.tamk,
            MenuUrl = new Uri("http://www.amica.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=0823&language=fi"),
            HomeUrl = new Uri("http://www.amica.fi/ravintolat/ravintolat-kaupungeittain/tampere/pirteria/"),
        };
        public static Restaurant ziberia = new Restaurant()
        {
            Name = "Ziberia",
            Firm = Restaurant.Company.Juvenes,
            School = School.tamk,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/290/moduleid/1853/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/henkil%C3%B6st%C3%B6ravintolat/ziberia.aspx"),
        };
        //-----------------------------
        // TAKK
        //-----------------------------
        public static Restaurant nasta = new Restaurant()
        {
            Name = "Nasta",
            Firm = Restaurant.Company.Amica,
            School = School.takk,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/788/moduleid/1669/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/takknirva/nasta.aspx"),
        };
        public static Restaurant nastaFusionKitchen = new Restaurant()
        {
            Name = "Nasta - Fusion Kitchen",
            Firm = Restaurant.Company.Juvenes,
            School = School.takk,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/782/moduleid/1650/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/takknirva/fusionkitchenpaninimeal.aspx"),
        };
        //public static Restaurant saladSoup = new Restaurant()
        //{
        //    name = "Salad & Soup",
        //    company = Restaurant.Company.Juvenes,
        //    school = School.takk,
        //    menuUrl = new Uri("RSS FEED NOT PROVIDED (YET)"),
        //    homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/takknirva/saladsoup.aspx"),
        //};
        public static Restaurant ratamo = new Restaurant()
        {
            Name = "Ratamo",
            Firm = Restaurant.Company.Juvenes,
            School = School.takk,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/1123/moduleid/3173/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/takkkeskusta/ratamo.aspx"),
        };
        

        internal void ParseMenu(string sourceCode)
        {
            try
            {
                switch (Firm)
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
                Menu = dayNode.Element("description").Value;
                break;
            }
        }

        private void CleanAmica()
        {
            Menu = Menu.Replace("&lt;br&gt;", string.Empty);
            Menu = Menu.Replace("<br>", string.Empty);

            // Remove whitespace inside curly and square brackets
            //  \s +     # Match whitespace
            //  (?=      # only if followed by...
            //  [^ ()] * # any number of characters except parentheses
            //  \)       # and a closing parenthesis
            //  )        # End of lookahead assertion
            Menu = Regex.Replace(Menu, "\\s+(?=[^()]*\\))", string.Empty);
            //dirtyMenu = Regex.Replace(dirtyMenu, "\\s+(?=[^[]]*\\])", string.Empty);

            // Remove uppercase from menu type title and capitalize the first letter.
            // Split by newlines
            string[] lines = Menu.Split(new string[]
                { "\r\n", "\n" }, StringSplitOptions.None);
            Menu = String.Empty;
            foreach (string line in lines)
            {
                if (line.Count(f => f == ':').Equals(1))
                {
                    Menu += Utils.FirstCharToUpper(line) + Environment.NewLine;
                    continue;
                }
                Menu += line + Environment.NewLine;
            }
            Menu = Menu.Trim();
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
                Menu = dayOfWeekAndMenu.Substring(
                    dayOfWeekAndMenu.IndexOf(dayOfWeekEndSequence) +
                    dayOfWeekEndSequence.Length);
                break;
            }
        }

        private void CleanJuvenes()
        {
            Menu = Menu.Replace("<ul>", string.Empty).Replace("</ul>", string.Empty);
            Menu = Menu.Replace("<strong>", Environment.NewLine);
            Menu = Menu.Replace("</strong>", string.Empty);
            // The case when one food consists of multiple elements
            Menu = Menu.Replace("</li><li>", Environment.NewLine);
            Menu = Menu.Replace("<li>", Environment.NewLine);
            Menu = Menu.Replace("</li>", Environment.NewLine);
            Menu = Menu.Replace("&nbsp;", " ");
            Menu = Menu.Trim();
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
                Menu += item + " " + itemSymbol + Environment.NewLine + Environment.NewLine;
            }
            Menu += Environment.NewLine;
        }

        private void CleanSodexo()
        {
            // With some foods, menu item symbols are not inside <span> elements,
            // and thus there were no brackets added around the symbols in ParseSodexo.
            // Find those symbols and put brackets around them so that each company
            // menus are presented consistently.
            for (int i = 0; i < Menu.Length; i++)
            {
                if (!Menu[i].Equals(','))
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
                if (restaurant.School.name_fi.Equals(School.name_fi))
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
