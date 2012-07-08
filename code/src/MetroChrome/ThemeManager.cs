using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace MetroChrome
{
    public enum ThemeTones
    {
        Light,
        Dark
    }

    public static class ThemeManager
    {
        private static string[] StyleFiles = new string[] { 
            "Buttons", 
            "CheckBox",
            "ComboBox",
            "ListBox", 
            "ProgressBar",
            "Scroll", 
            "Tabs",
            "Text" 
        };

        private static ResourceDictionary GetThemeResourceDictionary(string theme)
        {
            if (String.IsNullOrEmpty(theme))
                throw new ArgumentException("theme is null or empty.", "theme");

            string packUri = String.Format(@"/MetroChrome;component/Styles/{0}.xaml", theme);
            return Application.LoadComponent(new Uri(packUri, UriKind.Relative)) as ResourceDictionary;
        }

        private static ResourceDictionary CreateAccentDictionary(Color accentColor)
        {
            var dictionary = new MetroDictionary();
            dictionary.Add("MetroAccentColor", accentColor);

            var accentBrush = new SolidColorBrush(accentColor);
            accentBrush.Freeze();

            dictionary.Add("MetroAccentBrush", accentBrush);

            return dictionary;
        }

        public static void ChangeTheme(ThemeTones tone, Color accentColor)
        {
            ChangeTheme(Application.Current.Resources.MergedDictionaries, tone, accentColor);
        }

        private static void ChangeTheme(Collection<ResourceDictionary> mergedDictionaries, ThemeTones tone, Color accentColor)
        {
            var metroDictionaries = mergedDictionaries.OfType<MetroDictionary>().ToArray();

            foreach (var d in metroDictionaries)
                mergedDictionaries.Remove(d);

            switch (tone)
            {
                case ThemeTones.Light:
                    mergedDictionaries.Add(GetThemeResourceDictionary("StyleLight"));
                    break;
                case ThemeTones.Dark:
                    mergedDictionaries.Add(GetThemeResourceDictionary("StyleDark"));
                    break;
            }

            mergedDictionaries.Add(CreateAccentDictionary(accentColor));

            foreach (var s in StyleFiles)
                mergedDictionaries.Add(GetThemeResourceDictionary(s));
        }
    }
}
