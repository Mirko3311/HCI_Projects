
using global::ASystem;
using PrviProjektniZadatakHCI.Util;
using PrviProjektniZadatakHCI.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
namespace PrviProjektniZadatakHCI.ViewModel
{
 

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

    

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
               MessageBox.Show($"Greška u OnPropertyChanged: {ex.Message}");
                Console.WriteLine($"Property: {propertyName}");
                MessageBox.Show($"Greška pri obavještavanju za: {propertyName}\n{ex.Message}", "OnPropertyChanged Greška");
            }
        }
        public event Action ClearPasswordRequested;

        protected void ClearPassword()
        {
       
            ClearPasswordRequested?.Invoke();
        }

        public ICommand LightThemeCommand { get; protected set; }
        public ICommand DarkThemeCommand { get; protected set; }
        public ICommand GreenThemeCommand { get; protected set; }
        public ICommand ChangeLanguageCommand { get; protected set; }
        public ICommand LogOutCommand { get; protected set; }
        public ICommand ChangeToEnglishCommand { get; protected set; }
        public ICommand ChangeToSerbianCommand { get; protected set; }
        public ICommand ChangePasswordCommand { get; }

        protected Korisnik _korisnik;
        public BaseViewModel()
        {
           
            LightThemeCommand = new RelayCommand(() => ChangeTheme("Light"));
            DarkThemeCommand = new RelayCommand(() => ChangeTheme("Dark"));
            GreenThemeCommand = new RelayCommand(() => ChangeTheme("Green"));
            ChangeToEnglishCommand = new RelayCommand(() => ((App)Application.Current).ChangeLanguage("en"));
            ChangeToSerbianCommand = new RelayCommand(() => ((App)Application.Current).ChangeLanguage("sr"));
            LogOutCommand = new RelayCommand(LogOut);
            ChangePasswordCommand = new RelayCommand(OnChangePassword);

        }
        private void OnChangePassword()
        {
            if (_korisnik == null)
                return;

            var prozor = new PromjenaLozinkeView(_korisnik);
            prozor.ShowDialog();
        }
        protected virtual void ChangeTheme(string theme)
        {
       
            ChangerThemes.ChangeTheme(theme,"admin");
        }

  
        protected virtual void LogOut()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var oldWindow = Application.Current.Windows
                    .OfType<Window>()
                    .FirstOrDefault(w => !(w is MainWindow));
                var mainWindow = new MainWindow();
                mainWindow.Show();
                oldWindow?.Close();
            });
        }
    
    }

}

