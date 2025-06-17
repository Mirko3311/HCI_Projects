using ASystem;
using PrviProjektniZadatakHCI.Command;
using PrviProjektniZadatakHCI.DataAccess;
using PrviProjektniZadatakHCI.Util;
using PrviProjektniZadatakHCI.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;


namespace PrviProjektniZadatakHCI.ViewModel
{
    public class UpdateStudentViewModel : BaseViewModel
    {
        public ObservableCollection<Student> Students { get; }

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
                ((RelayCommand)UpdateStudentCommand).RaiseCanExecuteChanged();   
                GodinaStudijaInput = _selectedStudent != null ? _selectedStudent.GodinaStudija.ToString() : string.Empty;
            }
        }

        private bool _isGodinaStudijaValid = true;

        private string _godinaStudijaInput;
        public string GodinaStudijaInput
        {
            get => _godinaStudijaInput;
            set
            {
                _godinaStudijaInput = value;
                OnPropertyChanged();
            }
              
        }

        public ICommand UpdateStudentCommand { get; }

     

        public UpdateStudentViewModel(ObservableCollection<Student> sharedStudents)
        {
            Students = sharedStudents;
            UpdateStudentCommand = new RelayCommand(ExecuteUpdate, CanUpdate);
        }

        public UpdateStudentViewModel() : this(new ObservableCollection<Student>(StudentDataAccess.GetStudents())) { }

        private bool HasEmptyFields()
        {
              return string.IsNullOrWhiteSpace(SelectedStudent?.ime) ||
                      string.IsNullOrWhiteSpace(SelectedStudent?.prezime) ||
                      string.IsNullOrWhiteSpace(SelectedStudent?.email) ||
                      string.IsNullOrWhiteSpace(SelectedStudent?.username) ||
                 
                      string.IsNullOrWhiteSpace(SelectedStudent?.BrojIndeksa) ||
                      SelectedStudent.idKorisnika == 0 ||
                      SelectedStudent.GodinaStudija == 0;
        
        }
        private bool CanUpdate(object obj) => SelectedStudent != null;
        private void ExecuteUpdate(object obj)
        {
            if (HasEmptyFields())
            {
                string message = (string)Application.Current.Resources["FillField"];
                new WarningWindow(message).ShowDialog();
                return;
            }

            if (!ValidationHelper.IsValidEmail(SelectedStudent.email))
            {
                string message = (string)Application.Current.Resources["InvalidEmailFormat"];
                new WarningWindow(message).ShowDialog();
                return;
            }

            if (!int.TryParse(GodinaStudijaInput, out int parsedGodina))
            {
                string message = (string)Application.Current.Resources["AcademicIntegerValue"];
                new WarningWindow(message).ShowDialog();
                return;
            }

            if (parsedGodina < 1 || parsedGodina > 5)
            {
                string message = (string)Application.Current.Resources["AcademicYearRange"];
                new WarningWindow(message).ShowDialog();
                return;
            }
            SelectedStudent.GodinaStudija = parsedGodina;
            var cmd = new UpdateStudentCommand(SelectedStudent, Students);
            bool success = cmd.Execute();

            string messageKey = success ? "StudentSuccessfullyUpdated" : "UnsuccessfullyStudent";
            string finalMessage = (string)Application.Current.Resources[messageKey];

            if (success)
            {
             
                new SuccessWindow(finalMessage).ShowDialog();
            }
            else
            {
                new WarningWindow(finalMessage).ShowDialog();
            }
        }
    }
}
