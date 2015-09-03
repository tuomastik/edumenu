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
            //Scroller.MouseMove += MyScroller_MouseMove;
        }

        //private double lastY = 0.0;
        //private double thresholdY = 5.0;
        //private double headerHeight = 58.0;
        //private void MyScroller_MouseMove(object sender, MouseEventArgs e)
        //{
        //    //System.Diagnostics.Debug.WriteLine("X="+e.GetPosition(Scroller).X.ToString());
        //    System.Diagnostics.Debug.WriteLine("Y="+e.GetPosition(Scroller).Y.ToString());
        //    double currentY = e.GetPosition(Scroller).Y;

        //    // Do nothing, if less than threshold value was scrolled
        //    if (Math.Abs(lastY - currentY) <= thresholdY)
        //    {
        //        return;
        //    }


        //    // If they scrolled down and are past the navbar, add class .nav-up.
        //    // This is necessary so you never see what is "behind" the navbar.
        //    if (currentY > lastY) // && currentY > headerHeight){
        //    {
        //        // Scroll Down
        //        MainPage.AnimateMove()
        //    //    $('header').removeClass('nav-down').addClass('nav-up');
        //    //} else {
        //    //    // Scroll Up
        //    //    if(st + $(window).height() < $(document).height()) {
        //    //        $('header').removeClass('nav-up').addClass('nav-down');
        //    //    }
        //    //}
    
        //    //lastScrollTop = st;
        //}



        private void Button_Tapped(object sender, RoutedEventArgs e)
        {
            string restaurantName = (((sender as Button).Content as Border).Child as TextBlock).Text;
            MessageBox.Show("Haluatko varmasti avata ravintolan " + restaurantName +
                " verkkosivun selaimessa?");
        }
    }
}
