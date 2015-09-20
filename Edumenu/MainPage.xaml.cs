using Edumenu.Models;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Phone.Devices.Notification;
using Windows.Phone.UI.Input;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Edumenu
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
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
        private double initialPosition;
        private bool viewMoved = false;

        // Hiding header when scrolling down
        private bool headerVisible = true;
        private bool animationRunning = false;

        // Opening restaurant website
        private Uri uriToLaunch = null;

        // Other
        private AppSettings appSettings = new AppSettings();
        MainPageProperties mainPageProperties = new MainPageProperties();

        /*
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Constructor
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        */
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            // Set data contexts
            ParentCanvas.DataContext = mainPageProperties;
            Scroller.DataContext = App.RestaurantViewModel;
            DaysOfWeekItemsControl.DataContext = App.DayViewModel;
            SchoolsItemsControl.DataContext = App.SchoolViewModel;
            SelectedSchoolHeader.DataContext = App.SchoolViewModel;

            //// Define and launch BackgroundWorker
            //this.backgroundWorker.WorkerSupportsCancellation = true;
            //this.backgroundWorker.WorkerReportsProgress = true;
            //this.backgroundWorker.DoWork += new DoWorkEventHandler(this.BackgroundWorker_DoWork);
            //this.backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            //this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            //if (this.backgroundWorker.IsBusy != true)
            //{
            //    this.backgroundWorker.RunWorkerAsync();
            //}

            // StatusBar
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.BackgroundOpacity = 1;
            statusBar.BackgroundColor = Colors.Black;
            statusBar.ForegroundColor = Colors.White;

            //ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            //ApplicationView.GetForCurrentView().SuppressSystemOverlays = false;

            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            GetRestaurantMenus();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        /*
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Fetching restaurant menus with BackgroundWorker
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        */
        private async void GetRestaurantMenus()
        {
            //restaurantsProcessed = 0;
            //allRestaurantsProcessed = false;
            //progress = 0;
            //worker.ReportProgress(0);

            foreach (Restaurant restaurant in App.RestaurantViewModel.restaurantsAll)
            {
                if (!App.SchoolViewModel.GetSelectedSchool().Equals(restaurant.School.NameShort_FI))
                {
                    // Skip the restaurants which do not belong to the selected school
                    continue;
                }
                else
                {
                    // Restaurant belongs to the selected school
                    HttpClient httpClient = new HttpClient();
                    HttpResponseMessage response = await httpClient.GetAsync(restaurant.MenuUrl);
                    if (!response.IsSuccessStatusCode)
                    {
                        return;
                    }
                    string sourceCode = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(sourceCode))
                    {
                        return;
                    }
                    App.RestaurantViewModel.ParseMenu(sourceCode, restaurant);
                }
            }

            // Wait until all restaurant menus have been downloaded and parsed.
            //int lastProgress = progress;

            //// Max sleep time 200 * 0,05 s = 10 s
            //for (int i = 1; i <= 200; i++)
            //{
            //    if (!lastProgress.Equals(progress))
            //    {
            //        worker.ReportProgress(progress);
            //        lastProgress = progress;
            //    }

            //    if (allRestaurantsProcessed)
            //    {
            //        worker.ReportProgress(100);
            //        break;
            //    }
            //    else
            //    {
            //        // Continue sleeping for another 100 ms = 0,10 s
            //        await Task.Delay(TimeSpan.FromMilliseconds(100));
            //    }
            //}

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
            //ProBar.Visibility = Visibility.Visible;
            //ProBar.Value = (double)e.ProgressPercentage;
            //var progressIndicator = new ProgressIndicator
            //{
            //    Text = "Ladataan ruokalistoja...",
            //    IsVisible = true,
            //    IsIndeterminate = false,
            //};
            //SystemTray.SetProgressIndicator(this, progressIndicator);
        }
        /*
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
        */
        
        /*
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        The code implementing moving between left, middle and right view
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        */
        private void OpenClose_Left(object sender, RoutedEventArgs e)
        {
            var left = Canvas.GetLeft(ChildCanvas);
            if (left > -mainPageProperties.ViewChangeThreshold)
            {
                this.MoveViewWindow(-mainPageProperties.LeftViewWidth);
            }
            else
            {
                this.MoveViewWindow(0);
            }
        }

        private void OpenClose_Right(object sender, RoutedEventArgs e)
        {
            var left = Canvas.GetLeft(ChildCanvas);
            if (left > -(mainPageProperties.LeftViewWidth + mainPageProperties.ViewChangeThreshold))
            {
                this.MoveViewWindow(-(mainPageProperties.LeftViewWidth + mainPageProperties.RightViewWidth));
            }
            else
            {
                this.MoveViewWindow(-mainPageProperties.LeftViewWidth);
            }
        }

        private void MoveViewWindow(double left)
        {
            this.viewMoved = true;
            ////((Storyboard)ParentCanvas.Resources["ChangeViewAnimation"]).SkipToFill();
            ((DoubleAnimation)((Storyboard)ParentCanvas.Resources["ChangeViewAnimation"]).Children[0]).To = left;
            ((Storyboard)ParentCanvas.Resources["ChangeViewAnimation"]).Begin();
        }

        private void ParentCanvas_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (e.Delta.Translation.X != 0)
            {
                Canvas.SetLeft(
                    this.ChildCanvas,
                    Math.Min(Math.Max(-(mainPageProperties.LeftViewWidth + mainPageProperties.RightViewWidth), Canvas.GetLeft(this.ChildCanvas) + e.Delta.Translation.X), 0));
            }
        }

        private void ParentCanvas_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            this.viewMoved = false;
            this.initialPosition = Canvas.GetLeft(this.ChildCanvas);
        }

        private void ParentCanvas_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            var left = Canvas.GetLeft(ChildCanvas);
            if (this.viewMoved)
            {
                return;
            }

            if (Math.Abs(this.initialPosition - left) < mainPageProperties.ViewChangeThreshold)
            {
                // Bouncing back
                this.MoveViewWindow(this.initialPosition);
                return;
            }

            // Change of state
            if (this.initialPosition - left > 0)
            {
                // Slide to the left
                if (this.initialPosition > -mainPageProperties.LeftViewWidth)
                {
                    this.MoveViewWindow(-mainPageProperties.LeftViewWidth);
                }
                else
                {
                    this.MoveViewWindow(-(mainPageProperties.LeftViewWidth + mainPageProperties.RightViewWidth));
                }
            }
            else
            {
                // Slide to the right
                if (this.initialPosition < -mainPageProperties.RightViewWidth)
                {
                    this.MoveViewWindow(-mainPageProperties.RightViewWidth);
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
                        scrollBar.ValueChanged += this.ScrollBar_ValueChanged;
                    }
                }
            }
        }

        private void ScrollBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
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
                scrollDown.Completed += this.HeaderAnimate_Completed;
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
                scrollUp.Completed += this.HeaderAnimate_Completed;
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

            //Storyboard.SetTargetProperty(
            //    animation,
            //    new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateY)"));
            Storyboard.SetTargetProperty(
                animation,
                "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");
            Storyboard.SetTarget(animation, fe);

            // Create the storyboard
            var storyboard = new Storyboard();

            // Add the timeline to the storyboard
            storyboard.Children.Add(animation);

            return storyboard;
        }

        private void HeaderAnimate_Completed(object sender, object e)
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
            var vibrationDevice = VibrationDevice.GetDefault();
            vibrationDevice.Vibrate(TimeSpan.FromMilliseconds(200));

            // Darken the background
            PopUpOverlayGrid.Visibility = Visibility.Visible;

            // Set properties
            Restaurant clickedRestaurant = (Restaurant)(sender as Button).DataContext;
            this.uriToLaunch = clickedRestaurant.HomeUrl;
            navigationPrompt_textblock.Text = "Haluatko varmasti poistua sovelluksesta " +
                "ja avata ravintolan " + clickedRestaurant.Name + " verkkosivun selaimessa?";
        }

        private async void PopUpButton_Continue_Click(object sender, RoutedEventArgs e)
        {
            this.DismissWebsitePrompt();
            await Launcher.LaunchUriAsync(this.uriToLaunch);
        }

        private void PopUpButton_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DismissWebsitePrompt();
        }


        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            // Hide the popup when the back key is pressed
            if (this.WebsitePrompt_PopUp.IsOpen)
            {
                this.DismissWebsitePrompt();
                e.Handled = true;
            }
        }

        private void DismissWebsitePrompt()
        {
            // Begin animation
            Storyboard hidePopUp = this.AnimateMove(WebsitePrompt_PopUp, -60, -450, 300);
            hidePopUp.Completed += HidePopUp_Completed;
            hidePopUp.Begin();

            // Remove background darkener
            PopUpOverlayGrid.Visibility = Visibility.Collapsed;
        }

        private void HidePopUp_Completed(object sender, object e)
        {
            WebsitePrompt_PopUp.IsOpen = false;
        }

        /*
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Other click events
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        */
        private void DayOfWeek_Clicked(object sender, RoutedEventArgs e)
        {
            if (this.IsScrollingHorizontally())
            {
                return;
            }

            Day clickedDay = (Day)(sender as Button).DataContext;
            if (clickedDay.Name.ToLower().Equals(App.DayViewModel.GetSelectedDay().ToLower()))
            {
                return;
            }

            App.DayViewModel.SelectDay(clickedDay.Name);
            GetRestaurantMenus();

            // Go back to the main view
            OpenClose_Left(sender, e);
        }

        private void School_Clicked(object sender, RoutedEventArgs e)
        {
            if (this.IsScrollingHorizontally())
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
            GetRestaurantMenus();

            // Go back to the main view
            OpenClose_Right(sender, e);
        }

        private void Diets_Clicked(object sender, RoutedEventArgs e)
        {
            if (this.IsScrollingHorizontally())
            {
                return;
            }

            Frame.Navigate(typeof(DietsPage));
        }

        private void SelectedSchool_Clicked(object sender, RoutedEventArgs e)
        {
            if (this.IsScrollingHorizontally())
            {
                return;
            }

            OpenClose_Right(sender, e);
        }

        private void HamburgerButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (this.IsScrollingHorizontally())
            {
                return;
            }

            OpenClose_Left(sender, e);
        }

        private bool IsScrollingHorizontally()
        {
            // Do not fire up the event if user is scrolling horizontally
            if (Canvas.GetLeft(this.ChildCanvas) != 0 &&
                Canvas.GetLeft(this.ChildCanvas) != -(mainPageProperties.LeftViewWidth) &&
                Canvas.GetLeft(this.ChildCanvas) != -(mainPageProperties.LeftViewWidth + mainPageProperties.RightViewWidth))
            {
                return true;
            }
            return false;
        }

    }
}


