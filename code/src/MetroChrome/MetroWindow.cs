using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using MetroChrome.Native;

namespace MetroChrome
{
    [TemplatePart(Name = "PART_Min", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Max", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Clo", Type = typeof(Button))]
    public class MetroWindow : Window
    {
        static MetroWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroWindow), new FrameworkPropertyMetadata(typeof(MetroWindow)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var closeButton = base.GetTemplateChild("PART_Clo") as Button;
            if (closeButton != null)
                closeButton.Click += CloseButton_Click;

            var minimizeButton = base.GetTemplateChild("PART_Min") as Button;
            if (minimizeButton != null)
                minimizeButton.Click += MinimizeButton_Click;

            var maximizeButton = base.GetTemplateChild("PART_Max") as Button;
            if (maximizeButton != null)
                maximizeButton.Click += MaximizeButton_Click;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            var hwnd = ((HwndSource)HwndSource.FromVisual(this)).Handle;
            UnsafeNativeMethods.ShowWindow(hwnd, ShowWindowCommands.Minimize);
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            var hwnd = ((HwndSource)HwndSource.FromVisual(this)).Handle;
            UnsafeNativeMethods.ShowWindow(hwnd, this.WindowState == WindowState.Normal ? ShowWindowCommands.Maximize : ShowWindowCommands.Normal);
        }
    }
}