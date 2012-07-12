---
layout: main
title: MetroChrome
---

MetroChrome is my own personal take on a Metro UI. This project was started out of the frustration involved in getting other Metro themes up and running in an application. Given the simplicity and minimalism of Metro the setup of some of these packages was horribly complex. On top of that there were issues to do with changing the theme once set.

MetroChrome uses a ThemeManager class to manage the theme, and allow you to change it dynamically during runtime. To use MetroChrome there are 3 steps.

1. Add a reference to MetroChrome. MetroChrome is also in NuGet for easy access.
2. Change any windows of your application to inherit from MetroChrome.MetroWindow instead of System.Windows.Window.
3. Add a call to ThemeManager.ChangeTheme() somewhere in the startup of your application. This will set the tone (light or dark) as well as the accent colour for your application. You can call this method any time to change the application's theme.