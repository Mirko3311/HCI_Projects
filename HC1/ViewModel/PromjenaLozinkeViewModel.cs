
namespace PrviProjektniZadatakHCI.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class PromjenaLozinkeViewModel : INotifyPropertyChanged
    {
        private string _currentPassword;
        public string CurrentPassword
        {
            get => _currentPassword;
            set
            {
                _currentPassword = value;
                OnPropertyChanged();
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged();
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }
        public bool IsCurrentPasswordVisible
        {
            get => _isCurrentPasswordVisible;
            set
            {
                _isCurrentPasswordVisible = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentEyeIcon));
            }
        }
        private bool _isCurrentPasswordVisible;

        public bool IsNewPasswordVisible
        {
            get => _isNewPasswordVisible;
            set
            {
                _isNewPasswordVisible = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NewEyeIcon));
            }
        }
        private bool _isNewPasswordVisible;

        public bool IsConfirmPasswordVisible
        {
            get => _isConfirmPasswordVisible;
            set
            {
                _isConfirmPasswordVisible = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ConfirmEyeIcon));
            }
        }
        private bool _isConfirmPasswordVisible;


        public string CurrentEyeIcon => IsCurrentPasswordVisible ? "\uE070" : "\uE052";
        public string NewEyeIcon => IsNewPasswordVisible ? "\uE070" : "\uE052";
        public string ConfirmEyeIcon => IsConfirmPasswordVisible ? "\uE070" : "\uE052";

        public ICommand ToggleCurrentPasswordVisibilityCommand { get; }
        public ICommand ToggleNewPasswordVisibilityCommand { get; }
        public ICommand ToggleConfirmPasswordVisibilityCommand { get; }

        public PromjenaLozinkeViewModel()
        {
            ToggleCurrentPasswordVisibilityCommand = new RelayCommand(ToggleCurrentPasswordVisibility);
            ToggleNewPasswordVisibilityCommand = new RelayCommand(ToggleNewPasswordVisibility);
            ToggleConfirmPasswordVisibilityCommand = new RelayCommand(ToggleConfirmPasswordVisibility);
        }

        private void ToggleCurrentPasswordVisibility()
        {
            IsCurrentPasswordVisible = !IsCurrentPasswordVisible;
        }

        private void ToggleNewPasswordVisibility()
        {
            IsNewPasswordVisible = !IsNewPasswordVisible;
        }

        private void ToggleConfirmPasswordVisibility()
        {
            IsConfirmPasswordVisible = !IsConfirmPasswordVisible;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
