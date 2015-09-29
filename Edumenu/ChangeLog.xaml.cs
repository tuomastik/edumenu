using Edumenu.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Edumenu
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChangeLog : Page
    {
        public ChangeLog()
        {
            this.InitializeComponent();

            // Display application version in title
            PackageVersion pv = Package.Current.Id.Version;
            string version = new Version(Package.Current.Id.Version.Major,
                Package.Current.Id.Version.Minor,
                Package.Current.Id.Version.Revision,
                Package.Current.Id.Version.Build).ToString();
            changeLogTitle.Text = "Uutta versiossa " + version;

            Utils.ConfigureStatusBar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateWithDispatcher(Window.Current.Content as Frame, typeof(MainPage));
        }

        private async void NavigateWithDispatcher(Frame frame, Type typeOfPage)
        {
            if (frame == null)
            {
                return;
            }

            // Use Dispatcher to call Frame.Navigate in order to avoid
            // crash "(0xc0000005) Access violation"
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => frame.Navigate(typeOfPage, "ChangeLog"));
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
