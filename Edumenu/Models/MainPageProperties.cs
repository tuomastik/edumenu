using Windows.UI.Xaml;

namespace Edumenu.Models
{
    class MainPageProperties
    {
        private static double screenWidth = Window.Current.Bounds.Width;
        private static double screenHeight = Window.Current.Bounds.Height;
        private static double leftViewWidth = 0.75 * screenWidth;
        private static double rightViewWidth = 0.75 * screenWidth;
        private static double viewChangeThreshold = 0.1 * leftViewWidth;
        private static double canvasLeft = -leftViewWidth;
        private static double childCanvasWidth = leftViewWidth + screenWidth + rightViewWidth;
        private Thickness rightViewMargin = new Thickness(leftViewWidth + screenWidth, 0, 0, 0);
        private Thickness mainViewMargin = new Thickness(leftViewWidth, 0, 0, 0);

        public double ScreenWidth
        {
            get
            {
                return screenWidth;
            }
            set
            {
                screenWidth = value;
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
            }
        }
    }
}
