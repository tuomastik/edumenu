using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;

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
                var query = rss.Elements().Elements();
                foreach (XElement ele in query) // Loop through days
                {
                    if (!ele.Name.LocalName.ToString().Contains("item"))
                    {
                        continue;
                    }
                    var query2 = ele.Elements();
                    foreach (var ele2 in query2) // Loop through day attributes
                    {
                        if (ele2.Name.LocalName.ToString().Contains("title"))
                        {
                            if (!ele2.Value.ToString().ToLower().Contains(Globals.selectedDay.ToLower()))
                            {
                                continue;
                            }
                            correctDayFound = true;
                        }
                        else if (ele2.Name.LocalName.ToString().Contains("description"))
                        {
                            restaurantMenu = ele2.Value.ToString();
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
        }

        internal void ParseCampusRavita(string p)
        {
            //throw new NotImplementedException();
        }

        internal void ParseJuvenes(string p)
        {
            //throw new NotImplementedException();
        }

        internal void ParseSodexo(string p)
        {
            //throw new NotImplementedException();
        }

        private string PurifyMenu(string dirtyMenu)
        {
            dirtyMenu = dirtyMenu.Replace("&lt;br&gt;", "");
            dirtyMenu = dirtyMenu.Replace("<br>", "");
            string cleanMenu = dirtyMenu.Trim();
            return cleanMenu;
        }

    }
}
