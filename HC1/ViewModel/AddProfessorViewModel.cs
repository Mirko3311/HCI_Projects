using ASystem;
using PrviProjektniZadatakHCI.Command;
using PrviProjektniZadatakHCI.Util;
using PrviProjektniZadatakHCI.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PrviProjektniZadatakHCI.ViewModel
{
    public class AddProfessorViewModel : BaseViewModel
    {
        public Profesor NewProfessor { get; set; } = new Profesor();
        public ObservableCollection<Profesor> Profesori { get; }
        public ObservableCollection<Korisnik> Korisnici { get; }

        public event Action OnProfessorAdded;

        private string _professorId;
        public string ProfessorId
        {
            get => _professorId;
            set
            {
                _professorId = value;
                IdValidationVisibility = ShouldShowIdWarning(value) ? Visibility.Visible : Visibility.Collapsed;
                OnPropertyChanged(nameof(ProfessorId));
                OnPropertyChanged(nameof(IdValidationVisibility));
            }
        }

        private string _professorEmail;
        public string ProfessorEmail
        {
            get => _professorEmail;
            set
            {
                _professorEmail = value;
                NewProfessor.email = value;
                EmailValidationVisibility = ShouldShowEmailWarning(value) ? Visibility.Visible : Visibility.Collapsed;
                OnPropertyChanged(nameof(ProfessorEmail));
                OnPropertyChanged(nameof(EmailValidationVisibility));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NewProfessor.password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private bool _isPasswordVisible;
        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set
            {
                _isPasswordVisible = value;
                OnPropertyChanged(nameof(IsPasswordVisible));
                OnPropertyChanged(nameof(ToggleIcon));
            }
        }

        public string ToggleIcon => IsPasswordVisible ? "👁" : "👁‍🔦";

        public Visibility EmailValidationVisibility { get; private set; } = Visibility.Collapsed;
        public Visibility IdValidationVisibility { get; private set; } = Visibility.Collapsed;

        public ICommand AddProfessorCommand { get; }

        public AddProfessorViewModel(ObservableCollection<Profesor> sharedProfessors, ObservableCollection<Korisnik> sharedKorisnici)
        {
            Profesori = sharedProfessors;
            Korisnici = sharedKorisnici;
            AddProfessorCommand = new RelayCommand(ExecuteAddProfessor);
        }

        public void ClearFields()
        {
            NewProfessor = new Profesor();
            ProfessorEmail = string.Empty;
            ProfessorId = string.Empty;
            ClearPassword();
            EmailValidationVisibility = Visibility.Collapsed;
            IdValidationVisibility = Visibility.Collapsed;

            OnPropertyChanged(nameof(NewProfessor));
            OnPropertyChanged(nameof(ProfessorEmail));
            OnPropertyChanged(nameof(ProfessorId));
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(EmailValidationVisibility));
            OnPropertyChanged(nameof(IdValidationVisibility));
        }

        private void ExecuteAddProfessor(object _)
        {
            if (HasEmptyFields())
            {
                ShowWarning("FillField");
                return;
            }

            if (!ValidationHelper.IsValidEmail(NewProfessor.email))
            {
                ShowWarning("InvalidEmailFormat");
                return;
            }

            if (Korisnici.Any(p => p.username == NewProfessor.username))
            {
                ShowWarning("UsernameExist");
                return;
            }

            if (!int.TryParse(ProfessorId, out int parsedId))
            {
                IdValidationVisibility = Visibility.Visible;
                OnPropertyChanged(nameof(IdValidationVisibility));
                ShowWarning("IdIntegerValue");
                return;
            }

            if (Korisnici.Any(p => p.idKorisnika == parsedId))
            {
                ShowWarning("IdExist");
                return;
            }

            NewProfessor.idKorisnika = parsedId;
            var profCopy = CreateCopyWithHashedPassword(NewProfessor);

            var cmd = new AddProfessorCommand(profCopy, Profesori);
            if (cmd.Execute())
            {
                
                ShowSuccess("MessageProfAdd");
                ClearFields();
                OnProfessorAdded?.Invoke();
            }
            else
            {
                ShowWarning("UnsuccessfulStudent");
            }
        }

        private bool HasEmptyFields() =>
            string.IsNullOrWhiteSpace(NewProfessor.ime) ||
            string.IsNullOrWhiteSpace(NewProfessor.prezime) ||
            string.IsNullOrWhiteSpace(NewProfessor.username) ||
            string.IsNullOrWhiteSpace(NewProfessor.password) ||
            string.IsNullOrWhiteSpace(NewProfessor.email) ||
            string.IsNullOrWhiteSpace(ProfessorId);

        private static bool ShouldShowEmailWarning(string email) =>
            !string.IsNullOrWhiteSpace(email) && !ValidationHelper.IsValidEmail(email);

        private static bool ShouldShowIdWarning(string id) =>
            !string.IsNullOrWhiteSpace(id) && !int.TryParse(id, out _);

        private Profesor CreateCopyWithHashedPassword(Profesor p) =>
            new Profesor
            {
                idKorisnika = p.idKorisnika,
                ime = p.ime,
                prezime = p.prezime,
                email = p.email,
                username = p.username,
                password = BCrypt.Net.BCrypt.HashPassword(p.password),
                tipKorisnika = "profesor",
                Zvanje = p.Zvanje
            };

        private void ShowWarning(string key) =>
            new WarningWindow((string)Application.Current.Resources[key]).ShowDialog();

        private void ShowSuccess(string key) =>
            new SuccessWindow((string)Application.Current.Resources[key]).ShowDialog();
    }
}
