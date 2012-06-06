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
                ThemeManager.ChangeTheme(ThemeTones.Light, Colors.Red);
            else
                ThemeManager.ChangeTheme(ThemeTones.Dark, Colors.Lime);
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
    }
}
