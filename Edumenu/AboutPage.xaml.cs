using Edumenu.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.Security.ExchangeActiveSyncProvisioning;
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
    public sealed partial class AboutPage : Page
    {
        Version version;

        public AboutPage()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            // Display application version
            PackageVersion pv = Package.Current.Id.Version;
            version = new Version(Package.Current.Id.Version.Major,
                Package.Current.Id.Version.Minor,
                Package.Current.Id.Version.Build,
                Package.Current.Id.Version.Revision);
            AppVersion_TextBlock.Text = "Versio: " + version.ToString();

            Utils.ConfigureStatusBar();
        }

        private void Back_Clicked(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void ReviewApp_Clicked(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(
                new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
        }

        private async void Review_Clicked(object sender, RoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Address = "tuomas.tikkanen@hotmail.com"
            };

            // Create mail object
            EmailMessage mail = new EmailMessage();

            // Define body text
            mail.Subject = "Palautetta Edumenusta";
            string body = "[Palautteesi tähän]";
            var easClientDeviceInformation = new EasClientDeviceInformation();
            string deviceRmCode = easClientDeviceInformation.SystemProductName;
            mail.Body = body + Environment.NewLine + Environment.NewLine +
                "---------------------------------" + Environment.NewLine +
                "Laite: " + deviceRmCode + Environment.NewLine +
                "Edumenun versio: " + version.ToString() + Environment.NewLine +
                "---------------------------------";

            // Add recipients to the mail object
            mail.To.Add(sendTo);

            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private async void OpenInStore_Clicked(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(
                new Uri("ms-windows-store:navigate?appid=" + CurrentApp.AppId));
        }
    }
}
