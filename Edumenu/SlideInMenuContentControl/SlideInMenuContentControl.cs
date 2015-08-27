using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation; // Storyboard

namespace Edumenu
{
    [TemplatePart(Name = ElementLeftSideMenu, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ElementRightSideMenu, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ElementContentSelector, Type = typeof(Selector))]
    [TemplatePart(Name = ElementDisableContentOverlay, Type = typeof(Border))]
    public sealed class SlideInMenuContentControl : ContentControl
    {
        public static readonly DependencyProperty LeftMenuContentProperty =
            DependencyProperty.Register("LeftMenuContent", typeof(object), typeof(SlideInMenuContentControl), new PropertyMetadata(null));

        public static readonly DependencyProperty RightMenuContentProperty =
            DependencyProperty.Register("RightMenuContent", typeof(object), typeof(SlideInMenuContentControl), new PropertyMetadata(null));

        public static readonly DependencyProperty MenuStateProperty =
            DependencyProperty.Register("MenuState", typeof(MenuState), typeof(SlideInMenuContentControl), new PropertyMetadata(MenuState.Left, OnMenuStateChanged));

        public static readonly DependencyProperty RightSideMenuWidthProperty =
            DependencyProperty.Register("RightSideMenuWidth", typeof(double), typeof(SlideInMenuContentControl), new PropertyMetadata(250.0));

        public static readonly DependencyProperty LeftSideMenuWidthProperty =
            DependencyProperty.Register("LeftSideMenuWidth", typeof(double), typeof(SlideInMenuContentControl), new PropertyMetadata(250.0));

        private const string ElementLeftSideMenu = "ContentLeftSideMenu";
        private const string ElementRightSideMenu = "ContentRightSideMenu";
        private const string ElementContentSelector = "ContentSelector";
        private const string ElementDisableContentOverlay = "DisableContentOverlay";

        private FrameworkElement leftSideMenu;
        private FrameworkElement rightSideMenu;
        public Selector contentSelector;
        private Border disableContentOverlay;

        public SlideInMenuContentControl()
        {
            this.DefaultStyleKey = typeof(SlideInMenuContentControl);
        }

        public double LeftSideMenuWidth
        {
            get { return (double)GetValue(LeftSideMenuWidthProperty); }
            set { SetValue(LeftSideMenuWidthProperty, value); }
        }

        public double RightSideMenuWidth
        {
            get { return (double)GetValue(RightSideMenuWidthProperty); }
            set { SetValue(RightSideMenuWidthProperty, value); }
        }

        public MenuState MenuState
        {
            get { return (MenuState)GetValue(MenuStateProperty); }
            set { SetValue(MenuStateProperty, value); }
        }

        public object LeftMenuContent
        {
            get { return (object)GetValue(LeftMenuContentProperty); }
            set { SetValue(LeftMenuContentProperty, value); }
        }

        public object RightMenuContent
        {
            get { return (object)GetValue(RightMenuContentProperty); }
            set { SetValue(RightMenuContentProperty, value); }
        }

        public void GoToMenuState(ActiveState state)
        {
            switch (state)
            {
                case ActiveState.Main:
                    contentSelector.SelectedIndex = 1;
                    break;
                case ActiveState.Left:
                    contentSelector.SelectedIndex = 0;
                    break;
                case ActiveState.Right:
                    if (MenuState == MenuState.Right)
                    {
                        contentSelector.SelectedIndex = 1;
                    }
                    else if (MenuState == MenuState.Both)
                    {
                        contentSelector.SelectedIndex = 2;
                    }
                    break;
                default:
                    break;
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            contentSelector = GetTemplateChild(ElementContentSelector) as Selector;
            leftSideMenu = GetTemplateChild(ElementLeftSideMenu) as FrameworkElement;
            rightSideMenu = GetTemplateChild(ElementRightSideMenu) as FrameworkElement;
            disableContentOverlay = GetTemplateChild(ElementDisableContentOverlay) as Border;
            contentSelector.SelectionChanged += ContentSelector_SelectionChanged;
            SetMenuVisibility();
        }

        private static void OnMenuStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SlideInMenuContentControl;
            control.SetMenuVisibility();
        }

        private void SetMenuVisibility()
        {
            if (rightSideMenu != null && leftSideMenu != null && contentSelector != null)
            {
                switch (MenuState)
                {
                    case MenuState.Left:
                        rightSideMenu.Visibility = Visibility.Collapsed;
                        leftSideMenu.Visibility = Visibility.Visible;
                        contentSelector.SelectedIndex = 1;
                        break;
                    case MenuState.Right:
                        rightSideMenu.Visibility = Visibility.Visible;
                        leftSideMenu.Visibility = Visibility.Collapsed;
                        contentSelector.SelectedIndex = 0;
                        break;
                    case MenuState.Both:
                        rightSideMenu.Visibility = Visibility.Visible;
                        leftSideMenu.Visibility = Visibility.Visible;
                        contentSelector.SelectedIndex = 1;
                        break;
                    default:
                        break;
                }
            }
        }

        private void ContentSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (MenuState)
            {
                case MenuState.Left:
                    if (contentSelector.SelectedIndex == 0)
                    {
                        // TODO: rotate arrow here
                        disableContentOverlay.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        disableContentOverlay.Visibility = Visibility.Collapsed;
                    }
                    break;
                case MenuState.Right:
                    if (contentSelector.SelectedIndex == 0)
                    {
                        disableContentOverlay.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        disableContentOverlay.Visibility = Visibility.Visible;
                    }
                    break;
                case MenuState.Both:
                    if (contentSelector.SelectedIndex == 0 || contentSelector.SelectedIndex == 2)
                    {
                        disableContentOverlay.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        disableContentOverlay.Visibility = Visibility.Collapsed;
                    }
                    break;
                default:
                    break;
            }
        }
    }
    }
