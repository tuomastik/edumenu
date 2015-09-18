using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Edumenu
{
    public partial class Diets : PhoneApplicationPage
    {
        public Diets()
        {
            InitializeComponent();

            // Set data context
            Scroller.DataContext = App.DietViewModel;
        }

        private void Back_Clicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}