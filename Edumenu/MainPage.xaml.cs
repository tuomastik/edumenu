using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Edumenu.Resources;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Edumenu.ViewModels;
using System.Windows.Media;
using System.Windows.Controls.Primitives;

namespace Edumenu
{
    public partial class MainPage : PhoneApplicationPage
    {
        private RestaurantViewModel _restaurantViewModel
        {
            get
            {
                return this.DataContext as RestaurantViewModel;
            }
        }

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            VisualStateManager.GoToState(this, "Normal", false);
            this.DataContext = App.RestaurantViewModel;
        }

        public int viewChangeThreshold = 160;
        private void OpenClose_Left(object sender, RoutedEventArgs e)
        {
            var left = Canvas.GetLeft(LayoutRoot);
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
            var left = Canvas.GetLeft(LayoutRoot);
            if (left > (-LeftView.Width - viewChangeThreshold))
            {
                MoveViewWindow(-LeftView.Width + RightView.Width);
            }
            else
            {
                MoveViewWindow(-LeftView.Width);

            }
        }

        void MoveViewWindow(double left)
        {
            _viewMoved = true;
            ((Storyboard)canvas.Resources["moveAnimation"]).SkipToFill();
            ((DoubleAnimation)((Storyboard)canvas.Resources["moveAnimation"]).Children[0]).To = left;
            ((Storyboard)canvas.Resources["moveAnimation"]).Begin();
        }

        private void canvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (e.DeltaManipulation.Translation.X != 0)
                Canvas.SetLeft(LayoutRoot, Math.Min(Math.Max(-(LeftView.Width+RightView.Width),
                    Canvas.GetLeft(LayoutRoot) + e.DeltaManipulation.Translation.X), 0));
        }

        double initialPosition;
        bool _viewMoved = false;
        private void canvas_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            _viewMoved = false;
            initialPosition = Canvas.GetLeft(LayoutRoot);
        }

        private void canvas_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            var left = Canvas.GetLeft(LayoutRoot);
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
                //slide to the right
                if (initialPosition < -RightView.Width)
                    MoveViewWindow(-RightView.Width);
                else
                    MoveViewWindow(0);
            }

        }






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
            System.Diagnostics.Debug.WriteLine(e.OldValue.ToString());
            System.Diagnostics.Debug.WriteLine(e.NewValue.ToString());
            System.Diagnostics.Debug.WriteLine("Animation running: " + animationRunning.ToString());
            System.Diagnostics.Debug.WriteLine("Header visible: " + headerVisible.ToString());

            if (e.NewValue > e.OldValue &&
                e.NewValue > HeaderContainer.Height &&
                !animationRunning && headerVisible)
            {
                // Scroll Down
                headerVisible = false;
                AnimateMove(HeaderContainer, 0, -HeaderContainer.Height, 300);
                System.Diagnostics.Debug.WriteLine("User scrolled down!");
            }
            else if (e.NewValue < e.OldValue &&
                     !animationRunning && !headerVisible)
            {
                // Scroll up
                headerVisible = true;
                AnimateMove(HeaderContainer, -HeaderContainer.Height, 0, 300);
                System.Diagnostics.Debug.WriteLine("User scrolled up!");
            }
            System.Diagnostics.Debug.WriteLine("---------------------");
        }

        public void AnimateMove(FrameworkElement fe, double from, double to, int durationMs)
        {
            // Initialize a new instance of the CompositeTransform which allows 
            // addition of multiple different transforms
            fe.RenderTransform = new CompositeTransform();

            // Create the timeline
            var animation = new DoubleAnimationUsingKeyFrames();

            // Add key frames to the timeline
            animation.KeyFrames.Add(new EasingDoubleKeyFrame
            {
                KeyTime = TimeSpan.Zero,
                Value = from
            });
            animation.KeyFrames.Add(new EasingDoubleKeyFrame
            {
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

            // Listen for Completed event
            storyboard.Completed += new EventHandler(AnimateMove_Completed);

            // Start the animation
            storyboard.Begin();

            animationRunning = true;
        }

        private void AnimateMove_Completed(object sender, EventArgs e)
        {
            animationRunning = false;
        }

        //--------------------------------------------------------------------
        // ScrollViewer - Header interactions
        //--------------------------------------------------------------------










    }
}