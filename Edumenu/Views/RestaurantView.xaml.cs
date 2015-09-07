using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Edumenu.Views
{
    public sealed partial class RestaurantView : UserControl
    {
        public RestaurantView()
        {
            this.InitializeComponent();
        }

        

        private void Button_Tapped(object sender, RoutedEventArgs e)
        {
            string restaurantName = ((sender as Button).Content as TextBlock).Text;
            MessageBox.Show("Haluatko varmasti avata ravintolan " + restaurantName +
                " verkkosivun selaimessa?");
        }
    }
}
