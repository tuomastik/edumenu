using Edumenu.Models;
using System;
using System.Collections.ObjectModel;
using System.Reflection;

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


            //BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly
            var bindingFlags = BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.Public;
            Type myType = typeof(Restaurant);
            PropertyInfo[] properties = myType.GetProperties(bindingFlags);
            foreach (PropertyInfo property in properties)
            {
                //MyComboBox.Items.Add(property.GetValue(myType, null).ToString());
            }


            //var fieldValues = Restaurant.GetType()
            //                     .GetFields(bindingFlags)
            //                     .Select(field => field.GetValue(foo))
            //                     .ToList();

            //-----------------------------
            // TUT
            //-----------------------------
            restaurantsAll.Add(new Restaurant
            {
                name = "Hertsi",
                company = Company.Sodexo,
                school = School.tut,
                menuUrl = new Uri("http://www.sodexo.fi/ruokalistat/rss/weekly_rss/12812/fi"),
                homeUrl = new Uri("http://www.sodexo.fi/tty-tietotalo/ravintola-hertsi"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Reaktori",
                company = Company.Amica,
                school = School.tut,
                menuUrl = new Uri("http://www.amica.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=0812&language=fi"),
                homeUrl = new Uri("http://www.amica.fi/ravintolat/ravintolat-kaupungeittain/tampere/reaktori/"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Newton",
                company = Company.Juvenes,
                school = School.tut,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/337/moduleid/1149/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/ttykampus/newton.aspx"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Newton - Rohee Xtra",
                company = Company.Juvenes,
                school = School.tut,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/348/moduleid/1187/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/ttykampus/newton/roheextra.aspx"),
            });
            //restaurantsAll.Add(new Restaurant
            //{
            //    name = "Café Konehuone - Såås Bar",
            //    company = Company.Juvenes,
            //    school = Schools.tut,
            //    menuUrl = new Uri("RSS FEED NOT PROVIDED (YET)"),
            //    homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/ttykampus/caf%C3%A9konehuone/s%C3%A5%C3%A5sbar.aspx"),
            //});
            //restaurantsAll.Add(new Restaurant
            //{
            //    name = "Café Konehuone - Fusion Kitchen",
            //    company = Company.Juvenes,
            //    school = Schools.tut,
            //    menuUrl = new Uri("RSS FEED NOT PROVIDED (YET)"),
            //    homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/ttykampus/caf%C3%A9konehuone/fusionkitchen.aspx"),
            //});
            //restaurantsAll.Add(new Restaurant
            //{
            //    name = "Bitti",
            //    company = Company.Sodexo,
            //    school = Schools.tut,
            //    menuUrl = new Uri("RSS FEED NOT PROVIDED (YET)"),
            //    homeUrl = new Uri("http://www.sodexo.fi/tty-tietotalo/kahvila-bitti"),
            //});
            //-----------------------------
            // UTA
            //-----------------------------
            restaurantsAll.Add(new Restaurant
            {
                name = "Yliopiston Ravintola",
                company = Company.Juvenes,
                school = School.uta,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/344/moduleid/1147/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/yliopistonravintola.aspx"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Minerva",
                company = Company.Amica,
                school = School.uta,
                menuUrl = new Uri("http://www.amica.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=0815&language=fi"),
                homeUrl = new Uri("http://www.amica.fi/minerva"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Linna",
                company = Company.Sodexo,
                school = School.uta,
                menuUrl = new Uri("http://www.sodexo.fi/ruokalistat/rss/weekly_rss/92/fi"),
                homeUrl = new Uri("http://www.sodexo.fi/linna"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Café & Lunch Pinni",
                company = Company.Juvenes,
                school = School.uta,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/304/moduleid/1184/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/caf%C3%A9lunchpinni.aspx"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Salaattibaari",
                company = Company.Juvenes,
                school = School.uta,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/346/moduleid/1253/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/yliopistonravintola/salaattibaari.aspx"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Fusion Kitchen",
                company = Company.Juvenes,
                school = School.uta,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/345/moduleid/1155/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/yliopistonravintola/fusionkitchen.aspx"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Rohee Xtra",
                company = Company.Juvenes,
                school = School.uta,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/1256/moduleid/3520/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/yliopistonravintola/roheextra.aspx"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Intro",
                company = Company.Juvenes,
                school = School.uta,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/347/moduleid/1148/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/yliopistonravintola/intro.aspx"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Café Alakuppila",
                company = Company.Juvenes,
                school = School.uta,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/302/moduleid/1854/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/caf%C3%A9alakuppila.aspx"),
            });
            //-----------------------------
            // TAYS
            //-----------------------------
            restaurantsAll.Add(new Restaurant
            {
                name = "Medica Bio",
                company = Company.Juvenes,
                school = School.tays,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/305/moduleid/1185/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/taykaupinkampus/medicabio.aspx"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Medica Arvo",
                company = Company.Juvenes,
                school = School.tays,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/306/moduleid/2223/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/taykaupinkampus/medicaarvo.aspx"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Medica Arvo - Fusion Kitchen",
                company = Company.Juvenes,
                school = School.tays,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/299/moduleid/2226/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/taykaupinkampus/medicaarvo/fusionkitchen.aspx"),
            });
            //-----------------------------
            // TAMK
            //-----------------------------
            restaurantsAll.Add(new Restaurant
            {
                name = "Campusravita",
                company = Company.Campusravita,
                school = School.tamk,
                menuUrl = new Uri("http://www.campusravita.fi/ruokalista"), //http://www.campusravita.fi/rss/index.php
                homeUrl = new Uri("http://www.campusravita.fi/"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Pirteria",
                company = Company.Amica,
                school = School.tamk,
                menuUrl = new Uri("http://www.amica.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=0823&language=fi"),
                homeUrl = new Uri("http://www.amica.fi/ravintolat/ravintolat-kaupungeittain/tampere/pirteria/"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Ziberia",
                company = Company.Juvenes,
                school = School.tamk,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/290/moduleid/1853/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/henkil%C3%B6st%C3%B6ravintolat/ziberia.aspx"),
            });
            //-----------------------------
            // TAKK
            //-----------------------------
            restaurantsAll.Add(new Restaurant
            {
                name = "Nasta",
                company = Company.Amica,
                school = School.takk,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/788/moduleid/1669/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/takknirva/nasta.aspx"),
            });
            restaurantsAll.Add(new Restaurant
            {
                name = "Nasta - Fusion Kitchen",
                company = Company.Juvenes,
                school = School.takk,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/782/moduleid/1650/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/takknirva/fusionkitchenpaninimeal.aspx"),
            });
            //restaurantsAll.Add(new Restaurant
            //{
            //    name = "Salad & Soup",
            //    company = Company.Juvenes,
            //    school = Schools.takk,
            //    menuUrl = new Uri("RSS FEED NOT PROVIDED (YET)"),
            //    homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/takknirva/saladsoup.aspx"),
            //});
            restaurantsAll.Add(new Restaurant
            {
                name = "Ratamo",
                company = Company.Juvenes,
                school = School.takk,
                menuUrl = new Uri("http://www.juvenes.fi/tabid/1123/moduleid/3173/RSS.aspx"),
                homeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/takkkeskusta/ratamo.aspx"),
            });
        }
    }
}
