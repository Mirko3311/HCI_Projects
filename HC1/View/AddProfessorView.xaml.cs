using PrviProjektniZadatakHCI.ViewModel;
using System.Windows;
using System.Windows.Controls;


namespace PrviProjektniZadatakHCI.View
{

    public partial class AddProfessorView : UserControl
    {
        public AddProfessorView()
        {
            InitializeComponent();
            this.Loaded += AddProfessorView_Loaded;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddProfessorViewModel vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }

        private void AddProfessorView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is BaseViewModel vm)
            {
                vm.ClearPasswordRequested += () =>
                {
                    passwordBox.Password = string.Empty;
                };
            }
        }
    }
}
