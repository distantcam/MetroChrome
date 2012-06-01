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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = (ComboBox)sender;

            if (cb.SelectedIndex == 0)
                ThemeManager.ChangeTheme(ThemeTones.Light, Color.FromRgb(78, 166, 234));
            else
                ThemeManager.ChangeTheme(ThemeTones.Dark, Color.FromRgb(78, 166, 234));
        }
    }
}
