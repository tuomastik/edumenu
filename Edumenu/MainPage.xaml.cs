using Edumenu.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Edumenu
{
    public sealed partial class MainPage : Page
    {
        private RestaurantViewModel _restaurantViewModel
        {
            get
            {
                return this.DataContext as RestaurantViewModel;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Set the data context of the listbox control to the sample data
            this.DataContext = App.RestaurantViewModel;

            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void GoMainButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            slidein.GoToMenuState(ActiveState.Main);
        }

        private void HamburgerButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (slidein.contentSelector.SelectedIndex.Equals(1))
            {
                slidein.GoToMenuState(ActiveState.Left);
            }
            else if (slidein.contentSelector.SelectedIndex.Equals(0))
            {
                slidein.GoToMenuState(ActiveState.Main);
            }
        }



        //public void AnimateHeight(FrameworkElement element, UInt16 fromHeight, UInt16 toHeight)
        //{
        //    Storyboard sb = new Storyboard();

        //    DoubleAnimation changeHeight = new DoubleAnimation();
        //    changeHeight.From = fromHeight;
        //    changeHeight.To = toHeight;
        //    changeHeight.Duration = TimeSpan.FromMilliseconds(700);
        //    changeHeight.EnableDependentAnimation = true;

        //    QuadraticEase easing = new QuadraticEase()
        //    {
        //        EasingMode = EasingMode.EaseOut
        //    };
        //    changeHeight.EasingFunction = easing;

        //    Storyboard.SetTargetProperty(changeHeight, "(UIElement.Height)");
        //    Storyboard.SetTarget(changeHeight, SchoolsMenu);

        //    sb.Children.Add(changeHeight);
        //    sb.Begin();
        //}

        private void ExpandOrShrinkSchoolsMenu()
        {
            //UInt16 minHeight = 2;
            //UInt16 maxHeight = 120;

            //if (SchoolsMenu.Height.Equals(minHeight))
            //{
            //    AnimateHeight(SchoolsMenu, minHeight, maxHeight);
            //}
            //else if (SchoolsMenu.Height.Equals(maxHeight))
            //{
            //    AnimateHeight(SchoolsMenu, maxHeight, minHeight);
            //}
        }

        private void SchoolText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ExpandOrShrinkSchoolsMenu();
        }
    }
}
