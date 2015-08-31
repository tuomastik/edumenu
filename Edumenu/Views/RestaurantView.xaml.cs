using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Edumenu.Views
{
    public sealed partial class RestaurantView : UserControl
    {
        public RestaurantView()
        {
            this.InitializeComponent();
        }

        private async void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string restaurantName = (((sender as Button).Content as Border).Child as TextBlock).Text;
            //Creating instance for the MessageDialog Class  
            //and passing the message in it's Constructor  
            MessageDialog msgbox = new MessageDialog("Haluatko varmasti avata ravintolan " + restaurantName + 
                "  verkkosivun selaimessa?");
            //Calling the Show method of MessageDialog class  
            //which will show the MessageBox  
            await msgbox.ShowAsync();
        }
    }
}
