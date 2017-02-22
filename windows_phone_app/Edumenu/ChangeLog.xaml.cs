using Edumenu.Models;
using System;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Edumenu
{
    public sealed partial class ChangeLog : Page
    {
        public ChangeLog()
        {
            this.InitializeComponent();

            // Display application version in title
            PackageVersion pv = Package.Current.Id.Version;
            string version = new Version(Package.Current.Id.Version.Major,
                Package.Current.Id.Version.Minor,
                Package.Current.Id.Version.Build,
                Package.Current.Id.Version.Revision).ToString();
            changeLogTitle.Text = "Uutta versiossa " + version;

            Utils.ConfigureStatusBar();
        }

        private void ContinueToMainPage_Click(object sender, RoutedEventArgs e)
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
    }
}
