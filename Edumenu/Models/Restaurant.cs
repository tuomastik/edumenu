using System;

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
        //public static Restaurant newtonRoheeXtra = new Restaurant()
        //{
        //    Name = "Newton - Rohee Xtra",
        //    Firm = Restaurant.Company.Juvenes,
        //    School = School.tut,
        //    MenuUrl = new Uri("http://www.juvenes.fi/tabid/348/moduleid/1187/RSS.aspx"),
        //    HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/ttykampus/newton/roheextra.aspx"),
        //};
        //public static Restaurant cafeKonehuoneSaasBar = new Restaurant()
        //{
        //    Name = "Såås Bar",
        //    Firm = Restaurant.Company.Juvenes,
        //    School = School.tut,
        //    MenuUrl = new Uri("http://www.juvenes.fi/tabid/1284/moduleid/3584/RSS.aspx"),
        //    HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/ttykampus/caf%C3%A9konehuone/s%C3%A5%C3%A5sbar.aspx"),
        //};
        //public static Restaurant cafeKonehuoneFusionKitchen = new Restaurant()
        //{
        //    Name = "Fusion Kitchen",
        //    Firm = Restaurant.Company.Juvenes,
        //    School = School.tut,
        //    MenuUrl = new Uri("http://www.juvenes.fi/tabid/1286/moduleid/3578/RSS.aspx"),
        //    HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/ttykampus/caf%C3%A9konehuone/fusionkitchen.aspx"),
        //};
        //public static Restaurant biitti = new Restaurant()
        //{
        //    Name = "Bitti",
        //    Firm = Company.Sodexo,
        //    School = Restaurant.School.tut,
        //    MenuUrl = new Uri("RSS FEED NOT PROVIDED (YET)"),
        //    HomeUrl = new Uri("http://www.sodexo.fi/tty-tietotalo/kahvila-bitti"),
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
        //public static Restaurant salaattibaari = new Restaurant()
        //{
        //    Name = "Salaattibaari",
        //    Firm = Restaurant.Company.Juvenes,
        //    School = School.uta,
        //    MenuUrl = new Uri("http://www.juvenes.fi/tabid/346/moduleid/1253/RSS.aspx"),
        //    HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/yliopistonravintola/salaattibaari.aspx"),
        //};
        //public static Restaurant utaFusionKitchen = new Restaurant()
        //{
        //    Name = "Fusion Kitchen",
        //    Firm = Restaurant.Company.Juvenes,
        //    School = School.uta,
        //    MenuUrl = new Uri("http://www.juvenes.fi/tabid/345/moduleid/1155/RSS.aspx"),
        //    HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/yliopistonravintola/fusionkitchen.aspx"),
        //};
        //public static Restaurant roheeXtra = new Restaurant()
        //{
        //    Name = "Rohee Xtra",
        //    Firm = Restaurant.Company.Juvenes,
        //    School = School.uta,
        //    MenuUrl = new Uri("http://www.juvenes.fi/tabid/1256/moduleid/3520/RSS.aspx"),
        //    HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/yliopistonravintola/roheextra.aspx"),
        //};
        //public static Restaurant intro = new Restaurant()
        //{
        //    Name = "Intro",
        //    Firm = Restaurant.Company.Juvenes,
        //    School = School.uta,
        //    MenuUrl = new Uri("http://www.juvenes.fi/tabid/347/moduleid/1148/RSS.aspx"),
        //    HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/tayp%C3%A4%C3%A4kampus/yliopistonravintola/intro.aspx"),
        //};
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
        //public static Restaurant medicaBio = new Restaurant()
        //{
        //    Name = "Medica Bio",
        //    Firm = Restaurant.Company.Juvenes,
        //    School = School.tays,
        //    MenuUrl = new Uri("http://www.juvenes.fi/tabid/305/moduleid/1185/RSS.aspx"),
        //    HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/taykaupinkampus/medicabio.aspx"),
        //};
        //public static Restaurant medicaArvo = new Restaurant()
        //{
        //    Name = "Medica Arvo",
        //    Firm = Restaurant.Company.Juvenes,
        //    School = School.tays,
        //    MenuUrl = new Uri("http://www.juvenes.fi/tabid/306/moduleid/2223/RSS.aspx"),
        //    HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/taykaupinkampus/medicaarvo.aspx"),
        //};
        //public static Restaurant medicaArvoFusionKitchen = new Restaurant()
        //{
        //    Name = "Medica Arvo - Fusion Kitchen",
        //    Firm = Restaurant.Company.Juvenes,
        //    School = School.tays,
        //    MenuUrl = new Uri("http://www.juvenes.fi/tabid/299/moduleid/2226/RSS.aspx"),
        //    HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/taykaupinkampus/medicaarvo/fusionkitchen.aspx"),
        //};
        //-----------------------------
        // TAMK
        //-----------------------------
        //public static Restaurant campusravita = new Restaurant()
        //{
        //    Name = "Campusravita",
        //    Firm = Restaurant.Company.Campusravita,
        //    School = School.tamk,
        //    MenuUrl = new Uri("http://www.campusravita.fi/ruokalista"), //http://www.campusravita.fi/rss/index.php
        //    HomeUrl = new Uri("http://www.campusravita.fi/"),
        //};
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
        public static Restaurant live = new Restaurant()
        {
            Name = "Live",
            Firm = Restaurant.Company.Juvenes,
            School = School.tamk,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/1142/moduleid/3288/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/henkil%C3%B6st%C3%B6ravintolat/live.aspx"),
        };
        public static Restaurant liveFusionKitchen = new Restaurant()
        {
            Name = "Live - Fusion Kitchen",
            Firm = Restaurant.Company.Juvenes,
            School = School.tamk,
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/1184/moduleid/3354/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/henkil%C3%B6st%C3%B6ravintolat/live/fusionkitchen.aspx"),
        };
        //-----------------------------
        // TAKK
        //-----------------------------
        public static Restaurant nasta = new Restaurant()
        {
            Name = "Nasta",
            Firm = Restaurant.Company.Juvenes,
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
            MenuUrl = new Uri("http://www.juvenes.fi/tabid/1628/moduleid/3173/RSS.aspx"),
            HomeUrl = new Uri("http://www.juvenes.fi/fi-fi/ravintolatjakahvilat/opiskelijaravintolat/takkkeskusta/ratamo.aspx"),
        };
    }
}
