using PrviProjektniZadatakHCI.DataAccess;
using PrviProjektniZadatakHCI.Resources;
using PrviProjektniZadatakHCI.Util;
using PrviProjektniZadatakHCI.View;
using PrviProjektniZadatakHCI.ViewModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;



namespace ASystem
{

    public partial class MainWindow : Window
    {


        private PasswordViewModel ViewModel => (PasswordViewModel)DataContext;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                DataContext = new PasswordViewModel();

                this.WindowState = WindowState.Maximized;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error during MainWindow initialization: {ex.Message}", "Initialization Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void ChangeLanguage(string languageCode)
        {
            var cultureInfo = new CultureInfo(languageCode);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            if (languageCode == "sr")
            {
                resourceDictionary.Source = new Uri("pack://application:,,,/PrviProjektniZadatakHCI;component/Resources/SerbianLanguage.xaml");
            }
            else
            {
                resourceDictionary.Source = new Uri("pack://application:,,,/PrviProjektniZadatakHCI;component/Resources/EnglishLanguage.xaml");
            }
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(resourceDictionary);
        }


        private void ChangeLanguageToEnglish(object sender, RoutedEventArgs e)
        {
            ChangeLanguage("en");
        }

        private void ChangeLanguageToSerbian(object sender, RoutedEventArgs e)
        {
            ChangeLanguage("sr");
        }

        private const string AdminHashedPassword = "$2a$12$lgLEhnmfrD0Jjqve.k/UvOwY3hqA/oVO3SBkutzQ199MMZqHIPR06";
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var userType = KorisnikDataAccess.VerifyUser(txtUsername.Text, ViewModel.Password);
            try
            {
                if (userType == "administrator")
                {

                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                    ChangerThemes.ApplyUserTheme(txtUsername.Text);
                    this.Close();
                    return;

                }

                if (userType == "student")
                {
                    Student trenutniStudent = StudentDataAccess.GetStudent(txtUsername.Text);
                    if (trenutniStudent != null)
                    {

                        StudentWindow studentWindow = new StudentWindow(trenutniStudent);
                        studentWindow.Show();
                        ChangerThemes.ApplyUserTheme(txtUsername.Text);
                        this.Close();
                    }
                }
                else if (userType == "profesor")
                {
                    Profesor trenutniProfesor = ProfessorDataAccess.GetProfesor(txtUsername.Text);



                    if (trenutniProfesor != null)
                    {
                        Console.WriteLine(trenutniProfesor.ime);
                        ProfessorWindow profesorWindow = new ProfessorWindow(trenutniProfesor);
                        ChangerThemes.ApplyUserTheme(txtUsername.Text);
                        profesorWindow.Show();

                        this.Close();
                    }
                }


                else
                {
                    string message = SharedResource.UsernamePasswordInCorrect;
                    new WrongWindow(message).ShowDialog();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greška pri logovanju: " + ex.Message);
                System.Windows.MessageBox.Show("Greška pri logovanju: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.IsPasswordVisible && sender is PasswordBox passwordBox)
            {
                ViewModel.Password = passwordBox.Password;
            }
        }
    }

}

