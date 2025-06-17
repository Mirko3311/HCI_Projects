using ASystem;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Windows;
using System.Windows.Markup;

namespace PrviProjektniZadatakHCI
{
    public partial class App : Application
    {
        
        public App()
        {
        
        }



        public void ChangeLanguage(string languageCode)
        {

            var cultureInfo = new CultureInfo(languageCode);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            ResourceDictionary languageResource = new ResourceDictionary();
            if (languageCode == "sr")
            {
                languageResource.Source = new Uri("pack://application:,,,/PrviProjektniZadatakHCI;component/Resources/SerbianLanguage.xaml");
            }
            else
            {
                languageResource.Source = new Uri("pack://application:,,,/PrviProjektniZadatakHCI;component/Resources/EnglishLanguage.xaml");
            }

            var dictionariesToRemove = Application.Current.Resources.MergedDictionaries
                .Where(d => d.Source != null &&
                       (d.Source.OriginalString.Contains("SerbianLanguage.xaml") ||
                        d.Source.OriginalString.Contains("EnglishLanguage.xaml")))
                .ToList();

            foreach (var dict in dictionariesToRemove)
            {
                Application.Current.Resources.MergedDictionaries.Remove(dict);
            }

            Application.Current.Resources.MergedDictionaries.Add(languageResource);
            foreach (Window window in Application.Current.Windows)
            {
                if (window is ProfessorWindow professorWindow)
                {
                    professorWindow.RefreshAllDataGrids();
                }
            }
        }
        public static void ChangeTheme(Uri themeuri)
        {
            ResourceDictionary Theme = new ResourceDictionary() { Source = themeuri };

            App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(Theme);

        }

 
     
    }
}