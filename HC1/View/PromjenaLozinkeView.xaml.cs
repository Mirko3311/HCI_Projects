using ASystem;
using PrviProjektniZadatakHCI.DataAccess;
using PrviProjektniZadatakHCI.Resources;
using PrviProjektniZadatakHCI.ViewModel;
using System.Windows;


namespace PrviProjektniZadatakHCI.View
{
  
    public partial class PromjenaLozinkeView : Window
    {
        private PromjenaLozinkeViewModel viewModel;
        private Korisnik korisnik;

        public PromjenaLozinkeView(Korisnik korisnik)
        {
            InitializeComponent();
            this.korisnik = korisnik;
            viewModel = new PromjenaLozinkeViewModel();
            DataContext = viewModel;
        }

        private void CurrentPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPassword = CurrentPasswordBox.Password;
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            viewModel.NewPassword = NewPasswordBox.Password;
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            viewModel.ConfirmPassword = ConfirmPasswordBox.Password;
        }

        private void SaveNewPassword_Click(object sender, RoutedEventArgs e)
        {
            string currentPassword = viewModel.CurrentPassword;
            string newPassword = viewModel.NewPassword;
            string confirmPassword = viewModel.ConfirmPassword;

            if (newPassword != confirmPassword)
            {
                string message = SharedResource.PassConfirmation;
                new WrongWindow(message).ShowDialog();

                return;
            }


            if (!KorisnikDataAccess.provjeriLozinku(korisnik, currentPassword))
            {
                string message = SharedResource.CurrentPassMessage;
                new WrongWindow(message).ShowDialog();
                return;
            }


            bool success = KorisnikDataAccess.azurirajLozinku(korisnik, newPassword);
            if (success)
            {

                string message = SharedResource.PasswordChangeMessage;
                new SuccessWindow(message).ShowDialog();
                this.Close();
            }
            else
            {
                string message = SharedResource.ErrorChangePassword;
                new WrongWindow(message).ShowDialog();

            }
        }
    }
    


}


