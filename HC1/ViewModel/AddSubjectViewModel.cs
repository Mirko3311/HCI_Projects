using ASystem;
using PrviProjektniZadatakHCI.Command;
using PrviProjektniZadatakHCI.Util;
using PrviProjektniZadatakHCI.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace PrviProjektniZadatakHCI.ViewModel
{
    public class AddSubjectViewModel : INotifyPropertyChanged
    {
        public Predmet NewSubject { get; set; } = new Predmet();
        public ObservableCollection<Predmet> Predmeti { get; set; }
        public ICommand AddPredmetCommand { get; }

        public AddSubjectViewModel(ObservableCollection<Predmet> sharedSubjects)
        {
            Predmeti = sharedSubjects;
            AddPredmetCommand = new RelayCommand(ExecuteAddSubject);
        }

        private string _ectsInput;
        public string ECTSInput
        {
            get => _ectsInput;
            set 
            {
                _ectsInput = value;
                OnPropertyChanged();
                ValidateECTS(value);
            }
        }

        private string _idInput;
        public string IdInput
        {
            get => _idInput;
            set
            {
                _idInput = value;
                OnPropertyChanged();
                ValidateId(value);
            }
        }

        public Visibility ECTSValidationVisibility { get; set; } = Visibility.Collapsed;
        public Visibility IdValidationVisibility { get; set; } = Visibility.Collapsed;
        public Visibility NameValidationVisibility { get; set; } = Visibility.Collapsed;
        public string IdExistsMessage { get; set; }
        public Visibility IdExistsVisibility { get; set; } = Visibility.Collapsed;

        private void ValidateECTS(string value)
        {
            if (int.TryParse(value, out int parsed) && parsed is >= 1 and <= 8)
            {
                NewSubject.ECTS = parsed;
                ECTSValidationVisibility = Visibility.Collapsed;
            }
            else
            {
                ECTSValidationVisibility = Visibility.Visible;
            }

            OnPropertyChanged(nameof(ECTSValidationVisibility));
        }

        private void ValidateId(string value)
        {
            if (int.TryParse(value, out int parsed))
            {
                NewSubject.IdPredmeta = parsed;
                IdValidationVisibility = Visibility.Collapsed;
            }
            else
            {
                IdValidationVisibility = Visibility.Visible;
            }

            OnPropertyChanged(nameof(IdValidationVisibility));
        }

        public void ClearFields()
        {
            NewSubject = new Predmet();
            ECTSInput = string.Empty;
            IdInput = string.Empty;
            IdExistsMessage = string.Empty;

            ECTSValidationVisibility = Visibility.Collapsed;
            IdValidationVisibility = Visibility.Collapsed;
            IdExistsVisibility = Visibility.Collapsed;
            NameValidationVisibility = Visibility.Collapsed;

            OnPropertyChanged(nameof(NewSubject));
            OnPropertyChanged(nameof(ECTSInput));
            OnPropertyChanged(nameof(IdInput));
            OnPropertyChanged(nameof(IdValidationVisibility));
            OnPropertyChanged(nameof(IdExistsVisibility));
            OnPropertyChanged(nameof(ECTSValidationVisibility));
        }

        private void ExecuteAddSubject(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewSubject.Naziv) ||
                string.IsNullOrWhiteSpace(ECTSInput) ||
                string.IsNullOrWhiteSpace(IdInput))
            {
                ShowWarning("FillField");
                return;
            }

            if (!int.TryParse(ECTSInput, out int parsedEcts) || parsedEcts is < 1 or > 8)
            {
                ShowWarning("ECTSRange");
                ECTSValidationVisibility = Visibility.Visible;
                OnPropertyChanged(nameof(ECTSValidationVisibility));
                return;
            }

            if (!int.TryParse(IdInput, out int parsedId))
            {
                ShowWarning("IdIntegerValue");
                IdValidationVisibility = Visibility.Visible;
                OnPropertyChanged(nameof(IdValidationVisibility));
                return;
            }

            if (Predmeti.Any(p => p.IdPredmeta == parsedId))
            {
                IdExistsMessage = (string)Application.Current.Resources["IdExist"];
                IdExistsVisibility = Visibility.Visible;
                OnPropertyChanged(nameof(IdExistsMessage));
                ShowWarning(IdExistsMessage);
                return;
            }

            NewSubject.ECTS = parsedEcts;
            NewSubject.IdPredmeta = parsedId;

            var command = new AddSubjectCommand(NewSubject, Predmeti);
            bool success = command.Execute();

            if (success)
                ShowSuccess("SuccessAddSubject");
            else
                ShowWarning("UnsuccessSubject");

            ClearFields();
        }

        private void ShowWarning(string resourceKey)
        {
            string message = (string)Application.Current.Resources[resourceKey];
            new WarningWindow(message).ShowDialog();
        }

        private void ShowSuccess(string resourceKey)
        {
            string message = (string)Application.Current.Resources[resourceKey];
            new SuccessWindow(message).ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
