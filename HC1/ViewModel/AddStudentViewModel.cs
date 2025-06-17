using ASystem;
using PrviProjektniZadatakHCI.Command;
using PrviProjektniZadatakHCI.Util;
using PrviProjektniZadatakHCI.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PrviProjektniZadatakHCI.ViewModel
{
    public class AddStudentViewModel : BaseViewModel
    {
        public Student NewStudent { get; set; } = new();
        public ObservableCollection<Student> Students { get; }
        public ObservableCollection<Korisnik> Korisnici { get; }

        public ICommand AddStudentCommand { get; }

        public AddStudentViewModel(ObservableCollection<Student> sharedStudents, ObservableCollection<Korisnik> korisnici)
        {
            Students = sharedStudents;
            AddStudentCommand = new RelayCommand(ExecuteAddStudent);
            Korisnici = korisnici;
        }

        private string _studentId;
        public string StudentId
        {
            get => _studentId;
            set
            {
                _studentId = value;
                OnPropertyChanged();
                ValidateId();
            }
        }

        private string _godinaStudijaInput;
        public string GodinaStudijaInput
        {
            get => _godinaStudijaInput;
            set
            {
                _godinaStudijaInput = value;
                OnPropertyChanged();
                NewStudent.GodinaStudija = int.TryParse(value, out var g) && g is >= 1 and <= 5 ? g : 0;
            }
        }

        private string _studentEmail;
        public string StudentEmail
        {
            get => _studentEmail;
            set
            {
                _studentEmail = value;
                NewStudent.email = value;
                OnPropertyChanged();
                ValidateEmail();
            }
        }

        private string _studentPassword;
        public string StudentPassword
        {
            get => _studentPassword;
            set
            {
                _studentPassword = value;
                NewStudent.password = value;
                OnPropertyChanged();
            }
        }
        private bool _isEmailInvalid;
        public bool IsEmailInvalid
        {
            get => _isEmailInvalid;
            set
            {
                _isEmailInvalid = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EmailValidationVisibility));
            }
        }

        private bool _isIdInvalid;
        public bool IsIdInvalid
        {
            get => _isIdInvalid;
            set
            {
                _isIdInvalid = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IdValidationVisibility));
            }
        }
        public Visibility EmailValidationVisibility => IsEmailInvalid ? Visibility.Visible : Visibility.Collapsed;
        public Visibility IdValidationVisibility => IsIdInvalid ? Visibility.Visible : Visibility.Collapsed;

        public Visibility UsernameValidationVisibility { get; set; } = Visibility.Collapsed;
        public Visibility BrojIndeksaValidationVisibility { get; set; } = Visibility.Collapsed;
        public string UsernameValidationMessage { get; set; }
        public string BrojIndeksaValidationMessage { get; set; }

        private bool _isPasswordVisible;
        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set
            {
                _isPasswordVisible = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ToggleIcon));
            }
        }

        public string ToggleIcon => IsPasswordVisible ? "👁" : "👁‍🗨";
        private void ValidateId()
        {
            IsIdInvalid = !int.TryParse(StudentId, out var parsed);
            if (!IsIdInvalid)
                NewStudent.idKorisnika = parsed;
        }

        private void ValidateEmail()
        {
            IsEmailInvalid = !string.IsNullOrWhiteSpace(StudentEmail) && !ValidationHelper.IsValidEmail(StudentEmail);
        }

   

        private void ExecuteAddStudent(object parameter)
        {
            ValidateId();
            ValidateEmail();

            if (IsIdInvalid)
            {
                ShowWarning("IdIntegerValue");
                return;
            }

            if (NewStudent.GodinaStudija is < 1 or > 5)
            {
                ShowWarning("AcademicYearRange");
                return;
            }

            if (string.IsNullOrWhiteSpace(NewStudent.ime) ||
                string.IsNullOrWhiteSpace(NewStudent.prezime) ||
                string.IsNullOrWhiteSpace(NewStudent.email) ||
                string.IsNullOrWhiteSpace(NewStudent.username) ||
                string.IsNullOrWhiteSpace(NewStudent.password) ||
                string.IsNullOrWhiteSpace(StudentId) ||
                string.IsNullOrWhiteSpace(NewStudent.BrojIndeksa) ||
                NewStudent.GodinaStudija == 0)
            {
                ShowWarning("FillField");
                return;
            }

            if (Korisnici.Any(s => s.username == NewStudent.username))
            {
                ShowWarning("UsernameExist");
                return;
            }

            if (Students.Any(s => s.BrojIndeksa == NewStudent.BrojIndeksa))
            {
                ShowWarning("IndexExists");
                return;
            }

            if (Korisnici.Any(s => s.idKorisnika == NewStudent.idKorisnika))
            {
                ShowWarning("IdExist");
                return;
            }

            if (!ValidationHelper.IsValidEmail(NewStudent.email))
            {
                ShowWarning("InvalidEmailFormat");
                return;
            }

            var studentCopy = new Student
            {
                idKorisnika = NewStudent.idKorisnika,
                ime = NewStudent.ime,
                prezime = NewStudent.prezime,
                email = NewStudent.email,
                username = NewStudent.username,
                password = BCrypt.Net.BCrypt.HashPassword(NewStudent.password),
                BrojIndeksa = NewStudent.BrojIndeksa,
                GodinaStudija = NewStudent.GodinaStudija
            };

            bool success = new AddStudentCommand(studentCopy, Students).Execute();
            ShowMessage(success ? "SuccessAddStudent" : "UnsuccessfulStudent");
            ClearFields();
            OnPropertyChanged(nameof(NewStudent));
        }

        public void ClearFields()
        {
            NewStudent = new();
            StudentEmail = StudentPassword = StudentId = GodinaStudijaInput = string.Empty;

            IsEmailInvalid = false;
            IsIdInvalid = false;

            ClearPassword();
            OnPropertyChanged(null);
        }

        private void ShowWarning(string resourceKey) =>
            new WarningWindow((string)Application.Current.Resources[resourceKey]).ShowDialog();

        private void ShowMessage(string resourceKey)
        {
            string message = (string)Application.Current.Resources[resourceKey];
            if (resourceKey.StartsWith("Success"))
                new SuccessWindow(message).ShowDialog();
            else
                new WarningWindow(message).ShowDialog();
        }
    }
}


