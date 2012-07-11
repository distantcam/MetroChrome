using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MetroChrome.Sample
{
    partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StyleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = (ComboBox)sender;

            if (cb.SelectedIndex == 0)
            {
                ThemeManager.SetThemeTone(this, ThemeTones.Light);
                ThemeManager.SetAccentColor(this, Colors.Red);
            }
            else
            {
                ThemeManager.SetThemeTone(this, ThemeTones.Dark);
                ThemeManager.SetAccentColor(this, Colors.Lime);
            }
        }

        private void ResizeModeChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = (ComboBox)sender;

            switch (cb.SelectedIndex)
            {
                case 0:
                    this.ResizeMode = ResizeMode.NoResize;
                    break;
                case 1:
                    this.ResizeMode = ResizeMode.CanMinimize;
                    break;
                case 2:
                    this.ResizeMode = ResizeMode.CanResize;
                    break;
                case 3:
                    this.ResizeMode = ResizeMode.CanResizeWithGrip;
                    break;
            }
        }

        private void PopupMiddle(object sender, RoutedEventArgs e)
        {
            ShowPopup(new SamplePopup());

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(5));
            })
            .ContinueWith(t =>
            {
                HidePopup();
            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
