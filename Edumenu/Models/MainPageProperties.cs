using System.ComponentModel;
using Windows.UI.Xaml;

namespace Edumenu.Models
{
    class MainPageProperties : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static double screenWidth;
        private static double screenHeight;
        private static double leftViewWidth;
        private static double rightViewWidth;
        private static double viewChangeThreshold;
        private static double canvasLeft;
        private static double childCanvasWidth;
        private Thickness rightViewMargin;
        private Thickness mainViewMargin;

        public MainPageProperties()
        {
            Window.Current.SizeChanged += Current_SizeChanged;
            this.UpdateProperties();
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            this.UpdateProperties();
        }
        
        private void UpdateProperties()
        {
            ScreenWidth = Window.Current.Bounds.Width;
            ScreenHeight = Window.Current.Bounds.Height;
            double sideViewWidthPortion;
            if (ScreenHeight > ScreenWidth)
            {
                sideViewWidthPortion = 0.8;
            }
            else
            {
                sideViewWidthPortion = 0.5;
            }
            LeftViewWidth = sideViewWidthPortion * ScreenWidth;
            RightViewWidth = sideViewWidthPortion * ScreenWidth;
            ViewChangeThreshold = 0.15 * LeftViewWidth;
            CanvasLeft = -LeftViewWidth;
            ChildCanvasWidth = LeftViewWidth + ScreenWidth + RightViewWidth;
            RightViewMargin = new Thickness(LeftViewWidth + ScreenWidth, 0, 0, 0);
            MainViewMargin = new Thickness(LeftViewWidth, 0, 0, 0);
            System.Diagnostics.Debug.WriteLine("ScreenWidth: " + ScreenWidth.ToString());
            System.Diagnostics.Debug.WriteLine("ScreenHeight: " + ScreenHeight.ToString());
            System.Diagnostics.Debug.WriteLine("LeftViewWidth: " + LeftViewWidth.ToString());
            System.Diagnostics.Debug.WriteLine("RightViewWidth: " + RightViewWidth.ToString());
            System.Diagnostics.Debug.WriteLine("ViewChangeThreshold: " + ViewChangeThreshold.ToString());
            System.Diagnostics.Debug.WriteLine("CanvasLeft: " + CanvasLeft.ToString());
            System.Diagnostics.Debug.WriteLine("ChildCanvasWidth: " + ChildCanvasWidth.ToString());
            System.Diagnostics.Debug.WriteLine("RightViewMargin: " + RightViewMargin.ToString());
            System.Diagnostics.Debug.WriteLine("MainViewMargin: " + MainViewMargin.ToString());
            System.Diagnostics.Debug.WriteLine("-----------------------------------");
        }

        public double ScreenWidth
        {
            get
            {
                return screenWidth;
            }
            set
            {
                screenWidth = value;
                OnPropertyChanged("ScreenWidth");
            }
        }
        public double ScreenHeight
        {
            get
            {
                return screenHeight;
            }
            set
            {
                screenHeight = value;
                OnPropertyChanged("ScreenHeight");
            }
        }
        public double LeftViewWidth
        {
            get
            {
                return leftViewWidth;
            }
            set
            {
                leftViewWidth = value;
                OnPropertyChanged("LeftViewWidth");
            }
        }
        public double RightViewWidth
        {
            get
            {
                return rightViewWidth;
            }
            set
            {
                rightViewWidth = value;
                OnPropertyChanged("RightViewWidth");
            }
        }
        public double CanvasLeft
        {
            get
            {
                return canvasLeft;
            }
            set
            {
                canvasLeft = value;
                OnPropertyChanged("CanvasLeft");
            }
        }
        public double ChildCanvasWidth
        {
            get
            {
                return childCanvasWidth;
            }
            set
            {
                childCanvasWidth = value;
                OnPropertyChanged("ChildCanvasWidth");
            }
        }
        public Thickness RightViewMargin
        {
            get
            {
                return rightViewMargin;
            }
            set
            {
                rightViewMargin = value;
                OnPropertyChanged("RightViewMargin");
            }
        }
        public Thickness MainViewMargin
        {
            get
            {
                return mainViewMargin;
            }
            set
            {
                mainViewMargin = value;
                OnPropertyChanged("MainViewMargin");
            }
        }
        public double ViewChangeThreshold
        {
            get
            {
                return viewChangeThreshold;
            }
            set
            {
                viewChangeThreshold = value;
                OnPropertyChanged("ViewChangeThreshold");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
