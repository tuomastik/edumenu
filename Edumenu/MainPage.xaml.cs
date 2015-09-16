using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using Edumenu.Models;
using System.Text;
using System.Globalization;
using System.Threading;
using Microsoft.Phone.Shell;
using System.Collections.Generic;
using Microsoft.Devices;
using Microsoft.Phone.Tasks;

namespace Edumenu
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Class level variables
        private BackgroundWorker bw = new BackgroundWorker();
        AppSettings appSettings = new AppSettings();
        WebBrowserTask webBrowserTask = new WebBrowserTask();
        public static List<string> daysOfWeek = new List<string>()
        {
            Utils.FirstCharToUpper(new CultureInfo("fi-FI").DateTimeFormat.GetDayName(DayOfWeek.Monday)),
            Utils.FirstCharToUpper(new CultureInfo("fi-FI").DateTimeFormat.GetDayName(DayOfWeek.Tuesday)),
            Utils.FirstCharToUpper(new CultureInfo("fi-FI").DateTimeFormat.GetDayName(DayOfWeek.Wednesday)),
            Utils.FirstCharToUpper(new CultureInfo("fi-FI").DateTimeFormat.GetDayName(DayOfWeek.Thursday)),
            Utils.FirstCharToUpper(new CultureInfo("fi-FI").DateTimeFormat.GetDayName(DayOfWeek.Friday)),
            Utils.FirstCharToUpper(new CultureInfo("fi-FI").DateTimeFormat.GetDayName(DayOfWeek.Saturday))
        };


        public MainPage()
        {
            InitializeComponent();
            // Set data contexts
            SelectedSchoolHeader.DataContext = appSettings;
            this.DataContext = App.RestaurantViewModel;
            DaysOfWeekItemsControl.DataContext = daysOfWeek;
            //DietIcon.DataContext = Diet.glutenFree;
            // BackgroundWorker
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
        }





        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Fetching restaurant menus with BackgroundWorker
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Globals.nRestaurantsProcessed = 0;
            Globals.allRestaurantsProcessed = false;
            Globals.progress = 0;
            worker.ReportProgress(0);

            foreach(Restaurant restaurant in App.RestaurantViewModel.restaurantsAll)
            {
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                if (!appSettings.SelectedSchool.Equals(restaurant.School.NameShort_FI))
                {
                    // Skip the restaurants which do not belong to the selected school
                    continue;
                }
                else
                {
                    // Restaurant belongs to the selected school
                    WebClient client = new WebClient();
                    client.Encoding = Encoding.UTF8;
                    client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(MenuDownloadCompleted);
                    client.DownloadStringAsync(restaurant.MenuUrl, restaurant);
                }
            }
            // Wait until all restaurant menus have been downloaded and parsed.
            int lastProgress = Globals.progress;
            for (int i = 1; i <= 1000; i++) // Max sleep time 100 * 0,1 s = 10 s
            {
                if (!lastProgress.Equals(Globals.progress))
                {
                    worker.ReportProgress(Globals.progress);
                    lastProgress = Globals.progress;
                }
                if (Globals.allRestaurantsProcessed)
                {
                    worker.ReportProgress(100);
                    break;
                }
                else
                {
                    // Continue sleeping for another 100 ms = 0,1 s
                    Thread.Sleep(50);
                }
            }
        }

        private void MenuDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if ((e.Error != null) || (string.IsNullOrEmpty(e.Result)))
            {
                return;
            }
            Restaurant restaurant = e.UserState as Restaurant;
            restaurant.ParseMenu(e.Result);
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProBar.Visibility = Visibility.Visible;
            ProBar.Value = ((double)e.ProgressPercentage);
            var progressIndicator = new ProgressIndicator
            {
                Text = "Ladataan ruokalistoja...",
                IsVisible = true,
                IsIndeterminate = false,        
            };
            SystemTray.SetProgressIndicator(this, progressIndicator);
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                System.Diagnostics.Debug.WriteLine("Canceled!");
            }
            else if (!(e.Error == null))
            {
                System.Diagnostics.Debug.WriteLine("Error: " + e.Error.Message);
            }
            else // Backgroundworker completed successfully
            {
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
                    if (!appSettings.SelectedSchool.Equals(restaurant.School.NameShort_FI))
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

        //--------------------------------------------------------------------
        // Fetching restaurant menus with BackgroundWorker
        //--------------------------------------------------------------------





        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // The code implementing moving between left, middle and right view
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public int viewChangeThreshold = 30;
        private void OpenClose_Left(object sender, RoutedEventArgs e)
        {
            var left = Canvas.GetLeft(ChildCanvas);
            if (left > -viewChangeThreshold)
            {
                MoveViewWindow(-LeftView.Width);
            }
            else
            {
                MoveViewWindow(0);
            }
        }

        private void OpenClose_Right(object sender, RoutedEventArgs e)
        {
            var left = Canvas.GetLeft(ChildCanvas);
            if (left > (-LeftView.Width - viewChangeThreshold))
            {
                MoveViewWindow(-LeftView.Width - RightView.Width);
            }
            else
            {
                MoveViewWindow(-LeftView.Width);
            }
        }

        void MoveViewWindow(double left)
        {
            _viewMoved = true;
            //((Storyboard)ParentCanvas.Resources["ChangeViewAnimation"]).SkipToFill();
            ((DoubleAnimation)((Storyboard)ParentCanvas.Resources["ChangeViewAnimation"]).Children[0]).To = left;
            ((Storyboard)ParentCanvas.Resources["ChangeViewAnimation"]).Begin();
        }

        private void ParentCanvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (e.DeltaManipulation.Translation.X != 0)
                Canvas.SetLeft(ChildCanvas, Math.Min(Math.Max(-(LeftView.Width+RightView.Width),
                    Canvas.GetLeft(ChildCanvas) + e.DeltaManipulation.Translation.X), 0));  
        }

        double initialPosition;
        bool _viewMoved = false;
        private void ParentCanvas_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            _viewMoved = false;
            initialPosition = Canvas.GetLeft(ChildCanvas);
        }

        private void ParentCanvas_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            var left = Canvas.GetLeft(ChildCanvas);
            if (_viewMoved)
                return;

            if (Math.Abs(initialPosition - left) < viewChangeThreshold)
            {
                // Bouncing back
                MoveViewWindow(initialPosition);
                return;
            }
            // Change of state
            if (initialPosition - left > 0)
            {
                // Slide to the left
                if (initialPosition > -LeftView.Width)
                    MoveViewWindow(-LeftView.Width);
                else
                    MoveViewWindow(-(LeftView.Width+RightView.Width));
            }
            else
            {
                // Slide to the right
                if (initialPosition < -RightView.Width)
                    MoveViewWindow(-RightView.Width);
                else
                    MoveViewWindow(0);
            }

        }

        //--------------------------------------------------------------------
        // The code implementing moving between left, middle and right view
        //--------------------------------------------------------------------





        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // ScrollViewer - Header interactions
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        // When the ScrollViewer is loaded, start to listen its scrollbar's events.
        // Hide the header when scrolling down, and show the header when scrolling up.

        private bool headerVisible = true;
        private bool animationRunning = false;

        private void Scroller_Loaded(object sender, RoutedEventArgs e)
        {
            GetScrollBar(Scroller);
        }

        void GetScrollBar(DependencyObject parent)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                ScrollBar scrollBar = child as ScrollBar;
                if (scrollBar == null)
                {
                    GetScrollBar(child);
                }
                else
                {
                    string name = scrollBar.Name;

                    if (name == "VerticalScrollBar")
                    {
                        scrollBar.ValueChanged += OnScrollbarValueChanged;
                    }
                }
            }
        }

        private void OnScrollbarValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //System.Diagnostics.Debug.WriteLine(e.OldValue.ToString());
            //System.Diagnostics.Debug.WriteLine(e.NewValue.ToString());
            //System.Diagnostics.Debug.WriteLine("Animation running: " + animationRunning.ToString());
            //System.Diagnostics.Debug.WriteLine("Header visible: " + headerVisible.ToString());

            if (e.NewValue > e.OldValue &&
                e.NewValue > HeaderContainer.Height &&
                !animationRunning && headerVisible)
            {
                // Scroll Down
                headerVisible = false;
                Storyboard scrollDown = AnimateMove(HeaderContainer, 0, -HeaderContainer.Height, 300);
                scrollDown.Completed += new EventHandler(HeaderAnimate_Completed);
                scrollDown.Begin();
                animationRunning = true;
                //System.Diagnostics.Debug.WriteLine("User scrolled down!");
            }
            else if (e.NewValue < e.OldValue &&
                     !animationRunning && !headerVisible)
            {
                // Scroll up
                headerVisible = true;
                Storyboard scrollUp = AnimateMove(HeaderContainer, -HeaderContainer.Height, 0, 300);
                scrollUp.Completed += new EventHandler(HeaderAnimate_Completed);
                scrollUp.Begin();
                //System.Diagnostics.Debug.WriteLine("User scrolled up!");
            }
            //System.Diagnostics.Debug.WriteLine("---------------------");
        }

        public Storyboard AnimateMove(FrameworkElement fe, double from, double to, int durationMs)
        {
            // Initialize a new instance of the CompositeTransform which allows 
            // addition of multiple different transforms
            fe.RenderTransform = new CompositeTransform();

            // Create the timeline
            var animation = new DoubleAnimationUsingKeyFrames();

            // Define easingfunction
            ExponentialEase easingFunction = new ExponentialEase();
            easingFunction.EasingMode = EasingMode.EaseOut;

            // Add key frames to the timeline
            animation.KeyFrames.Add(new EasingDoubleKeyFrame
            {
                EasingFunction = easingFunction,
                KeyTime = TimeSpan.Zero,
                Value = from
            });
            animation.KeyFrames.Add(new EasingDoubleKeyFrame
            {
                EasingFunction = easingFunction,
                KeyTime = TimeSpan.FromMilliseconds(durationMs),
                Value = to
            });

            Storyboard.SetTargetProperty(animation,
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
            animationRunning = false;
        }

        //--------------------------------------------------------------------
        // ScrollViewer - Header interactions
        //--------------------------------------------------------------------





        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Open restaurant website pop up
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void RestaurantName_Clicked(object sender, RoutedEventArgs e)
        {
            // Do not fire up the MessageBox if user is scrolling horizontally
            if (Canvas.GetLeft(ChildCanvas) != -LeftView.Width)
            {
                return;
            }
            WebsitePrompt_PopUp.IsOpen = true;
            // Begin animation
            Storyboard showPopUp = AnimateMove(WebsitePrompt_PopUp, -400, 0, 400);
            showPopUp.Begin();
            // Vibrate
            VibrateController testVibrateController = VibrateController.Default;
            testVibrateController.Start(TimeSpan.FromSeconds(0.07));
            // Darken the background
            PopUpOverlayGrid.Visibility = Visibility.Visible;
            // Set properties
            Restaurant clickedRestaurant = (Restaurant)(sender as Button).DataContext;
            webBrowserTask.Uri = clickedRestaurant.HomeUrl;
            navigationPrompt_textblock.Text = "Haluatko varmasti poistua sovelluksesta " +
                "ja avata ravintolan " + clickedRestaurant.Name + " verkkosivun selaimessa?";
        }

        private void PopUpButton_Continue_Click(object sender, RoutedEventArgs e)
        {
            DismissWebsitePrompt();
            webBrowserTask.Show();
        }

        private void PopUpButton_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DismissWebsitePrompt();
        }

        // Hide the popup when the back key is pressed
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (this.WebsitePrompt_PopUp.IsOpen)
            {
                DismissWebsitePrompt();
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
            Storyboard hidePopUp = AnimateMove(WebsitePrompt_PopUp, 0, -400, 400);
            hidePopUp.Completed += new EventHandler(HidePopUp_Completed);
            hidePopUp.Begin();
            // Remove background darkener
            PopUpOverlayGrid.Visibility = Visibility.Collapsed;
        }

        //--------------------------------------------------------------------
        // Open restaurant website pop up
        //--------------------------------------------------------------------        





        private void DayOfWeek_Clicked(object sender, RoutedEventArgs e)
        {
            string clickedDay = (string)(sender as Button).DataContext;
            Globals.SelectedDay = clickedDay;
            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
        }






        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (appSettings.SelectedSchool == "TAY")
            {
                appSettings.SelectedSchool = "TTY";
            }
            else
            {
                appSettings.SelectedSchool = "TAY";
            }
            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
        }






    }
}