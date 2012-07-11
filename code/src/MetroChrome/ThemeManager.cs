using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace MetroChrome
{
    public enum ThemeTones
    {
        None,
        Light,
        Dark
    }

    public static class ThemeManager
    {
        public static readonly DependencyProperty ThemeToneProperty =
            DependencyProperty.RegisterAttached("ThemeTone", typeof(ThemeTones), typeof(ThemeManager),
            new UIPropertyMetadata(ThemeTones.None, OnThemeChanged));

        public static ThemeTones GetThemeTone(DependencyObject obj)
        {
            return (ThemeTones)obj.GetValue(ThemeToneProperty);
        }

        public static void SetThemeTone(DependencyObject obj, ThemeTones value)
        {
            obj.SetValue(ThemeToneProperty, value);
        }

        public static readonly DependencyProperty AccentColorProperty =
            DependencyProperty.RegisterAttached("AccentColor", typeof(Color), typeof(ThemeManager),
            new UIPropertyMetadata(Colors.Red, OnThemeChanged));

        public static Color GetAccentColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(AccentColorProperty);
        }

        public static void SetAccentColor(DependencyObject obj, Color value)
        {
            obj.SetValue(AccentColorProperty, value);
        }

        private static void OnThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as FrameworkElement;
            if (element == null)
                return;

            ChangeTheme(element.Resources.MergedDictionaries, GetThemeTone(d), GetAccentColor(d));
        }

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

            if (tone == ThemeTones.None)
                return;

            ResourceDictionary toneDictionary = null;

            switch (tone)
            {
                case ThemeTones.Light:
                    toneDictionary = GetThemeResourceDictionary("StyleLight");
                    break;
                case ThemeTones.Dark:
                    toneDictionary = GetThemeResourceDictionary("StyleDark");
                    break;
            }

            var accentDictionary = CreateAccentDictionary(accentColor);

            mergedDictionaries.Add(toneDictionary);

            mergedDictionaries.Add(accentDictionary);

            foreach (var s in StyleFiles)
            {
                var dict = GetThemeResourceDictionary(s);
                dict.MergedDictionaries.Add(toneDictionary);
                dict.MergedDictionaries.Add(accentDictionary);

                mergedDictionaries.Add(dict);
            }
        }
    }
}
