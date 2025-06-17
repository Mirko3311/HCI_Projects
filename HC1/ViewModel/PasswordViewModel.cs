using PrviProjektniZadatakHCI.Util;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PrviProjektniZadatakHCI.ViewModel
{

    public class PasswordViewModel : INotifyPropertyChanged
    {
        private string _password;
        private bool _isPasswordVisible;

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set { _isPasswordVisible = value; OnPropertyChanged(); OnPropertyChanged(nameof(ToggleButtonText)); }
        }

        public string ToggleButtonText => IsPasswordVisible ? "Hide" : "Show";

        public ICommand TogglePasswordVisibilityCommand { get; }

        public PasswordViewModel()
        {
            TogglePasswordVisibilityCommand = new RelayCommand(ToggleVisibility);
        }

        private void ToggleVisibility()
        {
            IsPasswordVisible = !IsPasswordVisible;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}
