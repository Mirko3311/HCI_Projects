using PrviProjektniZadatakHCI.ViewModel;
using System.Windows;
using System.Windows.Controls;

using static MaterialDesignThemes.Wpf.Theme;
using PasswordBox = System.Windows.Controls.PasswordBox;

namespace PrviProjektniZadatakHCI.View
{
    public partial class AddStudentView : UserControl
    {
        public AddStudentView()
        {
            InitializeComponent();
            Loaded += AddStudentView_Loaded;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddStudentViewModel vm)
            {
                vm.StudentPassword = ((PasswordBox)sender).Password;
            }
        }
        private void AddStudentView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is BaseViewModel vm)
            {
                vm.ClearPasswordRequested += () =>
                {
                    passwordSBox.Password = string.Empty;
                };
            }
        }

        public void ClearPassword()
        {
            passwordSBox.Password = string.Empty;
        }
        private bool isStudentPasswordVisible = false;


    }
}
