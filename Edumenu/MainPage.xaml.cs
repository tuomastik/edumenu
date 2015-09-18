// <copyright file="MainPage.xaml.cs" company="Tuomas Tikkanen">
//     Copyright (c) Tuomas Tikkanen. All rights reserved.
// </copyright>

namespace Edumenu
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using Edumenu.Models;
    using Microsoft.Devices;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using Microsoft.Phone.Tasks;

    public partial class MainPage : PhoneApplicationPage
    {
        /*
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Class level variables
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        */

        // Progress tracking
        private static int progress; // Total progress 0...1
        private static int restaurantsProcessed; // Restaurants processed per school
        private static bool allRestaurantsProcessed; // Can we exit the background thread?

        // Moving between views
        private int viewChangeThreshold = 30;
        private double initialPosition;
        private bool viewMoved = false;

        // Hiding header when scrolling down
        private bool headerVisible = true;
        private bool animationRunning = false;

        // Other
        private BackgroundWorker backgroundWorker = new BackgroundWorker();
        private AppSettings appSettings = new AppSettings();
        private WebBrowserTask webBrowserTask = new WebBrowserTask();

        /*
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Constructor
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        */
        public MainPage()
        {
            this.InitializeComponent();

            // Set data contexts
            Scroller.DataContext = App.RestaurantViewModel;
            DaysOfWeekItemsControl.DataContext = App.DayViewModel;
            SchoolsItemsControl.DataContext = App.SchoolViewModel;
            SelectedSchoolHeader.DataContext = App.SchoolViewModel;

            // Define and launch BackgroundWorker
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            if (this.backgroundWorker.IsBusy != true)
            {
                this.backgroundWorker.RunWorkerAsync();
            }
        }

        /*
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Fetching restaurant menus with BackgroundWorker
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        */
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            restaurantsProcessed = 0;
            allRestaurantsProcessed = false;
            progress = 0;
            worker.ReportProgress(0);

            foreach (Restaurant restaurant in App.RestaurantViewModel.restaurantsAll)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                if (!App.SchoolViewModel.GetSelectedSchool().Equals(restaurant.School.NameShort_FI))
                {
                    // Skip the restaurants which do not belong to the selected school
                    continue;
                }
                else
                {
                    // Restaurant belongs to the selected school
                    WebClient client = new WebClient();
                    client.Encoding = Encoding.UTF8;
                    client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(this.MenuDownloadCompleted);
                    client.DownloadStringAsync(restaurant.MenuUrl, restaurant);
                }
            }

            // Wait until all restaurant menus have been downloaded and parsed.
            int lastProgress = progress;

            // Max sleep time 200 * 0,05 s = 10 s
            for (int i = 1; i <= 200; i++)
            {
                if (!lastProgress.Equals(progress))
                {
                    worker.ReportProgress(progress);
                    lastProgress = progress;
                }

                if (allRestaurantsProcessed)
                {
                    worker.ReportProgress(100);
                    break;
                }
                else
                {
                    // Continue sleeping for another 100 ms = 0,05 s
                    Thread.Sleep(50);
                }
            }
        }

        private void MenuDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || string.IsNullOrEmpty(e.Result))
            {
                return;
            }

            Restaurant restaurant = e.UserState as Restaurant;
            App.RestaurantViewModel.ParseMenu(e.Result, restaurant);
        }

        public static void UpdateProgress(Restaurant currentRestaurant)
        {
            restaurantsProcessed += 1;
            int totalSchoolRestaurants = 0;
            foreach (Restaurant r in App.RestaurantViewModel.restaurantsAll)
            {
                // Count the number of restaurants belonging to the selected school
                if (r.School.Name_FI.Equals(currentRestaurant.School.Name_FI))
                {
                    totalSchoolRestaurants += 1;
                }
            }

            // Do not divide by zero
            if (totalSchoolRestaurants != 0) 
            {
                progress = (int)((double)restaurantsProcessed /
                    (double)totalSchoolRestaurants * 100);
            }

            if (restaurantsProcessed.Equals(totalSchoolRestaurants))
            {
                allRestaurantsProcessed = true;
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProBar.Visibility = Visibility.Visible;
            ProBar.Value = (double)e.ProgressPercentage;
            var progressIndicator = new ProgressIndicator
            {
                Text = "Ladataan ruokalistoja...",
                IsVisible = true,
                IsIndeterminate = false,        
            };
            SystemTray.SetProgressIndicator(this, progressIndicator);
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                System.Diagnostics.Debug.WriteLine("Canceled!");
            }
            else if (!(e.Error == null))
            {
                System.Diagnostics.Debug.WriteLine("Error: " + e.Error.Message);
            }
            else
            {
                // Background worker completed successfully
                ProBar.Visibility = Visibility.Collapsed;
                var progressIndicator = new ProgressIndicator
                {
                    Text = string.Empty,
                    IsVisible = false,
                    Value = 0.0,
                };
                SystemTray.SetProgressIndicator(this, progressIndicator);

                App.RestaurantViewModel.restaurantsVisible.Clear();
                foreach (Restaurant restaurant in App.RestaurantViewModel.restaurantsAll)
                {
                    // Skip the restaurants which do not correspond to the selected school
                    if (!App.SchoolViewModel.GetSelectedSchool().Equals(restaurant.School.NameShort_FI))
                    {
                        continue;
                    }
                    else
                    {
                        App.RestaurantViewModel.restaurantsVisible.Add(restaurant);
                    }
                }
            }
        }

        /*
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        The code implementing moving between left, middle and right view
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        */
        private void OpenClose_Left(object sender, RoutedEventArgs e)
        {
            var left = Canvas.GetLeft(ChildCanvas);
            if (left > -this.viewChangeThreshold)
            {
                this.MoveViewWindow(-LeftView.Width);
            }
            else
            {
                this.MoveViewWindow(0);
            }
        }

        private void OpenClose_Right(object sender, RoutedEventArgs e)
        {
            var left = Canvas.GetLeft(ChildCanvas);
            if (left > (-LeftView.Width - this.viewChangeThreshold))
            {
                this.MoveViewWindow(-LeftView.Width - RightView.Width);
            }
            else
            {
                this.MoveViewWindow(-LeftView.Width);
            }
        }

        private void MoveViewWindow(double left)
        {
            this.viewMoved = true;
            ////((Storyboard)ParentCanvas.Resources["ChangeViewAnimation"]).SkipToFill();
            ((DoubleAnimation)((Storyboard)ParentCanvas.Resources["ChangeViewAnimation"]).Children[0]).To = left;
            ((Storyboard)ParentCanvas.Resources["ChangeViewAnimation"]).Begin();
        }

        private void ParentCanvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (e.DeltaManipulation.Translation.X != 0)
            {
                Canvas.SetLeft(
                    this.ChildCanvas,
                    Math.Min(Math.Max(-(LeftView.Width + RightView.Width), Canvas.GetLeft(this.ChildCanvas) + e.DeltaManipulation.Translation.X), 0));
            }
        }

        private void ParentCanvas_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            this.viewMoved = false;
            this.initialPosition = Canvas.GetLeft(this.ChildCanvas);
        }

        private void ParentCanvas_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            var left = Canvas.GetLeft(ChildCanvas);
            if (this.viewMoved)
            { 
                return;
            }

            if (Math.Abs(this.initialPosition - left) < this.viewChangeThreshold)
            {
                // Bouncing back
                this.MoveViewWindow(this.initialPosition);
                return;
            }

            // Change of state
            if (this.initialPosition - left > 0)
            {
                // Slide to the left
                if (this.initialPosition > -LeftView.Width)
                {
                    this.MoveViewWindow(-LeftView.Width);
                }
                else
                {
                    this.MoveViewWindow(-(LeftView.Width + RightView.Width));
                }
            }
            else
            {
                // Slide to the right
                if (this.initialPosition < -RightView.Width)
                {
                    this.MoveViewWindow(-RightView.Width);
                }
                else
                {
                    this.MoveViewWindow(0);
                }
            }
        }

        /*
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        ScrollViewer - Header interactions
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        
        When the ScrollViewer is loaded, start to listen its scrollbar's events.
        Hide the header when scrolling down, and show the header when scrolling up.
        
        */
        private void Scroller_Loaded(object sender, RoutedEventArgs e)
        {
            this.GetScrollBar(this.Scroller);
        }

        private void GetScrollBar(DependencyObject parent)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                ScrollBar scrollBar = child as ScrollBar;
                if (scrollBar == null)
                {
                    this.GetScrollBar(child);
                }
                else
                {
                    string name = scrollBar.Name;

                    if (name == "VerticalScrollBar")
                    {
                        scrollBar.ValueChanged += this.OnScrollbarValueChanged;
                    }
                }
            }
        }

        private void OnScrollbarValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ////System.Diagnostics.Debug.WriteLine(e.OldValue.ToString());
            ////System.Diagnostics.Debug.WriteLine(e.NewValue.ToString());
            ////System.Diagnostics.Debug.WriteLine("Animation running: " + animationRunning.ToString());
            ////System.Diagnostics.Debug.WriteLine("Header visible: " + headerVisible.ToString());

            if (e.NewValue > e.OldValue &&
                e.NewValue > HeaderContainer.Height &&
                !this.animationRunning && this.headerVisible)
            {
                // Scroll Down
                this.headerVisible = false;
                Storyboard scrollDown = this.AnimateMove(HeaderContainer, 0, -HeaderContainer.Height, 300);
                scrollDown.Completed += new EventHandler(this.HeaderAnimate_Completed);
                scrollDown.Begin();
                this.animationRunning = true;
                ////System.Diagnostics.Debug.WriteLine("User scrolled down!");
            }
            else if (e.NewValue < e.OldValue &&
                     !this.animationRunning && !this.headerVisible)
            {
                // Scroll up
                this.headerVisible = true;
                Storyboard scrollUp = this.AnimateMove(HeaderContainer, -HeaderContainer.Height, 0, 300);
                scrollUp.Completed += new EventHandler(this.HeaderAnimate_Completed);
                scrollUp.Begin();
                ////System.Diagnostics.Debug.WriteLine("User scrolled up!");
            }
            ////System.Diagnostics.Debug.WriteLine("---------------------");
        }

        private Storyboard AnimateMove(
            FrameworkElement fe,
            double from,
            double to,
            int durationMs,
            string easingType = "exponential")
        {
            // Initialize a new instance of the CompositeTransform which allows 
            // addition of multiple different transforms
            fe.RenderTransform = new CompositeTransform();

            // Create the timeline
            var animation = new DoubleAnimationUsingKeyFrames();            

            // Add key frames to the timeline
            // Start
            animation.KeyFrames.Add(new EasingDoubleKeyFrame
            {
                KeyTime = TimeSpan.Zero,
                Value = from
            });

            // Stop
            if (easingType.Equals("elastic"))
            {
                ElasticEase elasticEase = new ElasticEase()
                {
                    Oscillations = 1,
                    Springiness = 7,
                    EasingMode = EasingMode.EaseOut
                };
                animation.KeyFrames.Add(new EasingDoubleKeyFrame
                {
                    EasingFunction = elasticEase,
                    KeyTime = TimeSpan.FromMilliseconds(durationMs),
                    Value = to
                });
            }
            else
            {
                ExponentialEase expEase = new ExponentialEase()
                {
                    EasingMode = EasingMode.EaseOut
                };
                animation.KeyFrames.Add(new EasingDoubleKeyFrame
                {
                    EasingFunction = expEase,
                    KeyTime = TimeSpan.FromMilliseconds(durationMs),
                    Value = to
                });
            }

            Storyboard.SetTargetProperty(
                animation,
                new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateY)"));
            Storyboard.SetTarget(animation, fe);

            // Create the storyboard
            var storyboard = new Storyboard();

            // Add the timeline to the storyboard
            storyboard.Children.Add(animation);

            return storyboard;
        }

        private void HeaderAnimate_Completed(object sender, EventArgs e)
        {
            this.animationRunning = false;
        }

        /*
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Open restaurant website pop up
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        */
        private void RestaurantName_Clicked(object sender, RoutedEventArgs e)
        {
            // Do not fire up the MessageBox if user is scrolling horizontally
            if (Canvas.GetLeft(this.ChildCanvas) != -LeftView.Width)
            {
                return;
            }

            WebsitePrompt_PopUp.IsOpen = true;

            // Begin animation
            Storyboard showPopUp = this.AnimateMove(WebsitePrompt_PopUp, -450, -60, 900, "elastic");
            showPopUp.Begin();

            // Vibrate
            VibrateController testVibrateController = VibrateController.Default;
            testVibrateController.Start(TimeSpan.FromSeconds(0.07));

            // Darken the background
            PopUpOverlayGrid.Visibility = Visibility.Visible;

            // Set properties
            Restaurant clickedRestaurant = (Restaurant)(sender as Button).DataContext;
            this.webBrowserTask.Uri = clickedRestaurant.HomeUrl;
            navigationPrompt_textblock.Text = "Haluatko varmasti poistua sovelluksesta " +
                "ja avata ravintolan " + clickedRestaurant.Name + " verkkosivun selaimessa?";
        }

        private void PopUpButton_Continue_Click(object sender, RoutedEventArgs e)
        {
            this.DismissWebsitePrompt();
            this.webBrowserTask.Show();
        }

        private void PopUpButton_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DismissWebsitePrompt();
        }

        // Hide the popup when the back key is pressed
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (this.WebsitePrompt_PopUp.IsOpen)
            {
                this.DismissWebsitePrompt();
                e.Cancel = true;
            }
        }

        private void HidePopUp_Completed(object sender, EventArgs e)
        {
            WebsitePrompt_PopUp.IsOpen = false;
        }

        private void DismissWebsitePrompt()
        {
            // Begin animation
            Storyboard hidePopUp = this.AnimateMove(WebsitePrompt_PopUp, -60, -450, 300);
            hidePopUp.Completed += new EventHandler(this.HidePopUp_Completed);
            hidePopUp.Begin();

            // Remove background darkener
            PopUpOverlayGrid.Visibility = Visibility.Collapsed;
        }

        /*
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Other click events
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        */
        private void DayOfWeek_Clicked(object sender, RoutedEventArgs e)
        {
            // Do not fire up the event if user is scrolling horizontally
            if (Canvas.GetLeft(this.ChildCanvas) != 0)
            {
                return;
            }

            Day clickedDay = (Day)(sender as Button).DataContext;
            if (clickedDay.Name.ToLower().Equals(App.DayViewModel.GetSelectedDay().ToLower()))
            {
                return;
            }

            App.DayViewModel.SelectDay(clickedDay.Name);
            if (this.backgroundWorker.IsBusy != true)
            {
                this.backgroundWorker.RunWorkerAsync();
            }
        }

        private void School_Clicked(object sender, RoutedEventArgs e)
        {
            // Do not fire up the event if user is scrolling horizontally
            if (Canvas.GetLeft(this.ChildCanvas) != -(LeftView.Width + RightView.Width))
            {
                return;
            }

            School clickedSchool = (School)(sender as Button).DataContext;
            if (clickedSchool.NameShort_FI.ToLower().Equals(App.SchoolViewModel.GetSelectedSchool().ToLower()) ||
                clickedSchool.NameShort_EN.ToLower().Equals(App.SchoolViewModel.GetSelectedSchool().ToLower()))
            {
                return;
            }

            App.SchoolViewModel.SelectedSchool = clickedSchool.NameShort_FI;
            if (this.backgroundWorker.IsBusy != true)
            {
                this.backgroundWorker.RunWorkerAsync();
            }
        }

        private void Diets_Clicked(object sender, RoutedEventArgs e)
        {
            Storyboard hideMainPage = this.AnimateMove(ParentCanvas, 0, 800, 150);
            hideMainPage.Begin();
            hideMainPage.Completed += new EventHandler(this.HideMainPage_Completed);
        }

        private void HideMainPage_Completed(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Diets.xaml", UriKind.Relative));
        }
    }
}